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
        public string nameToConnect;
        public override void WritePacketData(List<byte> stream)
        {
            WriteStringBytes(stream, ipAddressToConnectTo);
            WriteStringBytes(stream, nameToConnect);
        }
        public override void ReadPacketData(Stream stream)
        {
            ipAddressToConnectTo = ReadString(stream);
            nameToConnect = ReadString(stream);
        }
    }
}
