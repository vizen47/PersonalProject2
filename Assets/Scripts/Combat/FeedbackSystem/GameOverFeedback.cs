using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Combat.FeedbackSystem
{
    public class GameOverFeedback : AbstractFeedback
    {
        [SerializeField] private Volume targetVolume;
        private DepthOfField effect;

        private void Start()
        {
            if (targetVolume.profile.TryGet<DepthOfField>(out var tmpEffect))
            {
                effect = tmpEffect;
            }
        }
        
        public override void CreateFeedback()
        {
            StartCoroutine(WinEffect(1));
        }

        public override void FinishFeedback()
        {
            
        }

        private IEnumerator WinEffect(float duration)
        {
            float elapsed = 0f;
            
            effect.mode.Override(DepthOfFieldMode.Gaussian);

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                float percent = elapsed / duration;

                effect.gaussianEnd.Override(Mathf.Lerp(100f, 1f, percent));
            
                effect.gaussianStart.Override(Mathf.Lerp(50f, 0f, percent));

                yield return null;
            }
        }
    }
}