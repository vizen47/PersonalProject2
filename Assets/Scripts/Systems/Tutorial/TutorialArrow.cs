using System;
using DG.Tweening;
using UnityEngine;

namespace Systems.Tutorial
{
    public class TutorialArrow : MonoBehaviour
    {
        [SerializeField] private Vector3 targetPos;
        private Vector3 startPos;
        private RectTransform myRect;

        private void Awake()
        {
            myRect = GetComponent<RectTransform>();
        }

        private void Start()
        {
            startPos =  myRect.anchoredPosition;
        }
        
        private void OnEnable()
        {
            AnimationStart();
        }

        private void AnimationStart()
        {
            Sequence seq =  DOTween.Sequence();
            
            seq.Append(myRect.DOAnchorPos(targetPos + startPos, 0.25f).SetEase(Ease.OutExpo));
            seq.AppendInterval(0.1f);
            seq.Append(myRect.DOAnchorPos(startPos, 0f));
            seq.SetLoops(-1);
        }
    }
}