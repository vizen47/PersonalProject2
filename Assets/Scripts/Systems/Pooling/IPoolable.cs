using UnityEngine;

namespace Systems.Pooling
{
    public interface IPoolable
    {
        public PoolItemSO PoolItem { get; }
        
        public GameObject GameObject { get; }
        
        public void ResetItem();
    }
}