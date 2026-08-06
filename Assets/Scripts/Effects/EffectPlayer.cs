using System.Collections;
using Systems;
using UnityEngine;

namespace Effects
{
    public class EffectPlayer : MonoBehaviour, IPoolable
    {
        [field: SerializeField] public PoolItemSO PoolItem { get; private set; }
        public GameObject GameObject => gameObject;
        
        [SerializeField] private ParticleSystem[] particle;
        private float _duration;
        private WaitForSeconds _delaySec;
        
        private void Awake()
        {
            // foreach (ParticleSystem p in particle)
            //     _duration = p.main.duration;
            _duration = 0.5f;
            foreach (ParticleSystem p in particle)
            {
                var size = p.main;
                float currentSize = size.startSize.constant;

                size.startSize = currentSize * 0.5f;
            }
            
            _delaySec = new WaitForSeconds(_duration);
        }
        
        public void SetPositionAndPlay(Vector3 position)
        {
            transform.position = position;
            foreach (ParticleSystem p in particle)
                p.Play();
            StartCoroutine(DelayAndGotoPool());
        }

        private IEnumerator DelayAndGotoPool()
        {
            yield return _delaySec;
            PoolManager.Instance.Push(this);
        }

        public void ResetItem()
        {
            foreach (ParticleSystem p in particle)
            {
                p.Stop();
                p.Simulate(0);
            }
        }
    }
}