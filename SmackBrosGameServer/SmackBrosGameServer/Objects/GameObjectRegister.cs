using System;
using System.Collections.Generic;

namespace SmackBrosGameServer
{
    public class GameObjectRegister
    {
        HashSet<IGameObject> _gameObjects = new HashSet<IGameObject>();

        public double ClosestObject(Vector2 rayOrigin, Vector2 rayDirection, out IGameObject intersect)
        {
            if (rayOrigin == null) throw new ArgumentNullException("rayOrigin");
            if (rayDirection == null) throw new ArgumentNullException("rayDirection");
            double closestDistance = Double.MaxValue;
            IGameObject closest = null;
            foreach (IGameObject obj in _gameObjects)
            {
                double distance = obj.IntersectsRay(rayOrigin, rayDirection);
                if (distance > 0 && closestDistance > distance)
                {
                    closest = obj;
                    closestDistance = distance;
                }
            }
            intersect = closest;
            return closestDistance;
        }
    }
}
