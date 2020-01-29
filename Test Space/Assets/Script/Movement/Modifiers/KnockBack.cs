using System.Collections.Generic;
using DM2DMovement.Core;
using MEC;
using PlayerDan;
using UnityEngine;

namespace Movement.Modifiers
{
    [CreateAssetMenu(fileName = "Knock Back", menuName = "Move Modifiers/Knock Back")]
    public class KnockBack : ScriptableObject, IMovementModifier<PlayerData>
    {
        [SerializeField] private Vector2 knockBackVelocity = new Vector2(0.1f,0.1f);
        [SerializeField] private int knockBackForce = 1;
        [SerializeField] private float duration =  0.3f;
        
        private ICharacter<PlayerData> _character;
        private ICharacterController<PlayerData> _controller;

        private Vector2 KnockBackVelocity => knockBackVelocity;
        
        public void TakeEffect(ICharacter<PlayerData> character)
        {
            _character = character;
            _controller = _character.Controller;
            
            Timing.RunCoroutine(DisableInputs(duration));

            var direction = Mathf.Sign(_character.Facing.x) * -1;
            var finalKnockBackVelocity = KnockBackVelocity;

            finalKnockBackVelocity.x *= direction;
            
            _controller.SetCharacterVelocity(finalKnockBackVelocity * knockBackForce);
        }

        private IEnumerator<float> DisableInputs(float time)
        {
            _controller.DisableInputs = true;
            yield return Timing.WaitForSeconds(time);
            _controller.DisableInputs = false;
        }
    }
}