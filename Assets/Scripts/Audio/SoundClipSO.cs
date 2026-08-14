using UnityEngine;

namespace Audio
{
    [CreateAssetMenu(fileName = "SoundClip", menuName = "SO/Sound", order = 0)]
    public class SoundClipSO : ScriptableObject
    {
        public enum AudioType
        {
            SFX, BGM
        }

        public AudioType audioType;
        public AudioClip clip;
        public bool loop;
        public bool randomizePitch;

        [Range(0, 1f)] public float randomizePitchRange = 0.1f;
        [Range(0, 1f)] public float volume = 1f;
        [Range(0, 3f)] public float basePitch = 1f;
    }
}