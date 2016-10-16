using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    public struct Ledge
    {
        const int invicibilityTime = 150; //frames
        bool hasPlayerActive;
        bool isFacingRight;
        public Vector2 myOrigin;
        float radius;
        public bool CollideLedge(Vector2 pos, bool FacingRight)
        {
            if((pos - myOrigin).Length < radius && (isFacingRight == FacingRight))
            {
                hasPlayerActive = true;
                return true;
            }
            return false;
        }
    }
}
