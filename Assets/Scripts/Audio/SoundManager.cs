using System.Collections.Generic;
using CoreLib;
using Stages;
using Systems.Pooling;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

namespace Audio
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField] private PoolItemSO soundPlayerItem;
        private Dictionary<int , SoundPlayer> _playerDict = new Dictionary<int, SoundPlayer>();
        
        [Header("Simple UI Sound Controllers")]
        [field: SerializeField] public SoundClipSO HoverUI { get; private set; }
        [field: SerializeField] public SoundClipSO SelectUI { get; private set; }
        
        [Header("BGM Sound Controllers")]
        [field: SerializeField] public SoundClipSO[] BGM { get; private set; }
        [field: SerializeField] public SoundClipSO MainBGM { get; private set; }
        
        [Header("Mixer")]
        [SerializeField] private AudioMixer mixer;

        private void Start()
        {
            PlayBGM(transform.position, BGM);
        }
        
        public void PlayBGM(Vector3 position, SoundClipSO[] clip)
        {
            if (StageManager.Instance.CurrentStage == 0 && StageManager.Instance.currentStageNumber == 0)
            {
                SoundPlayer soundPlayer = PoolManager.Instance.Pop(soundPlayerItem.ItemName) as SoundPlayer;
                if (soundPlayer != null && MainBGM != null)
                {
                    soundPlayer.transform.position = position;
                    soundPlayer.PlaySound(MainBGM);
                    soundPlayer.onClipEnd += HandleClipEnd;
                }

                return;
            }
            
            SoundPlayer player = PoolManager.Instance.Pop(soundPlayerItem.ItemName) as SoundPlayer;
            if (player != null)
            {
                player.transform.position = position;
                player.PlaySound(clip[Random.Range(0, clip.Length)]);
                player.onClipEnd += HandleClipEnd;
            }
        }

        public void PlaySFX(Vector3 position, SoundClipSO clip)
        {
            SoundPlayer player = PoolManager.Instance.Pop(soundPlayerItem.ItemName) as SoundPlayer;
            player.transform.position = position;
            player.PlaySound(clip);
            player.onClipEnd += HandleClipEnd;
        }

        public void ClickUISFxOnChannel(int channel)
        {
            StopSFXOnChannel(channel);
            SoundPlayer player = PoolManager.Instance.Pop(soundPlayerItem.ItemName) as SoundPlayer;
            player.transform.position = Vector2.zero;
            player.PlaySound(SelectUI);
            _playerDict.Add(channel, player);
        }
        
        public void PlaySFXOnChannel(int channel, Vector3 position, SoundClipSO clip)
        {
            StopSFXOnChannel(channel);
            SoundPlayer player = PoolManager.Instance.Pop(soundPlayerItem.ItemName) as SoundPlayer;
            player.transform.position = position;
            player.PlaySound(clip);
            _playerDict.Add(channel, player);
        }
        
        public void StopSFXOnChannel(int channel)
        {
            if (_playerDict.TryGetValue(channel, out SoundPlayer player))
            {
                player.StopAndReturnToPool();
                _playerDict.Remove(channel);
            }
        }
        
        private void HandleClipEnd(SoundPlayer targetPlayer)
        {
            targetPlayer.onClipEnd -= HandleClipEnd;
            PoolManager.Instance.Push(targetPlayer);
        }
    }
}