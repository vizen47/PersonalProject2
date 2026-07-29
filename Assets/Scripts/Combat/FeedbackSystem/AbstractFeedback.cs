using UnityEngine;

namespace Combat.FeedbackSystem
{
    public abstract class AbstractFeedback : MonoBehaviour
    {
        public abstract void CreateFeedback();
        public abstract void FinishFeedback();

        private void OnDisable()
        {
            FinishFeedback();
        }
    }
}
