using PlayerDan;
using UnityEngine;

namespace Actions
{
    public class Jump : CharacterAction
    {
        [Header("Jump Parameters")]
        [SerializeField] private float maxJumpHeight = 4;
        [SerializeField] private float minJumpHeight = 1;
        [SerializeField] private float timeToJumpApex = .4f;
        private ICharacterController _controller;
        private ICharacter _character;
        
        private float _maxJumpVelocity;
        private float _minJumpVelocity;

        private const float Reducer = 100f;

        public override void Execute(ICharacterController characterController)
        {
            _controller = characterController;
            _character = characterController.Character;
            
            if (characterController.Character.CanJump && characterController.OnGround)
            {
                CalculateGravity();
                UpdateYVelocity(characterController, _maxJumpVelocity);
            }
        }

        public override void Cancel(ICharacterController characterController)
        {
            if (characterController.CurrentVelocity.y > _minJumpVelocity)
                UpdateYVelocity(characterController,  minJumpHeight / Reducer);
        }

        private void UpdateYVelocity(ICharacterController characterController, float yValue)
        {
            var currentVelocity = characterController.CurrentVelocity;
            currentVelocity.y = yValue;
            characterController.UpdateVelocity(currentVelocity);
        }

        private void CalculateGravity()
        {
            _controller.Gravity = -(2 * (maxJumpHeight/Reducer)) / Mathf.Pow(timeToJumpApex, 2);

            _maxJumpVelocity = Mathf.Abs(_controller.Gravity) * timeToJumpApex;
            _minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(_controller.Gravity) * minJumpHeight/Reducer);
        }
    }
}