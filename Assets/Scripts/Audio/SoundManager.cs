using System;
using System.Collections.Generic;
using CoreLib;
using Systems.Pooling;
using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    public class SoundManager : MonoSingleton<SoundManager>
    {
        [SerializeField] private PoolItemSO soundPlayerItem;
        private Dictionary<int , SoundPlayer> _playerDict = new Dictionary<int, SoundPlayer>();
        
        [Header("Simple UI Sound Controllers")]
        [field: SerializeField] public SoundClipSO HoverUI { get; private set; }
        [field: SerializeField] public SoundClipSO SelectUI { get; private set; }
        
        [Header("Mixer")]
        [SerializeField] private AudioMixer mixer;
        
        public void PlayBGM(SoundClipSO clip)
        {
            
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