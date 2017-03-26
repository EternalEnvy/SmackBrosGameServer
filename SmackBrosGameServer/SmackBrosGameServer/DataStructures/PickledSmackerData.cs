using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class PickledSmackerData
    {
        public Vector2 Position;
        public short Id;
        public short State;
        public short StateDuration;
        public bool IsFacingRight;

        public PickledSmackerData(Vector2 position, short id, short state, short stateDuration, bool isFacingRight)
        {
            this.Position = position;
            this.Id = id;
            this.State = state;
            this.StateDuration = stateDuration;
            this.IsFacingRight = isFacingRight;
        }
    }
}
