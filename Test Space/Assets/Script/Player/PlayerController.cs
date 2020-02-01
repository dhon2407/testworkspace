using System;
using System.Collections.Generic;
using DM2DMovement.Collisions;
using DM2DMovement.Core;
using UnityEngine;

namespace PlayerDan
{
    using CharacterController = ICharacterController<PlayerData>;
    
    public class PlayerController : MonoBehaviour, CharacterController
    {
        [SerializeField] private float gravity;
        
        [Header("Move Acceleration")]
        [Range(0,1f), SerializeField]
        private float groundAcceleration = 0.2f;
        [Range(0,1f), SerializeField]
        private float airAcceleration = 0.3f;

        
        private Vector2 _velocity;
        private bool _inputDisabled;
        private Vector2 _inputVelocity;
        private readonly float _reducer = 100f;
        private float _velocityXSmoothing;
        private readonly float _defaultAcceleration = 0.1f;

        public ICharacter<PlayerData> Character { get; private set; }
        public IMovementController MoveController { get; private set; }

        public bool DisableInputs
        {
            get => _inputDisabled;
            set
            {
                if (!_inputDisabled && value)
                    _inputVelocity.x = 0;

                _inputDisabled = value;
            }
        }

        public Vector2 Position => MoveController.Position;
        public float Gravity
        {
            get => gravity;
            set => gravity = value;
        }

        public bool DisableGravity { get; set; }
        public bool DisableMovement { get; set; } = false;

        public List<AvailableAction<PlayerData>> Actions => Character.Actions;
        public List<CollisionData> Collisions => MoveController.Collisions;

        public Vector2 CharacterVelocity => _inputVelocity;

        public void SetCharacterVelocity(Vector2 velocity)
        {
            if (!DisableMovement)
                _inputVelocity = velocity;
        }

        private void Update()
        {
            if (!DisableInputs)
            {
                foreach (var availableAction in Actions)
                    availableAction.CheckInputs(this);
            }
            
            CalculateVelocity();
            MoveController.Move(_inputVelocity);

            if (MoveController.OnGround || Collisions.Exists(col => col.Direction == CollisionDirection.Up))
                _inputVelocity.y = 0;
        }

        private void CalculateVelocity()
        {
            if (!DisableInputs)
            {
                var targetVelocityX =
                    (DisableInputs) ? 0 : Input.GetAxisRaw("Horizontal") * (Character.Movespeed / _reducer);
                _inputVelocity.x = Mathf.SmoothDamp(_inputVelocity.x, targetVelocityX, ref _velocityXSmoothing, CurrentAcceleration);

                if (Mathf.Abs(_inputVelocity.x) > 0)
                    Character.Facing = (Mathf.Sign(_inputVelocity.x) > 0) ? Vector2.right : Vector2.left;
            }
            
            if (!DisableGravity)
                _inputVelocity.y += -gravity * Time.deltaTime;
        }

        private float CurrentAcceleration => GetAcceleration();

        private float GetAcceleration()
        {
            if (DisableInputs || DisableGravity || DisableMovement)
                return _defaultAcceleration;
            
            return MoveController.OnGround ? groundAcceleration : airAcceleration;
        }


        private void Awake()
        {
            Character =  GetComponentInChildren<ICharacter<PlayerData>>();
        }

        private void Start()
        {
            MoveController = Character.MoveController;
        }
    }
}