using System;
using DM2DMovement.Core;
using UnityEngine;
using PlayerDan;

namespace Actions
{
    [CreateAssetMenu(fileName = "Dash Simple", menuName = "Actions/Simple Dash")]
    public class Dash : CharacterAction
    {
        public override void Execute(ICharacterController<PlayerData> characterController)
        {
            throw new NotImplementedException();
        }

        public override void Cancel(ICharacterController<PlayerData> characterController)
        {
            throw new NotImplementedException();
        }

        public override void Hold(ICharacterController<PlayerData> characterController)
        {
            throw new NotImplementedException();
        }
    }
}