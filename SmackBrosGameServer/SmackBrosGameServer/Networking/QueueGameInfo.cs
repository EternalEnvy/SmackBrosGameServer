using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    class QueueGameInfoPacket : Packet
    {
        List<string> ipAddresses = new List<string>();
        public override void WritePacketData(List<byte> stream)
        {
            WriteInt(stream, ipAddresses.Count());
            for(int i = 0, len = ipAddresses.Count(); i < len; i++)
            {
                WriteStringBytes(stream, ipAddresses[i]);
            }
        }
        public override void ReadPacketData(Stream stream)
        {
            for(int i = 0, len = ReadInt(stream); i < len; i++)
            {
                ipAddresses[i] = ReadString(stream);
            }
        }
    }
}
