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
        
        float Gravity { get; set; }
        bool DisableGravity { get; set; }
        bool OnGround { get; }
        
        float GroundAcceleration { get; }
        float GroundDeceleration { get; }
        float AirAcceleration { get; }
        float AirDeceleration { get; }
        
        List<CollisionData> Collisions { get; }
    }
}