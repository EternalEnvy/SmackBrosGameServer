using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    class HitboxCollisionSphere
    {
        private int curFrame = 0;
        private List<Vector2> positions = new List<Vector2>();

        public float radius;
        public int priority;
        public int damage;
      
        public Vector2 centre;       
        public Vector2 directionKnock;

        public HitboxCollisionSphere(List<Vector2> positions, Vector2 start, Vector2 direction, float rad, int dmg, int prio)
        {
            this.damage = dmg;
            this.positions = positions;
            this.directionKnock = direction;
            this.radius = rad;
            this.priority = prio;
            this.centre = start;
        }
    }
}
