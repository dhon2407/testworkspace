using System.Collections.Generic;
using Actions;
using Movement.Collisions;
using UnityEngine;

namespace PlayerDan
{
    public interface ICharacterController<T> where T : ICharStats
    {
        ICharacter<T> Character { get; }
        
        bool DisableInputs { get; set; }
        Vector2 CurrentVelocity { get; }
        void UpdateVelocity(Vector2 velocity);
        float Gravity { get; set; }
        bool OnGround { get; }
        bool OnOverHeadCollision { get; }
        Vector2 Position { get; }
        bool DisableGravity { get; set; }

        List<AvailableAction<T>> Actions { get; }
        List<CollisionData> Collisions { get; }
    }
}