using System;
using System.Collections.Generic;
using Systems;
using Systems.Pooling;
using Systems.TurnSystem;
using UnityEngine;

namespace Combat.Bullets
{
    public class SmallSplitBullet : Bullet
    {
        private List<SmallSplitBullet> _group;

        [SerializeField] private float speed; 
        public Vector3 Direction { get; set; }

        public void SetGroup(List<SmallSplitBullet> group)
        {
            _group = group;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            _rigid.gravityScale = 0f;
        }

        protected override void Fire()
        {
            
        }

        private void FixedUpdate()
        {
            _rigid.linearVelocity = Direction * speed;
        }

        protected override void DestroyBullet()
        {
            _group?.Remove(this);

            bool isLastOne = _group == null || _group.Count == 0;

            if (isLastOne && TurnManager.Instance.CurrentState.Value == TurnManager.TurnState.PlayerTurn)
            {
                TurnManager.Instance.EndPlayerTurn();
                TurnManager.Instance.StopAction();
                GameManager.Instance.fuelSystem.Init();
            }

            PoolManager.Instance.Push(this);
        }
    }
}