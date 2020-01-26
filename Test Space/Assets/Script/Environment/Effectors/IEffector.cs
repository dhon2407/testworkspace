using PlayerDan;

namespace Script
{
    public interface IEffector<T> where T : ICharStats
    {
        void TakeEffect(ICharacter<T> character);
    }
}