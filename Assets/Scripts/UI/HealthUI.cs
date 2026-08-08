using System;
using Agents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField] private HealthModule health;
        
        private Slider healthSlider;
        private int healthValue;
        
        private void Awake()
        {
            healthSlider = GetComponent<Slider>();
        }
        
        public void SetHealthUI()
        {
            healthValue = health.CurrentHealth * 1 / 100;
            healthSlider.DOValue(healthValue, 1).SetEase(Ease.InOutCubic);
        }
    }
}