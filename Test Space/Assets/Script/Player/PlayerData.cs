using System;
using DM2DMovement.Core;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace PlayerDan
{
    [Serializable]
    public struct PlayerData : ICharStats
    {
        public string Name;
        public float MaxTemp;
        public float CurrentTemp;
        public float MaxBacteria;
        public float CurrentBacteria;
        public float MaxFreshness;
        public float CurrentFreshness;

        public int BacteriaGainSpeed;
        public float FreshnessDrain;
        public float TempRegen;
        
        //Character Restrictions
        public bool CanJump;
        public bool CanRun;
        
        public float TempRatio => CurrentTemp / MaxTemp;
        public float BacteriaRatio => CurrentTemp / MaxTemp;
        
        public void IncreaseTemp(float value)
        {
            CurrentTemp = Mathf.Clamp(CurrentTemp + Mathf.Abs(value), 0, MaxTemp);
        }

        public void DecreaseTemp(float value)
        {
            CurrentTemp = Mathf.Clamp(CurrentTemp - Mathf.Abs(value), 0, MaxTemp);
        }

        public void IncreaseBacteria(float value)
        {
            CurrentBacteria = Mathf.Clamp(CurrentBacteria + Mathf.Abs(value), 0, MaxBacteria);
        }

        public void DecreaseBacteria(float value)
        {
            CurrentBacteria = Mathf.Clamp(CurrentBacteria - Mathf.Abs(value), 0, MaxBacteria);
        }
        
        public void DecreaseFreshness(float value)
        {
            CurrentFreshness = Mathf.Clamp(CurrentFreshness - Mathf.Abs(value), 0, MaxFreshness);

            if (CurrentFreshness <= 0)
                SceneManager.LoadScene(0);
        }
    }
}