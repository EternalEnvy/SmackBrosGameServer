using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace SmackBrosGameServer
{
    class HitboxCollisionSphere
    {
        private int curFrame = 0;
        private List<Vector2> transformations = new List<Vector2>();

        public float radius;
        public int priority;
        public int damage;
      
        public Vector2 centre;       
        public Vector2 directionKnock;
        public Vector2[] POI = new Vector2[4];

        public HitboxCollisionSphere(List<Vector2> transforms, Vector2 pos, float rad, Vector2 direction, int dmg, int prio)
        {
            damage = dmg;
            transformations = transforms;
            directionKnock = direction;
            centre = pos;
            radius = rad;
            priority = prio;
            POI[0] = centre + new Vector2(rad, 0);
            POI[1] = centre + new Vector2(-rad, 0);
            POI[2] = centre + new Vector2(0, rad);
            POI[3] = centre + new Vector2(0, -rad);
        }
        public void Update()
        {
            centre += transformations[curFrame];
            for(int i = 0; i < 4; i++)
            {
                POI[i] += transformations[curFrame];
            }
            curFrame++;
        }
    }
}
