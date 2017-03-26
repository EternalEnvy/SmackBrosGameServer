using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class HitboxCollisionSphere
    {
        private int _curFrame = 0;
        private List<Vector2> _positions = new List<Vector2>();

        public float Radius;
        public int Priority;
        public int Damage;
      
        public Vector2 Centre;       
        public Vector2 DirectionKnock;

        public HitboxCollisionSphere(List<Vector2> positions, Vector2 start, Vector2 direction, float rad, int dmg, int prio)
        {
            this.Damage = dmg;
            this._positions = positions;
            this.DirectionKnock = direction;
            this.Radius = rad;
            this.Priority = prio;
            this.Centre = start;
        }
    }
}
