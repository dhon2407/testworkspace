using PlayerDan;
using UnityEngine;

namespace Actions
{
    [System.Serializable]
    public abstract class CharacterAction : ScriptableObject, IAction<PlayerData>
    {
        protected ICharacterController<PlayerData> Controller;
        protected ICharacter<PlayerData> Character;
        
        public abstract void Execute(ICharacterController<PlayerData> characterController);
        public abstract void Cancel(ICharacterController<PlayerData> characterController);
        public abstract void Hold(ICharacterController<PlayerData> characterController);
        
        protected void InitializeCharacter(ICharacterController<PlayerData> characterController)
        {
            Controller = characterController;
            Character = characterController.Character;
        }
    }
}