using System.Collections.Generic;
using Movement.Core;
using UnityEngine.Events;

namespace DM2DMovement.Core
{
    public interface ICharacter<T> where T : ICharStats
    {
        IMovementController MoveController { get; }
        ICharacterController<T> Controller { get; }

        int Movespeed { get; set; }
        T Stats { get; }
        List<AvailableAction<T>> Actions { get; }
    }
}