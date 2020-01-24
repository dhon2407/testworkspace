using PlayerDan;
using UnityEngine;

namespace Actions
{
    [System.Serializable]
    public abstract class CharacterAction : ScriptableObject, IAction
    {
        protected ICharacterController Controller;
        protected ICharacter Character;
        
        public abstract void Execute(ICharacterController characterController);
        public abstract void Cancel(ICharacterController characterController);
        
        protected void InitializeCharacter(ICharacterController characterController)
        {
            Controller = characterController;
            Character = characterController.Character;
        }
    }
}