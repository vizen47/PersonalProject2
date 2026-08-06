using Unity.Cinemachine;
using UnityEngine;

namespace Combat.FeedbackSystem
{
    public class ImpulseFeedback : AbstractFeedback
    {
        [SerializeField] private Vector3 velocity;
        [SerializeField] private CinemachineImpulseSource impulse;
        
        public override void CreateFeedback()
        {
            impulse.GenerateImpulse(velocity);
        }

        public override void FinishFeedback()
        {
            
        }
    }
}