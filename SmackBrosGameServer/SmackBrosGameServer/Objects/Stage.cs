using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class Stage
    {
        List<Ledge> ledges = new List<Ledge>();
        List<StageElement> barriers = new List<StageElement>();
        public Vector2 GrabLedge(Vector2 pos, bool isFacingRight)
        {
            foreach(Ledge ledge in ledges)
            {
                if(ledge.CollideLedge(pos, isFacingRight))
                {
                    return ledge.myOrigin;
                }
            }
            return null;
        }
        public bool OnGround(Vector2 lowestPoint, Vector2 velocity)
        {
            if (velocity.Y > 0)
            {
                foreach (StageElement s in barriers)
                {
                    if (s.GetBounds().Intersects(lowestPoint))
                    {
                        return true;
                    }
                }
                return false;
            }
            else return false;
        }
    }
}
