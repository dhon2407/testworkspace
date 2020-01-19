using PlayerDan;
using UnityEngine;

namespace Actions
{
    [System.Serializable]
    public abstract class Action : MonoBehaviour, IAction
    {
        public abstract void Execute(ICharacter character);
    }
}