using PlayerDan;
using UnityEngine;

namespace Script
{
    public class ColdEffector : MonoBehaviour, IEffector
    {
        [SerializeField] private float changeValue = 1;

        public void TakeEffect(ICharacter character)
        {
            character.DecreaseTemp(changeValue * Time.deltaTime);
        }
    }
}