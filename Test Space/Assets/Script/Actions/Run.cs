using DM2DMovement.Core;
using PlayerDan;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(fileName = "Run", menuName = "Actions/Run")]
    public class Run : CharacterAction
    {
        [SerializeField] private int speedPercentIncrease = 150;

        private bool _running;
        private int _speedBoost;
        
        public override void Execute(ICharacterController<PlayerData> characterController)
        {
            InitializeCharacter(characterController);

            if (!Character.Stats.CanRun || _running) return;
            
            var currentMovespeed = Character.Movespeed;
            _speedBoost = (int) (currentMovespeed * (speedPercentIncrease / 100f));
            
            ExecuteRun(true);
        }

        public override void Cancel(ICharacterController<PlayerData> characterController)
        {
            if (!_running) return;

            ExecuteRun(false);
        }

        public override void Hold(ICharacterController<PlayerData> characterController)
        {
            if (!Character.Stats.CanRun && _running)
                CancelRun();
            
            if (Character.Stats.CanRun && !_running)
                Execute(characterController);
        }
        
        private void ExecuteRun(bool value)
        {
            Character.Movespeed += (value ? _speedBoost : -_speedBoost);
            _running = value;
        }

        private void CancelRun()
        {
            Cancel(Controller);
        }
    }
}