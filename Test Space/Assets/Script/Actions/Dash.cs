using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using PlayerDan;

namespace Actions
{
    [CreateAssetMenu(fileName = "Dash Simple", menuName = "Actions/Simple Dash")]
    public class Dash : CharacterAction
    {
        [SerializeField] private float speedMultiplier = 2f;
        [SerializeField] private float distance = 0.5f;

        [SerializeField]
        private bool _onDash;
        private Vector2 _dashVelocity;
        private Vector2 _initialPosition;

        public override void Execute(ICharacterController characterController)
        {
            if (_onDash) return;
            
            InitializeCharacter(characterController);

            Timing.RunCoroutine(ExecuteDash());
        }
        
        public override void Cancel(ICharacterController characterController)
        { }

        public override void Hold(ICharacterController characterController)
        { }

        private IEnumerator<float> ExecuteDash()
        {
            InitializeDash();
            
            Controller.DisableInputs = true;
            Controller.DisableGravity = true;

            var remainingDistance = distance;
            while (remainingDistance > 0)
            {
                if (Mathf.Abs(_dashVelocity.x) * speedMultiplier > remainingDistance)
                    _dashVelocity.x = (remainingDistance * Mathf.Sign(_dashVelocity.x) / speedMultiplier);

                remainingDistance -= Mathf.Abs(_dashVelocity.x * speedMultiplier);

                UpdateVelocity(_dashVelocity);
                yield return Timing.WaitForOneFrame;
            }
            
            Controller.DisableInputs = false;
            Controller.DisableGravity = false;

             _onDash = false;
        }

        private void InitializeDash()
        {
            _initialPosition = Character.Position;
            var direction = Mathf.Sign(Controller.CurrentVelocity.x);
            _dashVelocity = Vector2.right * direction;
            _onDash = true;
        }

        private void UpdateVelocity(Vector2 dashVelocity)
        {
            Controller.UpdateVelocity(dashVelocity * speedMultiplier);
        }

        private void Awake()
        {
            _onDash = false;
        }
    }
}