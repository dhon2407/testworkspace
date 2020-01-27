using DM2DMovement.Core;

namespace Environment.Effectors
{
    public interface IEffector<T> where T : ICharStats
    {
        void TakeEffect(ICharacter<T> character);
    }
}