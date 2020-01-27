using UnityEngine;

namespace DM2DMovement.Core
{
    public abstract class AvailableAction<T> where T : ICharStats
    {
        public abstract KeyCode Code { get; }
        public abstract IAction<T> CharacterAction { get; }

        public void CheckInputs(ICharacterController<T> controller)
        {
            if (Input.GetKeyDown(Code))
                CharacterAction.Execute(controller);
                
            if (Input.GetKey(Code))
                CharacterAction.Hold(controller);
                
            if (Input.GetKeyUp(Code))
                CharacterAction.Cancel(controller);
        }
    }
}