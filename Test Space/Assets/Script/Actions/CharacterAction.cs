using System.Collections.Generic;
using DM2DMovement.Core;
using MEC;
using PlayerDan;
using UnityEngine;

namespace Actions
{
    [System.Serializable]
    public abstract class CharacterAction : ScriptableObject, IAction<PlayerData>
    {
        public abstract bool Ready { get; }
        public abstract float Cooldown { get; }
        public abstract void Execute(ICharacterController<PlayerData> characterController);
        public abstract void Cancel(ICharacterController<PlayerData> characterController);
        public abstract void Hold(ICharacterController<PlayerData> characterController);

        protected ICharacterController<PlayerData> Controller;
        protected ICharacter<PlayerData> Character;
        protected IMovementController MoveController;

        protected float RemainingCooldown = 0; 
        
        protected void InitializeCharacter(ICharacterController<PlayerData> characterController)
        {
            Controller = characterController;
            Character = characterController.Character;
            MoveController = characterController.MoveController;
        }

        protected void StartCooldown()
        {
            RemainingCooldown = Cooldown;
            Timing.RunCoroutine(RunCooldown());
        }

        private IEnumerator<float> RunCooldown()
        {
            while (RemainingCooldown > 0)
            {
                RemainingCooldown -= Time.deltaTime;
                yield return Timing.WaitForOneFrame;
            }
        }
    }
}