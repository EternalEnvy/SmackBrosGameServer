using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    class QueueStatusUpdatePacket : Packet
    {
        public bool Accepted;
        public override void WritePacketData(List<byte> stream)
        {

        }
        public override void ReadPacketData(Stream stream)
        {
        }
    } 
}
