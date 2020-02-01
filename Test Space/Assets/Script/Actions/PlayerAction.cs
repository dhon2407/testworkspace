using DM2DMovement.Core;
using PlayerDan;
using UnityEngine;

namespace Actions
{
    [System.Serializable]
    public class PlayerAction : AvailableAction<PlayerData>
    {
        [SerializeField] private KeyCode code = KeyCode.None;
        [SerializeField] private CharacterAction action = null;

        public override KeyCode Code => code;
        public override IAction<PlayerData> CharacterAction => action;
    }
}