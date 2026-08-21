using System.Collections.Generic;
using UnityEngine;

namespace Systems.Pooling
{
    public class Pool
    {
        private Stack<IPoolable> _pool; // IPoolable를 담을 곳
        private Transform _parentTrm; // 생성될 위치
        private IPoolable _poolable; // 담기는 녀석을 미리 선언
        private GameObject _prefab; // 
        
        // 생성자
        public Pool(IPoolable poolable, Transform parentTrm, int count)
        {
            _pool = new Stack<IPoolable>(count);
            _parentTrm = parentTrm;
            _poolable = poolable;
            _prefab = poolable.GameObject;

            for (int i = 0; i < count; i++)
            {
                IPoolable item = CreatePoolItem();
                _pool.Push(item);
            }
        }

        private IPoolable CreatePoolItem()
        {
            GameObject gameObject = Object.Instantiate(_prefab, _parentTrm);
            gameObject.SetActive(false);
            gameObject.name = _poolable.PoolItem.ItemName;
            return gameObject.GetComponent<IPoolable>();
        }

        public IPoolable Pop()
        {
            IPoolable item;
            if (_pool.Count <= 0)
            {
                item = CreatePoolItem();
            }
            else
            {
                item = _pool.Pop();
            }

            item.GameObject.SetActive(true);
            return item;
        }

        public void Push(IPoolable item)
        {
            item.GameObject.SetActive(false);
            _pool.Push(item);
        }
    }
}