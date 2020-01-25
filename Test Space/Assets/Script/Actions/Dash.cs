using System;
using System.Collections.Generic;
using MEC;
using UnityEngine;
using PlayerDan;

namespace Actions
{
    [CreateAssetMenu(fileName = "Dash Simple", menuName = "Actions/Simple Dash")]
    public class Dash : CharacterAction
    {
        public override void Execute(ICharacterController characterController)
        {
            throw new NotImplementedException();
        }

        public override void Cancel(ICharacterController characterController)
        {
            throw new NotImplementedException();
        }

        public override void Hold(ICharacterController characterController)
        {
            throw new NotImplementedException();
        }
    }
}