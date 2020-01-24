using UnityEngine.Events;

namespace PlayerDan
{
    public interface ICharacter
    {
        //Character Stats
        string Name { get; }
        int Movespeed { get; set; }
        float MaxTemp { get; }
        float CurrentTemp { get; }
        float MaxBacteria { get; }
        float CurrentBacteria { get; }
        float MaxFreshness { get; }
        float CurrentFreshness { get; }
        
        //Stat Functions
        void IncreaseTemp(float value);
        void DecreaseTemp(float value);
        void IncreaseBacteria(float value);
        void DecreaseBacteria(float value);

        //Stat Change Events
        UnityEvent<float> OnTempChange { get; }
        UnityEvent<float> OnBacteriaChange { get; }
        UnityEvent<float> OnFreshnessChange { get; }
        
        //Character Restrictions
        bool CanJump { get; }
        bool CanRun { get; }
    }
}