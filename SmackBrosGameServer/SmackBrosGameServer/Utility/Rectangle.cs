using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    struct Rectangle
    {
        Vector2 _topR;
        Vector2 _topL;
        Vector2 _botL;
        Vector2 _botR;
        bool _canTech;
        public bool Intersects(Vector2 pos)
        {
            if(pos.X < _topR.X && pos.X > _topL.X && pos.Y < _botL.Y && pos.Y > _botR.Y)
                return true;
            else return false;
        }
        public Vector2 Topr
        {
            get { return _topR; }
        }
        public Vector2 Topl
        {
            get { return _topL; }
        }
        public Vector2 Botr
        {
            get { return _botR; }
        }
        public Vector2 Botl
        {
            get { return _botL; }
        }
    }
}
