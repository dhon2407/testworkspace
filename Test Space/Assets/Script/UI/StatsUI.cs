using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUI : MonoBehaviour
{
    [SerializeField] private Script.Player player = null;
    [Space]
    [SerializeField] private Slider temperatureSlider = null;
    [SerializeField] private Image bacteriaGauge = null;
    [SerializeField] private Image freshnessGauge = null;

    private void Start()
    {
        player.OnTempChange.AddListener(TemperatureChange);
        player.OnBacteriaChange.AddListener(BacteriaChange);
        player.OnFreshnessChange.AddListener(FreshnessChange);

    }

    private void FreshnessChange(float value)
    {
        UpdateFreshnessRatio(value / player.MaxFreshness);
    }

    private void TemperatureChange(float value)
    {
        UpdateTemperatureRatio(value / player.MaxTemp);
    }
    
    private void BacteriaChange(float value)
    {
        UpdateBacteriaRatio(value / player.MaxBacteria);
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
