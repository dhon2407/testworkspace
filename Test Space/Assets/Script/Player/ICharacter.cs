using System.Collections.Generic;
using Actions;
using Movement.Core;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerDan
{
    public interface ICharacter<T> where T : ICharStats
    {
        IMovementController MoveController { get; }
        T Stats { get; }
        List<AvailableAction<T>> Actions { get; }
    }

    public interface ICharStats
    { }
    
    [System.Serializable]
    public struct AvailableAction<T> where T : ICharStats
    {
        public KeyCode code;
        public IAction<T> characterAction;

        public AvailableAction(KeyCode keyCode)
        {
            code = KeyCode.None;
            characterAction = null;
        }

        public void CheckInputs(ICharacterController<T> controller)
        {
            if (Input.GetKeyDown(code))
                characterAction.Execute(controller);
                
            if (Input.GetKey(code))
                characterAction.Hold(controller);
                
            if (Input.GetKeyUp(code))
                characterAction.Cancel(controller);
        }
    }
}