using System.Collections.Generic;
using Actions;
using Movement;
using Movement.Collisions;
using Movement.Core;
using UnityEngine;

namespace PlayerDan
{
    public class PlayerController : MonoBehaviour, ICharacterController
    {
        [SerializeField] private List<AvailableAction> inputActions = null;
        
        private IMovementController _moveController;
        private ICharacter _player;
        private Vector2 _velocity;

        private bool _inputDisabled;
        private float _inputVelocityX;
        
        private bool _gravityDisabled;

        private float _velocityXSmoothing;

        private readonly float _reducer = 100f;

        public ICharacter Character => _player;
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
        public List<CollisionData> Collisions => _moveController.Collisions;
        
        public void UpdateVelocity(Vector2 velocity)
        {
            _velocity = velocity;
        }

        private void Update()
        {
            if (!DisableInputs)
            {
                foreach (var availableAction in inputActions)
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
            _moveController = GetComponentInChildren<IMovementController>();
            
            _player = GetComponent<ICharacter>();
            _player.SetController(this);
        }

        [System.Serializable]
        private struct AvailableAction
        {
            public KeyCode code;
            public CharacterAction characterAction;

            public AvailableAction(KeyCode keyCode)
            {
                code = KeyCode.None;
                characterAction = null;
            }

            public void CheckInputs(ICharacterController controller)
            {
                if (Input.GetKeyDown(code))
                    characterAction.Execute(controller);
                
                if (Input.GetKey(code))
                    characterAction.Hold(controller);
                
                if (Input.GetKeyUp(code))
                    characterAction.Cancel(controller);
            }
        }
    }
}