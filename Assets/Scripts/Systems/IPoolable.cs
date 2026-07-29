using UnityEngine;

namespace Systems
{
    public interface IPoolable
    {
        public PoolItemSO PoolItem { get; }
        
        public GameObject GameObject { get; }
        
        public void ResetItem();
    }
}