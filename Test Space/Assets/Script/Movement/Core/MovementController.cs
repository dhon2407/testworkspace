using System.Collections.Generic;
using Movement.Collisions;
using PlayerDan;
using UnityEngine;

namespace Movement.Core
{
    public class MovementController : MonoBehaviour, IMovementController
    {
        private const CollisionDirection Left = CollisionDirection.Left;
        private const CollisionDirection Right = CollisionDirection.Right;
        private const CollisionDirection Up = CollisionDirection.Up;
        private const CollisionDirection Down = CollisionDirection.Down;
        private ICollisionDetector _collisionDetector;

        private List<IMovementModifier> _movementModifiers;

        public void Move(Vector2 velocity)
        {
            Velocity += velocity;
            UpdateCollisions();
        }

        public Vector2 Velocity { get; private set; }
        public List<CollisionData> Collisions { get; private set; } = new List<CollisionData>();
        public ICharacter Character { get; set; }

        private void UpdateCollisions()
        {
            foreach (var movementModifier in _movementModifiers)
                Velocity = movementModifier.Apply(Velocity);
            
            if (Velocity == Vector2.zero) return;
            
            Velocity = FilterCollisions(Velocity);
            transform.Translate(Velocity);
            
            Velocity = Vector2.zero;
        }

        private Vector2 FilterCollisions(Vector2 velocity)
        {
            Collisions =_collisionDetector.GetCollisions(velocity);

            if (Mathf.Abs(velocity.x) > 0)
            {
                var direction = Mathf.Sign(velocity.x) > 0 ? Right : Left;

                if (Collisions.Exists(data => data.Direction == direction))
                    velocity.x = Mathf.Sign(velocity.x) * Collisions.Find(data => data.Direction == direction).Distance;
            }

            if (Mathf.Abs(velocity.y) > 0)
            {
                var direction = Mathf.Sign(velocity.y) > 0 ? Up : Down;
                if (Collisions.Exists(data => data.Direction == direction))
                    velocity.y = Mathf.Sign(velocity.y) * Collisions.Find(data => data.Direction == direction).Distance;
            }

            return velocity;
        }

        private void Awake()
        {
            _collisionDetector = GetComponent<ICollisionDetector>();
            _movementModifiers = new List<IMovementModifier>(GetComponents<IMovementModifier>());
            
        }
    }

    public interface IMovementModifier
    {
        Vector2 Apply(Vector2 velocity);
    }
}