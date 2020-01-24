using PlayerDan;

namespace Actions
{
    public interface IAction
    {
        void Execute(ICharacterController characterController);
        void Cancel(ICharacterController characterController);
        void Hold(ICharacterController characterController);
    }
}