    using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace UI
{
    public class FreeMovingUI : MonoBehaviour
    {
        [SerializeField] private float moveXMin;
        [SerializeField] private float moveXMax;
        [SerializeField] private float moveYMin;
        [SerializeField] private float moveYMax;
        [SerializeField] private float time;
        private RectTransform frame;

        private void Awake()
        {
            frame = GetComponent<RectTransform>();
        }

        private void Start()
        {
            frame.DOAnchorPos(frame.anchoredPosition + new Vector2(Random.Range(moveXMin, moveXMax), Random.Range(-moveYMin, moveYMax)), time)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo); // 반복
        }
    }
}