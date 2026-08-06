using System.Collections;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Combat.FeedbackSystem
{
    public class UseCardFeedbackUI : AbstractFeedback
    {
        [SerializeField] private Image[] _images;
        [SerializeField] private GameObject parent;
        private readonly int DissolveHash = Shader.PropertyToID("_DissolveValue");

        private readonly WaitForSeconds _dissolveWait =  new WaitForSeconds(1f);
        private CardListContainerUI cardListContainerUI;
        
        private void Awake()
        {
            foreach (var image in _images)
                image.material = new Material(image.material);
            
            cardListContainerUI = GetComponentInParent<CardListContainerUI>();
        }

        private void Dissolve()
        {
            DOTween.Kill(true);

            Sequence seq = DOTween.Sequence();

            foreach (var image in _images)
            {
                var mat = image.material;
                mat.SetFloat(DissolveHash, -0.1f);

                var dissolveTween = DOTween.To(
                    () => mat.GetFloat(DissolveHash),
                    x => mat.SetFloat(DissolveHash, x),
                    1.5f,
                    1f
                );
                seq.Join(dissolveTween);
            }
            StartCoroutine(DissolveCoroutine());
        }

        private IEnumerator DissolveCoroutine()
        {
            yield return _dissolveWait;

            parent.transform.SetParent(null); // 그냥 비활성화 해버리면 코루틴이 작동이 안 되서 부모 밖으로 빼줘야 함.
            UIManager.Instance.CheckIsHoveringCard(false);
            cardListContainerUI.ArrangeCards();
            Destroy(parent);
        }

        public override void CreateFeedback()
        {
            Dissolve();
        }

        public override void FinishFeedback()
        {
            
        }
    }
}