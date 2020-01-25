using PlayerDan;

namespace Movement.Pushbacks
{
    public interface IMovementModifier
    {
        void TakeEffect(ICharacter character);
    }
}