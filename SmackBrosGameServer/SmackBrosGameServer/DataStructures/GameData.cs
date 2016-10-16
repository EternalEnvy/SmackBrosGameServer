using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    public struct GameData
    {
        public bool GamePaused;
        public bool GameReadyToStart;
        public double pauseAlpha;
        public long? FrameNumber;
    }
}
