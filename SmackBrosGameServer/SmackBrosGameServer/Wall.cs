using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    struct Wall
    {
        Vector2 topR;
        Vector2 topL;
        Vector2 botL;
        Vector2 botR;
        bool canTech;
        public bool HitWall(Vector2 pos)
        {
            if(pos.X < topR.X && pos.X > topL.X && pos.Y < botL.Y && pos.Y > botR.Y)
                return true;
            else return false;
        }
    }
}
