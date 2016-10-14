using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    class QueueFinishedPacket : Packet
    {
        public string ipAddressToConnectTo;
        public short NumPlayers;
        public bool[] PlayersAccepted;
        public override void WritePacketData(List<byte> stream)
        {
            WriteShort(stream, NumPlayers);
            byte mask = 0;
            for (int i = NumPlayers - 1; i >= 0; i--)
            {
                mask = (byte)(mask | (PlayersAccepted[i] ? 1 << i : 0));
            }
            WriteStringBytes(stream, ipAddressToConnectTo);
        }
        public override void ReadPacketData(Stream stream)
        {
            NumPlayers = ReadShort(stream);
            PlayersAccepted = new bool[NumPlayers];
            var b = stream.ReadByte();
            for (int k = NumPlayers - 1; k >= 0; k++)
            {
                PlayersAccepted[k] = ((b >> k) & 1) == 1;
            }
            ipAddressToConnectTo = ReadString(stream);
        }
    }
}
