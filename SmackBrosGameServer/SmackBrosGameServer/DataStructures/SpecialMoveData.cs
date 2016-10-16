using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SmackBrosGameServer
{
    public abstract class SpecialMoveData
    {
        //postion relative to character, launch direction, power, radius, damage 
        public abstract Tuple<Vector2, Vector2, float, float, int> Up(int frameNumber);
        public abstract Tuple<Vector2, Vector2, float, float, int> Down(int frameNumber);
        public abstract Tuple<Vector2, Vector2, float, float, int> Neutral(int frameNumber);
        public abstract Tuple<Vector2, Vector2, float, float, int> Forward(int frameNumber);
        public abstract bool LoadData(string path);
    }
}
