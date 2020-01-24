using System.Collections.Generic;
using Movement.Collisions;
using UnityEngine;

namespace PlayerDan
{
    public interface ICharacterController
    {
        ICharacter Character { get; }
        Vector2 CurrentVelocity { get; }
        void UpdateVelocity(Vector2 velocity);
        float Gravity { get; set; }
        bool OnGround { get; }
        bool OnOverHeadCollision { get; }
        List<CollisionData> Collisions { get; }
    }
}