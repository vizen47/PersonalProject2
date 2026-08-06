using Agents;
using UnityEngine;

namespace Effects
{
    public class GroundParticle : MonoBehaviour
    {
        [SerializeField] private AgentMovement agentMovement;
        [SerializeField] private GameObject particleObj;
        
        private void Update()
        {
            if (agentMovement.Velocity.Value != Vector2.zero)
            {
                particleObj.SetActive(true);
            }
            else
            {
                particleObj.SetActive(false);
            }
        }
    }
}
