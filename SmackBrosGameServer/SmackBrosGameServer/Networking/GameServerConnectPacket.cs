﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class GameServerConnectPacket:Packet
    {
        public GameServerConnectPacket()
        {
            typeID = 4;
        }
        public override void ReadPacketData(System.IO.Stream stream)
        {
            throw new NotImplementedException();
        }
        public override void WritePacketData(List<byte> stream)
        {
            throw new NotImplementedException();
        }
    }
}
