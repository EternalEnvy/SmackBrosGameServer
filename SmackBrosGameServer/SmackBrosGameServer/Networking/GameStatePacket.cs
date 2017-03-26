using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    class GameStatePacket : Packet
    {
        public long Sequence;
        private static long _lastSequence = -1;
        //we need fields: Position, current state enumerated, frame # in current state, is facing right, and id
        //this is low level stuff, so it's easier in this case to work with the Tuple
        public List<PickledSmackerData> Smackers = new List<PickledSmackerData>();
        public GameStatePacket()
        {
            TypeId = 7;
            Sequence = ++_lastSequence;
        }
        public override void ReadPacketData(Stream stream)
        {
            short numSmackers = ReadShort(stream);
            for (int i = 0; i < numSmackers; i++)
            {
                Smackers.Add(new PickledSmackerData(
                    ReadVector2(stream),
                    ReadShort(stream),
                    ReadShort(stream),
                    ReadShort(stream),
                    ReadBool(stream)));
            }
        }
        public override void WritePacketData(List<byte> stream)
        {
            WriteShort(stream, (short)Smackers.Count());
            for(int i = 0; i < Smackers.Count(); i++)
            {
                WriteVector2(stream, Smackers[i].Position);
                WriteShort(stream, Smackers[i].Id);
                WriteShort(stream, Smackers[i].State);
                WriteShort(stream, Smackers[i].StateDuration);
                WriteBool(stream, Smackers[i].IsFacingRight);
            }
        }
    } 
}
