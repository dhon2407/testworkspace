using DM2DMovement.Core;
using PlayerDan;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(fileName = "Run", menuName = "Actions/Run")]
    public class Run : CharacterAction
    {
        [SerializeField] private float coolDown = 0f;
        [Space]
        [SerializeField] private int speedPercentIncrease = 150;
        
        public override float Cooldown => coolDown;
        public override bool Ready => RemainingCooldown <= 0;

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
        
        private bool _running;
        private int _speedBoost;
        
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