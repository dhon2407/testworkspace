using System.Collections.Generic;
using MEC;
using PlayerDan;
using UnityEngine;

namespace Movement.Pushbacks
{
    [CreateAssetMenu(fileName = "Knock Back", menuName = "Move Modifiers/Knock Back")]
    public class KnockBack : ScriptableObject, IMovementModifier
    {
        [SerializeField] private Vector2 knockBackVelocity = new Vector2(0.2f,0.1f);
        [SerializeField] private int knockBackForce = 1;
        
        private ICharacter _character;
        private ICharacterController _controller;

        private Vector2 KnockBackVelocity => knockBackVelocity;
        
        public void TakeEffect(ICharacter character)
        {
            _character = character;
            _controller = character.Controller;
            
            Timing.RunCoroutine(PlayerDisabled());

            var direction = Mathf.Sign(_controller.CurrentVelocity.x) * -1;
            var finalKnockBackVelocity = KnockBackVelocity;

            finalKnockBackVelocity.x *= direction;
            
            _controller.UpdateVelocity(finalKnockBackVelocity * knockBackForce);
        }

        private IEnumerator<float> PlayerDisabled()
        {
            _controller.DisableInputs = true;
            yield return Timing.WaitForSeconds(0.5f);
            _controller.DisableInputs = false;
        }
    }
}