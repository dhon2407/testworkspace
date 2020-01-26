using System.Collections.Generic;
using Movement.Collisions;
using PlayerDan;
using UnityEngine;

namespace Movement.Core
{
    public interface IMovementController
    {
        void Move(Vector2 velocity);
        Vector2 Velocity { get; }
        Vector2 Position { get; }
        
        int Movespeed { get; set; }
        float GroundAcceleration { get; }
        float GroundDeceleration { get; }
        float AirAcceleration { get; }
        float AirDeceleration { get; }
        
        List<CollisionData> Collisions { get; }
    }
}