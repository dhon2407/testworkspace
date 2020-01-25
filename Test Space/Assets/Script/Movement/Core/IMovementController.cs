﻿using System.Collections.Generic;
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
        List<CollisionData> Collisions { get; }
    }
}