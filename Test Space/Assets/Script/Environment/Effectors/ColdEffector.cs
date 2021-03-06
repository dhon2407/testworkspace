﻿using DM2DMovement.Core;
using PlayerDan;
using UnityEngine;

namespace Environment.Effectors
{
    public class ColdEffector : MonoBehaviour, IEffector<PlayerData>
    {
        [SerializeField] private float changeValue = 1;

        public void TakeEffect(ICharacter<PlayerData> character)
        {
            character.Stats.DecreaseTemp(changeValue * Time.deltaTime);
            //TODO: Call temp change event
        }
    }
}