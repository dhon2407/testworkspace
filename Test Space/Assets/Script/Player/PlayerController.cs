using System.Collections.Generic;
using Actions;
using Movement;
using Movement.Collisions;
using Movement.Core;
using UnityEngine;



namespace PlayerDan
{
    using CharacterController = ICharacterController<PlayerData>;
    
    public class PlayerController : MonoBehaviour, CharacterController
    {
        private IMovementController _moveController;
        private ICharacter<PlayerData> _player;
        private Vector2 _velocity;

        private bool _inputDisabled;
        private float _inputVelocityX;
        
        private bool _gravityDisabled;

        private float _velocityXSmoothing;

        private readonly float _reducer = 100f;

        public ICharacter<PlayerData> Character => _player;
        public bool DisableInputs
        {
            get => _inputDisabled;
            set
            {
                if (!_inputDisabled && value)
                    _inputVelocityX = 0;

                _inputDisabled = value;
            }
        }

        public bool DisableGravity
        {
            get => _gravityDisabled;
            set
            {
                if (!_gravityDisabled && value)
                    _velocity.y = 0;

                _gravityDisabled = value;
            }
        }

        public Vector2 CurrentVelocity => _velocity;
        public Vector2 Position => _moveController.Position;

        public float Gravity { get; set; } = -0.50f;

        public bool OnGround => Collisions.Exists(data => data.Direction == CollisionDirection.Down);
        public bool OnOverHeadCollision => Collisions.Exists(data => data.Direction == CollisionDirection.Up);
        
        public List<AvailableAction<PlayerData>> Actions => _player.Actions;
        public List<CollisionData> Collisions => _moveController.Collisions;
        
        public void UpdateVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        private void Update()
        {
            if (!DisableInputs)
            {
                foreach (var availableAction in Actions)
                    availableAction.CheckInputs(this);
            }

            UpdateInputVelocity();
        }

        private void FixedUpdate()
        {
            ApplyGravity();
            DampHorizontalVelocity();

            _moveController.Move(_velocity);

            if (OnGround || OnOverHeadCollision)
                _velocity.y = 0;
        }

        private void DampHorizontalVelocity()
        {
            var acceleration = GetHorizontalAcceleration();
            _velocity.x = Mathf.SmoothDamp(_velocity.x, _inputVelocityX, ref _velocityXSmoothing,
                acceleration);
        }

        private float GetHorizontalAcceleration()
        {
            if (OnGround)
                return _velocity.x < _inputVelocityX ? _player.GroundAcceleration : _player.GroundDeceleration;
            else
                return _velocity.x < _inputVelocityX ? _player.AirAcceleration : _player.AirDeceleration;
        }

        private void ApplyGravity()
        {
            if (_gravityDisabled) return;
            
            _velocity.y += Gravity * Time.deltaTime;
        }

        private void UpdateInputVelocity()
        {
            if (!DisableInputs)
                _inputVelocityX = Input.GetAxisRaw("Horizontal") * (_player.Movespeed / _reducer);
        }

        private void Awake()
        {
            _player = GetComponentInChildren<ICharacter<PlayerData>>();
            _moveController = _player.MoveController;
        }


    }
}