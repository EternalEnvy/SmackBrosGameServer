using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    struct Rectangle
    {
        Vector2 topR;
        Vector2 topL;
        Vector2 botL;
        Vector2 botR;
        bool canTech;
        public bool Intersects(Vector2 pos)
        {
            if(pos.X < topR.X && pos.X > topL.X && pos.Y < botL.Y && pos.Y > botR.Y)
                return true;
            else return false;
        }
        public Vector2 TOPR
        {
            get { return topR; }
        }
        public Vector2 TOPL
        {
            get { return topL; }
        }
        public Vector2 BOTR
        {
            get { return botR; }
        }
        public Vector2 BOTL
        {
            get { return botL; }
        }
    }
}
