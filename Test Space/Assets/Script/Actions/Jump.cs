using PlayerDan;
using UnityEngine;

namespace Actions
{
    public class Jump : MonoBehaviour, IAction
    {
        [SerializeField] private float jumpHeight = 0.1f;
        
        public void Execute(ICharacter character)
        {
            Debug.Log($"Generic jump with height of {jumpHeight}");
        }
    }
}