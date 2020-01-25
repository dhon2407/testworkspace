using System.Collections.Generic;
using Movement.Collisions;
using UnityEngine;

namespace PlayerDan
{
    public interface ICharacterController
    {
        ICharacter Character { get; }
        bool DisableInputs { get; set; }
        Vector2 CurrentVelocity { get; }
        void UpdateVelocity(Vector2 velocity);
        float Gravity { get; set; }
        bool OnGround { get; }
        bool OnOverHeadCollision { get; }
        List<CollisionData> Collisions { get; }
        Vector2 Position { get; }
        bool DisableGravity { get; set; }
    }
}