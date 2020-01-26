using PlayerDan;

namespace Actions
{
    public interface IAction<T> where T : ICharStats
    {
        void Execute(ICharacterController<T> characterController);
        void Cancel(ICharacterController<T> characterController);
        void Hold(ICharacterController<T> characterController);
    }
}