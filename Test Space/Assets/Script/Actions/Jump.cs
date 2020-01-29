﻿using DM2DMovement.Core;
using PlayerDan;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(fileName = "Jump", menuName = "Actions/Jump")]
    public class Jump : CharacterAction
    {
        [Header("Jump Parameters")]
        [SerializeField] private float maxJumpHeight = 4;
        [SerializeField] private float minJumpHeight = 1;
        [SerializeField] private float timeToJumpApex = .4f;
        
        private float _maxJumpVelocity;
        private float _minJumpVelocity;

        private const float Reducer = 100f;

        public override void Execute(ICharacterController<PlayerData> characterController)
        {
            InitializeCharacter(characterController);

            if (!Character.Stats.CanJump || !MoveController.OnGround) return;
            
            UpdateGravity(characterController);
            UpdateYVelocity(characterController, _maxJumpVelocity);
        }

        public override void Cancel(ICharacterController<PlayerData> characterController)
        {
            if (characterController.CharacterVelocity.y > _minJumpVelocity)
                UpdateYVelocity(characterController,  minJumpHeight / Reducer);
        }

        public override void Hold(ICharacterController<PlayerData> characterController)
        { }

        private void UpdateYVelocity(ICharacterController<PlayerData> characterController, float yValue)
        {
            var currentVelocity = characterController.CharacterVelocity;
            currentVelocity.y = yValue;
            characterController.SetCharacterVelocity(currentVelocity);
        }

        private void UpdateGravity(ICharacterController<PlayerData> characterController)
        {
            characterController.Gravity = (2 * (maxJumpHeight/Reducer)) / Mathf.Pow(timeToJumpApex, 2);

            _maxJumpVelocity = Mathf.Abs(characterController.Gravity) * timeToJumpApex;
            _minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(characterController.Gravity) * minJumpHeight/Reducer);
        }
    }
}