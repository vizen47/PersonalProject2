using Agents;
using CoreLib;
using Systems.TurnSystem;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        
        #region Components

        public PlayerMovement PlayerMovement { get; private set; }
        public PlayerAimController PlayerAimController { get; private set; }
        public CheckWall[] WallCheckers {get; private set;}
        public SurfaceFollowModule SurfaceFollowModule { get; private set; }
        
        public FuelSystem FuelSystem { get; private set; }
    
        #endregion

        private void Awake()
        {
            PlayerMovement = GetComponentInChildren<PlayerMovement>();
            PlayerAimController = GetComponentInChildren<PlayerAimController>();
            SurfaceFollowModule =  GetComponentInChildren<SurfaceFollowModule>();
            WallCheckers = GetComponentsInChildren<CheckWall>();
            FuelSystem =  GetComponentInChildren<FuelSystem>();
        
            PlayerAimController.Init(this);
            SurfaceFollowModule.SetWallCheckers(WallCheckers);
        }

        private void Update()
        {
            PlayerMovement?.SetMovementInput(PlayerInput.MoveInput.x);
        }
    }
}
