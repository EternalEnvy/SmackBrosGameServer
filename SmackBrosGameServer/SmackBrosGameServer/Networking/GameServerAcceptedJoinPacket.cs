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
        public string ipToSend = "";
        public GameServerAcceptedJoinPacket()
        {
            typeID = 5;
        }
        public override void ReadPacketData(Stream stream)
        {
            Accepted = stream.ReadByte() == 1;
            ipToSend = ReadString(stream);
        }
        public override void WritePacketData(List<byte> stream)
        {
            WriteBool(stream, Accepted);
            WriteStringBytes(stream, ipToSend);
        }
    }
}
