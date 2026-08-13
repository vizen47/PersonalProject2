using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FuelUI : MonoBehaviour
    {
        [SerializeField] private Slider fuelSlider;
        [SerializeField] private TextMeshProUGUI fuelText;
        
        public void SetFuelAmountText(float fuelAmount, float maxFuel)
        {
            float ratio = fuelAmount / maxFuel * 100;
            
            fuelText.SetText(Mathf.Round(ratio) +"%");
        }

        public void SetFuelSlider(float fuelAmount, float maxFuel)
        {
            fuelSlider.value = fuelAmount / maxFuel;
        }
    }
}