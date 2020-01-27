using System.Collections.Generic;
using System.Linq;
using Actions;
using DM2DMovement.Core;
using Script;
using UnityEngine;
using UnityEngine.Events;

namespace PlayerDan
{
    public class Player : MonoBehaviour, ICharacter<PlayerData>
    {
        [SerializeField] private int moveSpeed = 10;
        
        [Space]
        [SerializeField] private PlayerData stats;
        
        [Space]
        [SerializeField] private List<PlayerAction> availableActions = new List<PlayerAction>();

        public UnityEvent<float> OnTempChange { get; } = new StateChangeEvent();
        public UnityEvent<float> OnBacteriaChange { get; } = new StateChangeEvent();
        public UnityEvent<float> OnFreshnessChange { get; } = new StateChangeEvent();
        public bool CanJump => Stats.CurrentBacteria / Stats.MaxBacteria > 0.35f;
        public bool CanRun => Stats.CurrentBacteria / Stats.MaxBacteria > 0.72f;

        public IMovementController MoveController { get; private set; }
        public ICharacterController<PlayerData> Controller { get; private set; }

        public PlayerData Stats => stats;

        public List<AvailableAction<PlayerData>> Actions => availableActions.ToList<AvailableAction<PlayerData>>();

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
                Stats.DecreaseFreshness(Time.deltaTime * Stats.FreshnessDrain);
                OnFreshnessChange.Invoke(Stats.CurrentFreshness);
            }
        }

        private void UpdateBacteria(float tempRatio)
        {
            if (tempRatio < 0.45f)
            {
                Stats.IncreaseBacteria((1 - tempRatio) * Time.deltaTime * Stats.BacteriaGainSpeed);
                OnBacteriaChange.Invoke(Stats.CurrentBacteria);
            }

            if (tempRatio > 0.55f)
            {
                Stats.DecreaseBacteria(tempRatio * Time.deltaTime * Stats.BacteriaGainSpeed * 2.5f);
                OnBacteriaChange.Invoke(Stats.CurrentBacteria);
            }
        }

        private void TemperatureRegen(float tempRatio)
        {
            if (tempRatio > 0.5f)
            {
                Stats.DecreaseTemp(Stats.TempRegen * Time.deltaTime);
                OnTempChange.Invoke(Stats.CurrentTemp);
            }

            if (tempRatio < 0.5f)
            {
                Stats.IncreaseTemp(Stats.TempRegen * Time.deltaTime);
                OnTempChange.Invoke(Stats.CurrentTemp);
            }
        }

        private void Awake()
        {
            Controller = GetComponentInParent<ICharacterController<PlayerData>>();
            MoveController = GetComponentInChildren<IMovementController>();
            
        }
    }
}