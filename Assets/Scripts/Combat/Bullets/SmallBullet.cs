using System.Collections.Generic;
using Systems;
using Systems.Pooling;
using Systems.TurnSystem;

namespace Combat.Bullets
{
    public class SmallBullet : Bullet
    {
        private List<SmallBullet> _group;

        public void SetGroup(List<SmallBullet> group)
        {
            _group = group;
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