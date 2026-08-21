using Agents;
using CoreLib;
using Players;
using UnityEngine;

namespace Systems
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Transform playerTrm;
        public FuelSystem fuelSystem;
        public HealthModule playerHealthModule;

        protected override void Awake()
        {
            base.Awake();
            
            playerHealthModule = playerTrm.gameObject.GetComponentInChildren<HealthModule>();
        }
    }
}
