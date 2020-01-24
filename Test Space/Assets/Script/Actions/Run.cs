using PlayerDan;
using UnityEngine;

namespace Actions
{
    [CreateAssetMenu(fileName = "Run Action", menuName = "Actions/Run")]
    public class Run : CharacterAction
    {
        [SerializeField] private int speedPercentIncrease = 200;

        private bool _running;
        
        public override void Execute(ICharacterController characterController)
        {
            if (!Character.CanRun || _running) return;
            
            InitializeCharacter(characterController);
            var currentMovespeed = Character.Movespeed;

            Character.Movespeed = (int) (currentMovespeed + (currentMovespeed * (speedPercentIncrease / 100f)));
            _running = true;
        }

        public override void Cancel(ICharacterController characterController)
        {
            if (!_running) return;
            
            var currentMovespeed = Character.Movespeed;
            Character.Movespeed = (int) (currentMovespeed - (currentMovespeed * (speedPercentIncrease / 100f)));
            _running = false;
        }
    }
}