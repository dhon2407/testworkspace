using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public interface ICollisionDetector
    {
        List<CollisionData> GetCollisions(Vector2 velocity);
        void CheckVerticalCollisions(Vector2 velocity);
        void CheckHorizontalCollisions(Vector2 velocity);
    }
}