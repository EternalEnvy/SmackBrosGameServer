using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SmackBrosGameServer
{
    struct CollisionData
    {
        int _damage;
        int _priority;
        Vector2 _direction;
        public CollisionData(int dmg, int prio, Vector2 dir)
        {
            _damage = dmg;
            _priority = prio;
            _direction = dir;
        }
        public int Priority
        {
            get
            {
                return _priority;
            }
        }
    }
    class Hitbox
    {
        List<HitboxCollisionSphere> _currentHitboxes = new List<HitboxCollisionSphere>();
        List<HitboxCollisionSphere> _lastFrameHitboxes = new List<HitboxCollisionSphere>();

        public object Collision(Hurtbox hurtbox)
        {
            List<CollisionData> collisions = new List<CollisionData>();
            foreach(HurtBoxData collisionBubble in hurtbox.HurtBoxes)
            {
                foreach(HitboxCollisionSphere hcs in _currentHitboxes)
                {
                    var d = Math.Round(Vector2.Distance(collisionBubble.Position, hcs.Centre));
                    var e = Math.Round(collisionBubble.Radius + hcs.Radius);
                    if(Math.Abs(e - d) < 0.005)
                    {
                        //phantom hit
                        collisions.Add(new CollisionData(hcs.Priority, hcs.Damage / 2, Vector2.Zero));
                    }
                    if(e < d)
                    {
                        collisions.Add(new CollisionData(hcs.Priority, hcs.Damage, hcs.DirectionKnock));
                    }
                }
                for(int i = Math.Min(_currentHitboxes.Count, _lastFrameHitboxes.Count); i >= 0; i--)
                {
                    if (IntersectsRay(_currentHitboxes[i].Centre, _lastFrameHitboxes[i].Centre, collisionBubble.Position, collisionBubble.Radius))
                        collisions.Add(new CollisionData(_currentHitboxes[i].Priority, _currentHitboxes[i].Damage, _currentHitboxes[i].DirectionKnock));
                }
            }
            Sort_Merge(ref collisions, 0, collisions.Count);
            if (collisions.Count > 0)
                return collisions[0];
            else return null;
        }
        private void Merge_By_Priority(ref List<CollisionData> collisions, int left, int middle, int right)
        {
            //find the lengths of the two halves
            int lengthLeft = middle - left;
            int lengthRight = right - middle;
            //set the final element of both arrays to infinity
            CollisionData[] leftArray = new CollisionData[lengthLeft + 2];
            CollisionData temp = new CollisionData(0, 0, Vector2.Zero);
            leftArray[lengthLeft + 1] = temp;
            CollisionData[] rightArray = new CollisionData[lengthRight + 1];
            rightArray[lengthRight] = temp;
            //create the two arrays that will be used to 
            for (int i = 0; i <= lengthLeft; i++)
            {
                leftArray[i] = collisions[left + i];
            }
            for (int j = 0; j < lengthRight; j++)
            {
                rightArray[j] = collisions[middle + j + 1];
            }
            int iIndex = 0;
            int jIndex = 0;
            //take the lower element of the two arrays and add it to the new sorted array
            for (int k = left; k <= right; k++)
            {
                if (leftArray[iIndex].Priority >= rightArray[jIndex].Priority)
                {
                    collisions[k] = leftArray[iIndex];
                    iIndex++;
                }
                else
                {
                    collisions[k] = rightArray[jIndex];
                    jIndex++;
                }
            }
        }
        private void Sort_Merge(ref List<CollisionData> collisions, int left, int right)
        {
            //Mergesort is the "serious" sorting algorithm
            int mid;
            if (right > left)
            {
                //find a midpoint
                mid = (right + left) / 2;
                //recursively call this function for each half of the current array
                Sort_Merge(ref collisions, left, mid);
                Sort_Merge(ref collisions, mid + 1, right);
                //once that is done, merge it
                Merge_By_Priority(ref collisions, left, mid, right);
            }
        }
        private bool IntersectsRay(Vector2 rayStart, Vector2 rayEnd, Vector2 centre, double radius)
        {
            Vector2 directionVector = rayEnd - rayStart;
            Vector2 centreToStart = rayStart - centre;
            double a = Vector2.Dot(directionVector, directionVector);
            double b = 2 * Vector2.Dot(directionVector, centreToStart);
            double c = (float)(Vector2.Dot(centreToStart, centreToStart) - (radius * radius));

            double discriminant = b * b - 4 * a * c;
            if (discriminant < 0)
            {
                return false;
            }
            else
            {
                // ray didn't totally miss sphere,
                // so there is a solution to
                // the equation.

                discriminant = Math.Sqrt(discriminant);

                // either solution may be on or off the ray so need to test both
                // t1 is always the smaller value, because BOTH discriminant and
                // a are nonnegative.
                double t1 = (-b - discriminant) / (2 * a);
                double t2 = (-b + discriminant) / (2 * a);

                // 3x HIT cases:
                //          -o->             --|-->  |            |  --|->
                // Impale(t1 hit,t2 hit), Poke(t1 hit,t2>1), ExitWound(t1<0, t2 hit), 

                // 3x MISS cases:
                //       ->  o                     o ->              | -> |
                // FallShort (t1>1,t2>1), Past (t1<0,t2<0), CompletelyInside(t1<0, t2>1)

                if (t1 >= 0 && t1 <= 1 || t2 >= 0 && t2 <= 1)
                {
                    // t1 is the intersection, and it's closer than t2
                    // (since t1 uses -b - discriminant)
                    return true;
                }
                return false;
            }
        }
    }
}
