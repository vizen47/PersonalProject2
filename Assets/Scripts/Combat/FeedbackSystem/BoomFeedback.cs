using System;
using Systems;
using UnityEngine;

namespace Combat.FeedbackSystem
{
    public class BoomFeedback : AbstractFeedback
    {
        [SerializeField] private PoolItemSO effectPrefab;

        public override void CreateFeedback()
        {
            GameObject effect = PoolManager.Instance.Pop(effectPrefab.ItemName).GameObject;
            Debug.Log(effect);
            effect.transform.position = transform.position;
        }

        public override void FinishFeedback()
        {
            
        }
    }
}