using PlayerDan;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(fileName = "Run Action", menuName = "Actions/Run")]
    public class Run : CharacterAction
    {
        [SerializeField] private int speedPercentIncrease = 150;

        private bool _running;
        private int _speedBoost;
        
        public override void Execute(ICharacterController characterController)
        {
            InitializeCharacter(characterController);
            
            if (!Character.CanRun || _running) return;
            
            var currentMovespeed = Character.Movespeed;
            _speedBoost = (int) (currentMovespeed * (speedPercentIncrease / 100f));
            Character.Movespeed += _speedBoost;
            _running = true;
        }

        public override void Cancel(ICharacterController characterController)
        {
            if (!_running) return;

            Character.Movespeed -= _speedBoost;
            _running = false;
        }
    }
}