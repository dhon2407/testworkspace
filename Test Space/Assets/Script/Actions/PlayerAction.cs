using DM2DMovement.Core;
using PlayerDan;
using UnityEngine;

namespace Actions
{
    [System.Serializable]
    public class PlayerAction : AvailableAction<PlayerData>
    {
        [SerializeField] private KeyCode _code;
        [SerializeField] private CharacterAction _action;

        public override KeyCode Code => _code;
        public override IAction<PlayerData> CharacterAction => _action;
    }
}