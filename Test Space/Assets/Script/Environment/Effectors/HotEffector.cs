using PlayerDan;
using UnityEngine;

namespace Script
{
    public class HotEffector : MonoBehaviour, IEffector
    {
        [SerializeField] private float changeValue = 1;

        public void TakeEffect(ICharacter character)
        {
            character.IncreaseTemp(changeValue * Time.deltaTime);
        }
    }
}