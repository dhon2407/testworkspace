using System.Collections.Generic;
using DM2DMovement.Collisions;
using DM2DMovement.Core;
using UnityEngine;

namespace Movement.Core
{
    public class MovementController : MonoBehaviour, IMovementController
    {
        [SerializeField] private float groundAcceleration = 0.2f;
        [SerializeField] private float groundDeceleration = 0.1f;
        [SerializeField] private float airAcceleration = 0.3f;
        [SerializeField] private float airDeceleration = 0.3f;
        
        private const CollisionDirection Left = CollisionDirection.Left;
        private const CollisionDirection Right = CollisionDirection.Right;
        private const CollisionDirection Up = CollisionDirection.Up;
        private const CollisionDirection Down = CollisionDirection.Down;
        private const float DefaultGravity = -0.50f;
        
        private ICollisionDetector _collisionDetector;

        private List<IMovementModifier> _movementModifiers;
        
        private Vector2 _currentVelocity;
        private Vector2 _targetVelocity;

        private bool _gravityDisabled;

        private bool OnOverHeadCollision => Collisions.Exists(data => data.Direction == CollisionDirection.Up);

        public bool OnGround => Collisions.Exists(data => data.Direction == CollisionDirection.Down);
        
        private float _velocityXSmoothing;

        public void Move(Vector2 velocity)
        {
            _targetVelocity += velocity;
        }

        private void Move()
        {
            UpdateCollisions();

            transform.Translate(_targetVelocity);

            _currentVelocity = _targetVelocity;
            
            _targetVelocity.x = 0;
        }

        public Vector2 Velocity => _currentVelocity;
        public Vector2 Position => transform.position;
        
        public float Gravity { get; set; } = DefaultGravity;
        
        public float GroundAcceleration => groundAcceleration;
        public float GroundDeceleration => groundDeceleration;
        public float AirAcceleration => airAcceleration;
        public float AirDeceleration => airDeceleration;

        public bool DisableGravity
        {
            get => _gravityDisabled;
            set
            {
                if (!_gravityDisabled && value)
                    _currentVelocity.y = 0;

                _gravityDisabled = value;
            }
        }

        private void FixedUpdate()
        {
            ApplyGravity();
            DampHorizontalVelocity();

            Move();

            if (OnGround || OnOverHeadCollision)
                _targetVelocity.y = 0;
        }

        private void DampHorizontalVelocity()
        {
            var acceleration = GetHorizontalAcceleration();
            _targetVelocity.x = Mathf.SmoothDamp(_currentVelocity.x, _targetVelocity.x, ref _velocityXSmoothing,
                acceleration);
        }

        private float GetHorizontalAcceleration()
        {
            if (OnGround)
                return Velocity.x < _targetVelocity.x ? GroundAcceleration : GroundDeceleration;
            else
                return Velocity.x < _targetVelocity.x ? AirAcceleration : AirDeceleration;
        }
        
        private void ApplyGravity()
        {
            if (DisableGravity) return;
            
            _targetVelocity.y += Gravity * Time.deltaTime;
        }
        
        public List<CollisionData> Collisions { get; private set; } = new List<CollisionData>();

        private void UpdateCollisions()
        {
            foreach (var movementModifier in _movementModifiers)
                _targetVelocity = movementModifier.Apply(_targetVelocity);
            
            if (_targetVelocity == Vector2.zero) return;
            
            _targetVelocity = FilterCollisions(_targetVelocity);
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