using PlayerDan;
using UnityEngine;

namespace Script
{
    public class HotEffector : MonoBehaviour, IEffector<PlayerData>
    {
        [SerializeField] private float changeValue = 1;

        public void TakeEffect(ICharacter<PlayerData> character)
        {
            character.IncreaseTemp(changeValue * Time.deltaTime);
        }
    }
}