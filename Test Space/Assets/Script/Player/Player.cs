using System.Collections.Generic;
using DM2DMovement.Core;
using Script;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerDan
{
    public class Player : MonoBehaviour, ICharacter<PlayerData>
    {
        [SerializeField] private string playerName = "Hamvurg";
        
        [Space, Header("Player Stats")]
        [SerializeField] private int moveSpeed = 10;

        [Space]
        [SerializeField] private int bacteriaGainSpeed = 10;
        [SerializeField] private float freshnessDrain = 10;
        [Space]
        [SerializeField] private float tempRegen = 10;

        [Space]
        [SerializeField] private List<AvailableAction<PlayerData>> availableActions = new List<AvailableAction<PlayerData>>();

        public UnityEvent<float> OnTempChange { get; } = new StateChangeEvent();
        public UnityEvent<float> OnBacteriaChange { get; } = new StateChangeEvent();
        public UnityEvent<float> OnFreshnessChange { get; } = new StateChangeEvent();
        public bool CanJump => Stats.CurrentBacteria / Stats.MaxBacteria > 0.35f;
        public bool CanRun => Stats.CurrentBacteria / Stats.MaxBacteria > 0.72f;

        public int Movespeed
        {
            get => moveSpeed;
            set => moveSpeed = value;
        }

        private void Update()
        {
            var tempRatio = Stats.TempRatio;
            
            TemperatureRegen(tempRatio);
            UpdateBacteria(tempRatio);
            UpdateFreshness(tempRatio);
        }

        private void UpdateFreshness(float tempRatio)
        {
            if (tempRatio >= 1 || Stats.BacteriaRatio >= 1)
            {
                Stats.DecreaseFreshness(Time.deltaTime * freshnessDrain);
                OnFreshnessChange.Invoke(Stats.CurrentFreshness);
            }
        }

        private void UpdateBacteria(float tempRatio)
        {
            if (tempRatio < 0.45f)
            {
                Stats.IncreaseBacteria((1 - tempRatio) * Time.deltaTime * bacteriaGainSpeed);
                OnBacteriaChange.Invoke(Stats.CurrentBacteria);
            }

            if (tempRatio > 0.55f)
            {
                Stats.DecreaseBacteria(tempRatio * Time.deltaTime * bacteriaGainSpeed * 2.5f);
                OnBacteriaChange.Invoke(Stats.CurrentBacteria);
            }
        }

        private void TemperatureRegen(float tempRatio)
        {
            if (tempRatio > 0.5f)
            {
                Stats.DecreaseTemp(tempRegen * Time.deltaTime);
                OnTempChange.Invoke(Stats.CurrentTemp);
            }

            if (tempRatio < 0.5f)
            {
                Stats.IncreaseTemp(tempRegen * Time.deltaTime);
                OnTempChange.Invoke(Stats.CurrentTemp);
            }
        }

        public IMovementController MoveController { get; } //TODO where to link this?
        
        public PlayerData Stats { get; } //TODO where to link this?

        public List<AvailableAction<PlayerData>> Actions => availableActions;
    }
}