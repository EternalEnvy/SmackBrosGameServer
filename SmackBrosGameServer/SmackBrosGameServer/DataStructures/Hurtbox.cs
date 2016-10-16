using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer{

    struct HurtBoxData
    {
        public Vector2 position;
        public float radius;
        public bool invincible;
    }
    class Hurtbox
    {
        public List<HurtBoxData> hurtBoxes = new List<HurtBoxData>();
    }
}
