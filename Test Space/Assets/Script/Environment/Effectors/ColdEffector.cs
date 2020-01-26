using PlayerDan;
using UnityEngine;

namespace Script
{
    public class ColdEffector : MonoBehaviour, IEffector<PlayerData>
    {
        [SerializeField] private float changeValue = 1;

        public void TakeEffect(ICharacter<PlayerData> character)
        {
            character.DecreaseTemp(changeValue * Time.deltaTime);
        }
    }
}