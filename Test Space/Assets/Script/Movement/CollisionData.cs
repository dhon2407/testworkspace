using System.Collections.Generic;

namespace Movement
{
    public struct CollisionData
    {
        public CollisionDirection Direction;
    }

    public enum CollisionDirection
    {
        Up, Down, Left, Right,
    }
}