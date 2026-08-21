    using Audio;
using Febucci.UI.Core;
using UnityEngine;

namespace Systems.Tutorial
{
    public class TutorialTypingSound : MonoBehaviour
    {
        [SerializeField] private SoundClipSO typingSound;
        [SerializeField] private TypewriterCore typewriter;

        private void OnEnable()
        {
            typewriter.onCharacterVisible.AddListener(OnCharacterVisible);
        }

        private void OnDisable()
        {
            typewriter.onCharacterVisible.RemoveListener(OnCharacterVisible);
        }

        private void OnCharacterVisible(char character)
        {
            if (char.IsWhiteSpace(character)) return;
            SoundManager.Instance.PlaySFX(transform.position, typingSound);
        }
    }
}
