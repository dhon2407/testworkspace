
namespace DM2DMovement.Collisions
{
    public class CollisionData
    {
        public CollisionDirection Direction { get; }

        public float Distance
        {
            set => UpdateDistance(value);
            get => _distance;
        }

        private void UpdateDistance(float newDistance)
        {
            if (newDistance < _distance)
                _distance = newDistance;
        }

        public CollisionData(CollisionDirection direction, float distance)
        {
            Direction = direction;
            _distance = distance;
        }

        private float _distance;
    }

    public enum CollisionDirection
    {
        Up, Down, Left, Right,
    }
}