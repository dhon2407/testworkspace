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
        [SerializeField] private float speed = 0.5f;
        [SerializeField] private float duration = 3;
        [SerializeField] private float controlDelay = 0.2f;
        
        
        public override void Execute(ICharacterController<PlayerData> characterController)
        {
            InitializeCharacter(characterController);

            Timing.RunCoroutine(ExecuteDash());
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

        public override void Cancel(ICharacterController<PlayerData> characterController)
        { }

        public override void Hold(ICharacterController<PlayerData> characterController)
        { }
        
        private void StartDash()
        {
            Controller.DisableGravity = true;
            Controller.DisableInputs = true;
        }
    }
}