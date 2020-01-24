using System.Collections.Generic;
using Movement.Collisions;
using PlayerDan;
using UnityEngine;

namespace Movement
{
    public interface IMovementController
    {
        void Move(Vector2 velocity);
        Vector2 Velocity { get; }
        List<CollisionData> Collisions { get; }
    }
}