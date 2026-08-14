using Audio;
using UnityEngine;

namespace Combat.FeedbackSystem
{
    public class FireFeedback : AbstractFeedback
    {
        [SerializeField] private SoundClipSO fireSound;
        
        public override void CreateFeedback()
        {
            PlaySound();
        }

        private void PlaySound()
        {
            SoundManager.Instance.PlaySFXOnChannel(1, transform.position, fireSound);
        }
        
        public override void FinishFeedback()
        {
            
        }
    }
}