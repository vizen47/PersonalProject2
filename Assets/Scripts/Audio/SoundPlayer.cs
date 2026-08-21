using System;
using System.Collections;
using Systems.Pooling;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Audio
{
    public class SoundPlayer : MonoBehaviour, IPoolable
    {
        public Action<SoundPlayer> onClipEnd;
        
        [field: SerializeField] public PoolItemSO PoolItem { get; private set;}
        public GameObject GameObject => this != null ? gameObject : null;
        
        [SerializeField] private AudioMixerGroup bgmGroup, sfxGroup;
        
        private AudioSource _audioSource;
        
        private void Awake() => _audioSource = GetComponent<AudioSource>();

        public void PlaySound(SoundClipSO clipData)
        {
            if (clipData.audioType == SoundClipSO.AudioType.SFX)
            {
                _audioSource.outputAudioMixerGroup = sfxGroup;
            }
            else if (clipData.audioType == SoundClipSO.AudioType.BGM)
            {
                _audioSource.outputAudioMixerGroup = bgmGroup;
            }
            
            _audioSource.volume = clipData.volume;
            _audioSource.pitch = clipData.basePitch;
            if (clipData.randomizePitch)
            {
                _audioSource.pitch += Random.Range(-clipData.randomizePitchRange, clipData.randomizePitchRange);
            }
            
            _audioSource.clip = clipData.clip;
            _audioSource.loop = clipData.loop;  

            _audioSource.Play();
                
            if (!clipData.loop)
            {
                float duration = _audioSource.clip.length;
                StartCoroutine(DisableSoundTimer(duration));
            }
        }
        
        private IEnumerator DisableSoundTimer(float duration)
        {
            yield return new WaitForSeconds(duration);
            _audioSource.Stop();
            onClipEnd?.Invoke(this);
        }

        public void StopAndReturnToPool()
        {
            _audioSource.Stop();
            PoolManager.Instance.Push(this);
        }
        
        public void ResetItem()
        {
            
        }
    }
}