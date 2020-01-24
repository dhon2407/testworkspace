using System;
using System.Data;
using PlayerDan;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Script
{
    public class Player : MonoBehaviour, ICharacter
    {
        [SerializeField] private string playerName = "Hamvurg";
        
        [Space, Header("Player Stats")]
        [SerializeField] private int moveSpeed = 10;

        [Space]
        [SerializeField] private float maxFreshness = 100;
        [SerializeField] private float maxTemp = 100;
        [SerializeField] private float maxBacteria = 100;
        
        [Space]
        [SerializeField] private float currentFreshness = 100;
        [SerializeField] private float currentTemp;
        [SerializeField] private float currentBacteria = 0;
        
        [Space]
        [SerializeField] private int bacteriaGainSpeed = 10;
        [SerializeField] private float freshnessDrain = 10;
        [Space]
        [SerializeField] private float tempRegen = 10;

        public string Name => playerName;
        public float MaxTemp => maxTemp;
        public float CurrentTemp => currentTemp;
        public float MaxBacteria => maxBacteria;
        public float CurrentBacteria => currentBacteria;
        public float MaxFreshness => maxFreshness;
        public float CurrentFreshness => currentFreshness;

        public void IncreaseTemp(float value)
        {
            currentTemp = Mathf.Clamp(currentTemp + Mathf.Abs(value), 0, maxTemp);
            OnTempChange.Invoke(currentTemp);
        }

        public void DecreaseTemp(float value)
        {
            currentTemp = Mathf.Clamp(currentTemp - Mathf.Abs(value), 0, maxTemp);
            OnTempChange.Invoke(currentTemp);
        }

        public void IncreaseBacteria(float value)
        {
            currentBacteria = Mathf.Clamp(currentBacteria + Mathf.Abs(value), 0, maxBacteria);
            OnBacteriaChange.Invoke(currentBacteria);
        }

        public void DecreaseBacteria(float value)
        {
            currentBacteria = Mathf.Clamp(currentBacteria - Mathf.Abs(value), 0, maxBacteria);
            OnBacteriaChange.Invoke(currentBacteria);
        }

        public UnityEvent<float> OnTempChange { get; } = new StateChangeEvent();
        public UnityEvent<float> OnBacteriaChange { get; } = new StateChangeEvent();
        public UnityEvent<float> OnFreshnessChange { get; } = new StateChangeEvent();
        public bool CanJump => CurrentBacteria / maxBacteria > 0.35f;
        public bool CanRun => CurrentBacteria / maxBacteria > 0.72f;

        public int Movespeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        private void Update()
        {
            var tempRatio = currentTemp / maxTemp;
            
            TemperatureRegen(tempRatio);
            UpdateBacteria(tempRatio);
            UpdateFreshness(tempRatio);
        }

        private void UpdateFreshness(float tempRatio)
        {
            if (tempRatio >= 1)
                DecreaseFreshness(Time.deltaTime * freshnessDrain);
            if (CurrentBacteria / maxBacteria >= 1)
                DecreaseFreshness(Time.deltaTime * freshnessDrain);
        }

        private void UpdateBacteria(float tempRatio)
        {
            if (tempRatio < 0.45f)
                IncreaseBacteria((1 - tempRatio) * Time.deltaTime * bacteriaGainSpeed);
            if (tempRatio > 0.55f)
                DecreaseBacteria(tempRatio * Time.deltaTime * bacteriaGainSpeed * 2.5f);
        }

        private void TemperatureRegen(float tempRatio)
        {
            if (tempRatio > 0.5f)
                DecreaseTemp(tempRegen * Time.deltaTime);
            if (tempRatio < 0.5f)
                IncreaseTemp(tempRegen * Time.deltaTime);
        }

        private void DecreaseFreshness(float value)
        {
            currentFreshness = Mathf.Clamp(currentFreshness - Mathf.Abs(value), 0, maxFreshness);
            OnFreshnessChange.Invoke(currentFreshness);

            if (currentFreshness <= 0)
                SceneManager.LoadScene(0);
        }
    }

    public class StateChangeEvent : UnityEvent<float> { }
}