using DM2DMovement.Core;

namespace Movement.Modifiers
{
    public interface IMovementModifier<T> where T : ICharStats
    {
        void TakeEffect(ICharacter<T> character);
    }
}