using System.Collections.Generic;
using DM2DMovement.Collisions;
using DM2DMovement.Core;
using UnityEngine;

namespace Movement.Core
{
    public class MovementController : MonoBehaviour, IMovementController
    {
        public Vector2 Velocity => _velocity;
        public Vector2 Position => transform.position;
        public List<CollisionData> Collisions { get; private set; } = new List<CollisionData>();

        public bool OnGround => Collisions.Exists(data => data.Direction == CollisionDirection.Down);

        public void Move(Vector2 velocity)
        {
            _velocity = velocity;
            UpdateCollisions();
        }

        private void FixedUpdate()
        {
            transform.Translate(_velocity);
        }

        private const CollisionDirection Left = CollisionDirection.Left;
        private const CollisionDirection Right = CollisionDirection.Right;
        private const CollisionDirection Up = CollisionDirection.Up;
        private const CollisionDirection Down = CollisionDirection.Down;
        
        private ICollisionDetector _collisionDetector;
        private List<IMovementModifier> _movementModifiers;
        private Vector2 _velocity;

        private void UpdateCollisions()
        {
            foreach (var movementModifier in _movementModifiers)
                _velocity = movementModifier.Apply(_velocity);
            
            if (_velocity == Vector2.zero) return;
            
            FilterCollisions();
        }

        private void FilterCollisions()
        {
            Collisions =_collisionDetector.GetCollisions(_velocity);
            
            if (Mathf.Abs(_velocity.x) > 0)
            {
                var direction = Mathf.Sign(_velocity.x) > 0 ? Right : Left;

                if (Collisions.Exists(data => data.Direction == direction))
                    _velocity.x = Mathf.Sign(_velocity.x) * Collisions.Find(data => data.Direction == direction).Distance;
            }

            if (Mathf.Abs(_velocity.y) > 0)
            {
                var direction = Mathf.Sign(_velocity.y) > 0 ? Up : Down;
                if (Collisions.Exists(data => data.Direction == direction))
                    _velocity.y = Mathf.Sign(_velocity.y) * Collisions.Find(data => data.Direction == direction).Distance;
            }
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