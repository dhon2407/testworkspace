using PlayerDan;
using UnityEngine;

namespace Actions
{
    [System.Serializable]
    public abstract class CharacterAction : MonoBehaviour, IAction
    {
        public abstract void Execute(ICharacterController characterController);
        public abstract void Cancel(ICharacterController characterController);
    }
}