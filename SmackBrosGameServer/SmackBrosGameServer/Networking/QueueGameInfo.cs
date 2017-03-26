using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    class QueueGameInfoPacket : Packet
    {
        List<string> _ipAddresses = new List<string>();
        public override void WritePacketData(List<byte> stream)
        {
            WriteInt(stream, _ipAddresses.Count());
            for(int i = 0, len = _ipAddresses.Count(); i < len; i++)
            {
                WriteStringBytes(stream, _ipAddresses[i]);
            }
        }
        public override void ReadPacketData(Stream stream)
        {
            for(int i = 0, len = ReadInt(stream); i < len; i++)
            {
                _ipAddresses[i] = ReadString(stream);
            }
        }
    }
}
