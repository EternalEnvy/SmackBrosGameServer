using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    class QueueInteractionPacket : Packet
    {
        public string Name;
        public short Mmr;
        public string IpAddress;
        public bool Joining;
        public QueueInteractionPacket()
        {
            TypeId = 1;
        }
        public override void WritePacketData(List<byte> stream)
        {
            WriteBool(stream, Joining);
            WriteStringBytes(stream, Name);
            WriteShort(stream, Mmr);
            WriteStringBytes(stream, IpAddress);
        }
        public override void ReadPacketData(Stream stream)
        {
            Joining = ReadBool(stream);
            Name = ReadString(stream);
            Mmr = ReadShort(stream);
            IpAddress = ReadString(stream);
        }
    } 
}
