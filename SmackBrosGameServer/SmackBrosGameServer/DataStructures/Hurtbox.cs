using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer{

    public struct HurtBoxData
    {
        public Vector2 Position;
        public float Radius;
        public bool Invincible;
    }
    public class Hurtbox
    {
        public List<HurtBoxData> HurtBoxes = new List<HurtBoxData>();
    }
}
