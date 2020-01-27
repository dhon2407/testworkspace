using System.Collections.Generic;
using UnityEngine;

namespace DM2DMovement.Collisions
{
    public interface ICollisionDetector
    {
        List<CollisionData> GetCollisions(Vector2 velocity);
        void CheckVerticalCollisions(Vector2 velocity);
        void CheckHorizontalCollisions(Vector2 velocity);
    }
}