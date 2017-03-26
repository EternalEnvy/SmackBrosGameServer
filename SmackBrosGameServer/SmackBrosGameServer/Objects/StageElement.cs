using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    public class StageElement
    {
        Rectangle _bounds;
        bool _canGoThroughFromBelow;
        public Rectangle GetBounds()
        {
            return _bounds;
        }
        public bool HitElement(Vector2 pos, int height)
        {
            if(_canGoThroughFromBelow)
            {
                if(_bounds.Intersects(pos) && pos.Y + height > (_bounds.Botl + (_bounds.Botr - _bounds.Botl).Normalize() * Vector2.Projection(_bounds.Botr - _bounds.Botl, pos).Length).Y)
                {
                    return true;
                }
            }
            else
            {
                if(_bounds.Intersects(pos))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
