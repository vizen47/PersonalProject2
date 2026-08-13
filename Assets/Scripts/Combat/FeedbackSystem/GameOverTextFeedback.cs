using System.Collections;
using UnityEngine;

namespace Combat.FeedbackSystem
{
    public class GameOverTextFeedback : AbstractFeedback
    {
        [SerializeField] public GameObject[] textObjects;
        
        public override void CreateFeedback()
        {
            foreach (var obj in textObjects)
            {
                obj.SetActive(false);
            }
            StartCoroutine(Feedback());
        }

        public override void FinishFeedback()
        {
            
        }

        private IEnumerator Feedback()
        {
            textObjects[0].SetActive(true);
            yield return new WaitForSeconds(1);
            textObjects[1].SetActive(true);
            yield return new WaitForSeconds(3);
            textObjects[2].SetActive(true);
        }
    }
}