using System;
using System.Collections.Generic;
using System.Linq;
using DM2DMovement.Collisions;
using DM2DMovement.Core;
using MEC;
using UnityEngine;
using PlayerDan;

namespace Actions
{
    [CreateAssetMenu(fileName = "Dash Simple", menuName = "Actions/Simple Dash")]
    public class Dash : CharacterAction
    {
        [SerializeField] private float coolDown = 0f;
        [Space]
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float duration = 3;
        [SerializeField] private float controlDelay = 0.2f;

        public override float Cooldown => coolDown;
        public override bool Ready => RemainingCooldown <= 0;

        public override void Execute(ICharacterController<PlayerData> characterController)
        {
            if (!Ready) return;
            
            StartCooldown();
            InitializeCharacter(characterController);
            Timing.RunCoroutine(ExecuteDash());
        }

        public override void Cancel(ICharacterController<PlayerData> characterController) { }

        public override void Hold(ICharacterController<PlayerData> characterController) { }
        
        private void StartDash()
        {
            Controller.DisableGravity = true;
            Controller.DisableInputs = true;
        }
        
        private IEnumerator<float> ExecuteDash()
        {
            StartDash();

            var dashVelocity = Character.Facing * speed;
            Controller.SetCharacterVelocity(dashVelocity);
            float timeLapse = 0;

            while (timeLapse < duration && NoCollision)
            {
                timeLapse += Time.deltaTime;
                yield return Timing.WaitForOneFrame;
            }

            if (NoCollision)
                Controller.SetCharacterVelocity(Vector2.zero);
            
            yield return Timing.WaitForSeconds(controlDelay);
            Controller.DisableGravity = false;
            Controller.DisableInputs = false;
        }

        private bool NoCollision => MoveController.Collisions.All(collisionData =>
            collisionData.Direction != CollisionDirection.Left &&
            collisionData.Direction != CollisionDirection.Right);
    }
}