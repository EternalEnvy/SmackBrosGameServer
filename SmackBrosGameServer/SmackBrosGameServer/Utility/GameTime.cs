using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    public class GameTime
    {
        private TimeSpan internalTime;
        public TimeSpan ElapsedGameTime
        {
            get { return internalTime; }
            set { internalTime = value; }
        }
        public bool operator>(GameTime g1, GameTime g2)
        {
            return g1.ElapsedGameTime > g2.ElapsedGameTime;
        }
        public bool operator<(GameTime g1, GameTime g2)
        {
            return g1.ElapsedGameTime < g2.ElapsedGameTime;
        }
        public bool operator>=(GameTime g1, GameTime g2)
        {
            return g1.ElapsedGameTime >= g2.ElapsedGameTime;
        }
        public bool operator<=(GameTime g1, GameTime g2)
        {
            return g1.ElapsedGameTime <= g2.ElapsedGameTime;
        }
    }
}
