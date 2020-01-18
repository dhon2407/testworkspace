using Player;

namespace Actions
{
    public interface IAction
    {
        void Execute(ICharacter character);
    }
}