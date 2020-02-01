using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private PlayerDan.Player player = null;
    [Space]
    [SerializeField] private Slider temperatureSlider = null;
    [SerializeField] private Image bacteriaGauge = null;
    [SerializeField] private Image freshnessGauge = null;

    [Space]
    [SerializeField] private float maxBacteriaFillAmount = 0.35f;

    [Space, Header("Indicators")]
    [SerializeField] private GameObject jumpIndicator = null;
    [SerializeField] private GameObject runIndicator = null;

    private void Start()
    {
        //TODO Null exception
//        player.OnTempChange.AddListener(TemperatureChange);
//        player.OnBacteriaChange.AddListener(BacteriaChange);
//        player.OnFreshnessChange.AddListener(FreshnessChange);

        UpdateIndicators();
    }

    private void FreshnessChange(float value)
    {
        UpdateFreshnessRatio(value / player.Stats.MaxFreshness);
    }

    private void TemperatureChange(float value)
    {
        UpdateTemperatureRatio(value / player.Stats.MaxTemp);
    }
    
    private void BacteriaChange(float value)
    {
        var newFillRatio = Mathf.Lerp(0, maxBacteriaFillAmount, value / player.Stats.MaxBacteria);
        
        UpdateBacteriaRatio(newFillRatio);
        UpdateIndicators();
    }

    private void UpdateIndicators()
    {
        jumpIndicator.SetActive(player.CanJump);
        runIndicator.SetActive(player.CanRun);
    }

    private void UpdateTemperatureRatio(float value)
    {
        temperatureSlider.value = value;
    }
    
    private void UpdateBacteriaRatio(float value)
    {
        bacteriaGauge.fillAmount = value;
    }
    
    private void UpdateFreshnessRatio(float value)
    {
        freshnessGauge.fillAmount = value;
    }
}
