using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace SmackBrosGameServer
{
    class PacketQueue
    {
        static List<IPEndPoint> _clientIpList = new List<IPEndPoint>();
        bool _receiveActive = true;
        static readonly object Lockobj = new object();
        public static PacketQueue Instance = new PacketQueue();
        List<Packet> _queue = new List<Packet>();
        long _id = 0;
        long _lastReceivedFromOther = -1;
        long _lastReceivedFromMe = -1;

        public void AddPacket(Packet packet)
        {
            lock (Lockobj)
                _queue.Add(packet);
            ++_id;
        }

        private long ReadLong(Stream stream)
        {
            var bytes = new byte[8];
            stream.Read(bytes, 0, 8);
            if (BitConverter.IsLittleEndian)
                bytes = bytes.Reverse().ToArray();
            return BitConverter.ToInt64(bytes, 0);
        }

        private void WriteLong(List<byte> stream, long num)
        {
            var bytes = BitConverter.GetBytes(num);
            if (BitConverter.IsLittleEndian)
                bytes = bytes.Reverse().ToArray();
            stream.AddRange(bytes);
        }
        public void Deactivate()
        {
            _receiveActive = false;
        }
        public void AddClient(string ip, int port)
        {
            _clientIpList.Add(new IPEndPoint(new IPAddress(ip.Split('.').Select(byte.Parse).ToArray()), port));
        }
        public Packet[] ReceivePackets(Stream stream)
        {
            //Read the most recent packet received in the current batch.
            var newLastReceivedFromOther = ReadLong(stream);

            //Get the number of new packets received in this call, this number may be negative, which is fine as the packets will be read and discarded.
            var numPackets2 = newLastReceivedFromOther - _lastReceivedFromOther;

            //Read the most recent packet received from the other client.
            var newLastReceivedFromMe = ReadLong(stream);

            var amount = newLastReceivedFromMe - _lastReceivedFromMe;
            if (amount > 0)
            {
                lock (Lockobj)
                    _queue.RemoveRange(0, (int)amount);
                _lastReceivedFromMe = newLastReceivedFromMe;
            }

            //The number of packets received in this batch
            var numPackets = ReadLong(stream);

            //Only return new packets
            Packet[] packets = new Packet[numPackets2 < 0 ? 0 : numPackets2];
            for (var i = 0; i < numPackets; i++)
            {
                //If the packet is new, store it
                if (newLastReceivedFromOther - numPackets + i >= _lastReceivedFromOther)
                    packets[newLastReceivedFromOther - numPackets + i - _lastReceivedFromOther] = Packet.ReadPacket(stream);
                else
                    //We still have to read it if it's old, but we just don't use it.
                    Packet.ReadPacket(stream);
            }
            _lastReceivedFromOther = newLastReceivedFromOther;

            return packets;
        }

        public void WritePackets(List<byte> stream)
        {
            WriteLong(stream, _id - 1);
            WriteLong(stream, _lastReceivedFromOther);

            lock (Lockobj)
            {
                var numPackets = _queue.Count;

                WriteLong(stream, numPackets);

                for (int i = 0; i < numPackets; i++)
                {
                    Packet.WritePacket(stream, _queue[i]);
                }
            }
        }

        public void ReceivingLoop(UdpClient client, IPEndPoint serverIp, Queue<Packet> queue, object queueLock)
        {
            var reset = serverIp.Address.Equals(IPAddress.Any);
            while (_receiveActive)
            {
                var res = client.Receive(ref serverIp);
                var otherIp = serverIp.Address;
                if (reset)
                    serverIp = new IPEndPoint(IPAddress.Any, serverIp.Port);
                var stream = new MemoryStream(res);
                var packets = ReceivePackets(stream);
                foreach (var packet in packets)
                {
                    lock (queueLock)
                    {

                    }
                        /*if (packet.GetPacketType() == 1)
                        {
                            var pac = (QueueInteractionPacket)packet;
                            pac.IPAddress = string.Join(".", otherIP.GetAddressBytes().Select(a => a.ToString()));
                            queue.Enqueue(pac);
                        }
                        else
                        {
                            queue.Enqueue(packet);
                        }*/
                }
            }
        }
        public static void SendFunc(UdpClient client)
        {
            var buffer = new List<byte>();
            Instance.WritePackets(buffer);
            foreach(IPEndPoint endPoint in _clientIpList)
            {
                client.Send(buffer.ToArray(), buffer.Count, endPoint);
            }
            
        }
    }
}
