using System.Collections.Generic;
using DM2DMovement.Collisions;
using UnityEngine;

namespace DM2DMovement.Core
{
    public interface ICharacterController<T> where T : ICharStats
    {
        ICharacter<T> Character { get; }
        IMovementController MoveController { get; }

        void SetCharacterVelocity(Vector2 velocity);
        
        Vector2 CharacterVelocity { get; }

        Vector2 Position { get; }
        bool DisableGravity { get; set; }
        bool DisableInputs { get; set; }

        List<AvailableAction<T>> Actions { get; }
        List<CollisionData> Collisions { get; }
    }
}