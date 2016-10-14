using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    public class StageElement
    {
        Rectangle bounds;
        bool canGoThroughFromBelow;
        public bool HitElement(Vector2 pos, int height)
        {
            if(canGoThroughFromBelow)
            {
                if(bounds.Intersects(pos) && pos.Y + height > (bounds.BOTL + (bounds.BOTR - bounds.BOTL).Normalize() * Vector2.Projection(bounds.BOTR - bounds.BOTL, pos).Length).Y)
                {
                    return true;
                }
            }
            else
            {
                if(bounds.Intersects(pos))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
