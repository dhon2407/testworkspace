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
        private Vector2 _velocity;

        private bool _inputDisabled;

        private Vector2 _inputVelocity;
        
        private readonly float _reducer = 100f;

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
        public bool DisableGravity { get; set; }

        public List<AvailableAction<PlayerData>> Actions => Character.Actions;
        public List<CollisionData> Collisions => MoveController.Collisions;

        public Vector2 CharacterVelocity => MoveController.Velocity;

        public void SetCharacterVelocity(Vector2 velocity)
        {
            _inputVelocity = velocity;
        }

        private void Update()
        {
            if (!DisableInputs)
            {
                foreach (var availableAction in Actions)
                    availableAction.CheckInputs(this);
                
                UpdateInputVelocity();
            }

            if (_inputVelocity != Vector2.zero)
                MoveController.Move(_inputVelocity);
        }

        private void LateUpdate()
        {
            _inputVelocity = Vector2.zero;
        }

        private void UpdateInputVelocity()
        {
            if (!DisableInputs)
                _inputVelocity.x = Input.GetAxisRaw("Horizontal") * (Character.Movespeed / _reducer);
        }

        private void Awake()
        {
            Character = GetComponent<ICharacter<PlayerData>>();
        }

        private void Start()
        {
            MoveController = Character.MoveController;
        }
    }
}