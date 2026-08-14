using Audio;
using UnityEngine;

namespace Combat.FeedbackSystem
{
    public class ExplosionFeedback : AbstractFeedback
    {
        [SerializeField] private SoundClipSO explosionSound;
        
        public override void CreateFeedback()
        {
            SoundPlay();
        }

        private void SoundPlay()
        {
            SoundManager.Instance.PlaySFXOnChannel(2, transform.position, explosionSound);
        }
        
        public override void FinishFeedback()
        {
            
        }
    }
}