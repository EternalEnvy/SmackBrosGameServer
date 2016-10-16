using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class PickledSmackerData
    {
        public Vector2 position;
        public short id;
        public short state;
        public short stateDuration;
        public bool isFacingRight;

        public PickledSmackerData(Vector2 position, short id, short state, short stateDuration, bool isFacingRight)
        {
            this.position = position;
            this.id = id;
            this.state = state;
            this.stateDuration = stateDuration;
            this.isFacingRight = isFacingRight;
        }
    }
}
