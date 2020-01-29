using System.Collections.Generic;
using Movement.Core;
using UnityEngine;
using UnityEngine.Events;

namespace DM2DMovement.Core
{
    public interface ICharacter<T> where T : ICharStats
    {
        IMovementController MoveController { get; }
        ICharacterController<T> Controller { get; }
        
        Vector2 Facing { get; set; }
        int Movespeed { get; set; }
        T Stats { get; }
        List<AvailableAction<T>> Actions { get; }
    }
}