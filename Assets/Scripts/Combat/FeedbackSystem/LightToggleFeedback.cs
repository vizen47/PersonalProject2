using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Combat.FeedbackSystem
{
    public class LightToggleFeedback : AbstractFeedback
    {
        [SerializeField] private float blinkDuration = 0.1f;
        [SerializeField] private Light2D targetLight;

        private WaitForSeconds _waitForSec;
        private Coroutine _toggleCoroutine;

        private void Awake()
        {
            _waitForSec = new WaitForSeconds(blinkDuration); // 미리 만들어두면 힙메모리 절약이 된단다.
        }
        
        public override void CreateFeedback()
        {
            FinishFeedback();
            _toggleCoroutine = StartCoroutine(ToggleCoroutine());

        }

        public override void FinishFeedback()
        {
            if (_toggleCoroutine != null)
            {
                StopCoroutine(_toggleCoroutine);
            }
        }
        
        private IEnumerator ToggleCoroutine()
        {
            targetLight.enabled = true;
            yield return _waitForSec;
            targetLight.enabled = false;
        }
    }
}