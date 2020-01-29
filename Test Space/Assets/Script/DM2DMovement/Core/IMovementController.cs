using System.Collections.Generic;
using DM2DMovement.Collisions;
using UnityEngine;

namespace DM2DMovement.Core
{
    public interface IMovementController
    {
        void Move(Vector2 velocity);
        Vector2 Velocity { get; }
        Vector2 Position { get; }
        
        bool OnGround { get; }

        List<CollisionData> Collisions { get; }
    }
}