using CoreLib;
using Players;
using UnityEngine;

namespace Systems
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public Transform playerTrm;
        public FuelSystem fuelSystem;
    }
}
