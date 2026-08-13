using UI;
using UnityEngine;

namespace Players
{
    public class FuelSystem : MonoBehaviour
    {
        [field: SerializeField] public float CurrentFuel { get; private set; }
        [SerializeField] private PlayerController playerController;
        [SerializeField] private FuelUI fuelUI;
        [SerializeField] private float maxFuel = 7.5f;
        [SerializeField] private float fuelUseAmount = 1f;
        [SerializeField] private float moveAmount = 0.1f;
        
        private PlayerMovement playerMovement;
        
        private void Start()
        {            
            playerMovement = playerController.PlayerMovement;
            Init();
        }

        public void Init()
        {
            CurrentFuel = maxFuel;
            fuelUI.SetFuelAmountText(CurrentFuel, maxFuel);
            fuelUI.SetFuelSlider(CurrentFuel, maxFuel);
        }
        
        private void Update()
        {
            bool isMoving = playerMovement.Velocity.Value.magnitude > moveAmount;

            if (isMoving && CurrentFuel > 0f)
            {
                CurrentFuel -= fuelUseAmount * Time.deltaTime;
                CurrentFuel = Mathf.Clamp(CurrentFuel, 0f, maxFuel);

                fuelUI.SetFuelAmountText(CurrentFuel, maxFuel);
                fuelUI.SetFuelSlider(CurrentFuel, maxFuel);
            }

            if (CurrentFuel <= 0f)
                playerMovement.FalseCanMove();
        }
    }
}