using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class Ledge
    {
        const double invicibilityTime = 2.5;
        bool hasPlayerActive;
        bool isFacingRight;
        double timePlayerOnLedge;
        Vector2 myOrigin;
        float radius;
        public void CollideLedge(Smacker smacker)
        {
            if((smacker.Position - myOrigin).Length < radius && (isFacingRight == smacker.isFacingRight))
            {
                
            }
        }
    }
}
