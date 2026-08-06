using Agents;
using CoreLib;
using UnityEngine;

namespace Players
{
    public class PlayerController : MonoBehaviour
    {
        [field: SerializeField] public PlayerInputSO PlayerInput { get; private set; }
        
        #region Components

        public AgentMovement AgentMovement { get; private set; }
        public AgentAim AgentAim { get; private set; }
        public CheckWall[] WallCheckers {get; private set;}
        public SurfaceFollowModule SurfaceFollowModule { get; private set; }
    
        #endregion

        private void Awake()
        {
            AgentMovement = GetComponentInChildren<AgentMovement>();
            AgentAim = GetComponentInChildren<AgentAim>();
            SurfaceFollowModule =  GetComponentInChildren<SurfaceFollowModule>();
            WallCheckers = GetComponentsInChildren<CheckWall>();
        
            AgentAim.Init(this);
            SurfaceFollowModule.SetWallCheckers(WallCheckers);
        }
    
        private void Update()
        {
            AgentMovement?.SetMovementInput(PlayerInput.MoveInput.x);
        }
    }
}
