using PlayerDan;

namespace Movement.Pushbacks
{
    public interface IMovementModifier<T> where T : ICharStats
    {
        void TakeEffect(ICharacter<T> character);
    }
}