using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    class GameServerAcceptedJoinPacket:Packet
    {
        public bool Accepted;
        public string IpToSend = "";
        public GameServerAcceptedJoinPacket()
        {
            TypeId = 5;
        }
        public override void ReadPacketData(Stream stream)
        {
            Accepted = stream.ReadByte() == 1;
            IpToSend = ReadString(stream);
        }
        public override void WritePacketData(List<byte> stream)
        {
            WriteBool(stream, Accepted);
            WriteStringBytes(stream, IpToSend);
        }
    }
}
