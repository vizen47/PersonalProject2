using CoreLib;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Systems.Settings
{
    public class Settings : MonoBehaviour
    {
        [Header("AudioMixers")]
        [SerializeField] private AudioMixerGroup master;
        [SerializeField] private AudioMixerGroup bGM;
        [SerializeField] private AudioMixerGroup sFX;
        
        [Header("SerializeField Values")]
        [SerializeField] private GameObject target;
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider bGMSlider;
        [SerializeField] private Slider sFXSlider;
        
        private static readonly NotifyValue<float> masterVolume = new NotifyValue<float>(1);
        private static readonly NotifyValue<float> bGMVolume = new NotifyValue<float>(1);
        private static readonly NotifyValue<float> sFXVolume = new NotifyValue<float>(1);

        private void Start()
        {
            masterSlider.value = masterVolume.Value;
            bGMSlider.value = bGMVolume.Value;
            sFXSlider.value = sFXVolume.Value;
            
            masterVolume.OnValueChanged += HandleChangeMasterVolume;
            bGMVolume.OnValueChanged += HandleChangeBGMVolume;
            sFXVolume.OnValueChanged += HandleChangeSFXVolume;
            
            masterSlider.onValueChanged.AddListener(SetMasterVolume);
            bGMSlider.onValueChanged.AddListener(SetBGMVolume);
            sFXSlider.onValueChanged.AddListener(SetSFXVolume);
            
            SetMasterVolume(masterSlider.value);
            SetBGMVolume(bGMSlider.value);
            SetSFXVolume(sFXSlider.value);
        }

        private void OnDestroy()
        {
            masterVolume.OnValueChanged -= HandleChangeMasterVolume;
            bGMVolume.OnValueChanged -= HandleChangeBGMVolume;
            sFXVolume.OnValueChanged -= HandleChangeSFXVolume;

            masterSlider.onValueChanged.RemoveListener(SetMasterVolume);
            bGMSlider.onValueChanged.RemoveListener(SetBGMVolume);
            sFXSlider.onValueChanged.RemoveListener(SetSFXVolume);
        }
        
        private void SetMasterVolume(float value)
        {
            masterVolume.Value = value;
        }

        private void SetBGMVolume(float value)
        {
            bGMVolume.Value = value;
        }

        private void SetSFXVolume(float value)
        {
            sFXVolume.Value = value;
        }
        
        private void HandleChangeSFXVolume(float prev, float next)
        {
            float dB = Mathf.Log10(Mathf.Max(sFXVolume.Value, 0.0001f)) * 20f;
            sFX.audioMixer.SetFloat("SFXVolume", dB);
        }

        private void HandleChangeBGMVolume(float prev, float next)
        {
            float dB = Mathf.Log10(Mathf.Max(bGMVolume.Value, 0.0001f)) * 20f;
            bGM.audioMixer.SetFloat("BGMVolume", dB);
        }

        private void HandleChangeMasterVolume(float prev, float next)
        {
            float dB = Mathf.Log10(Mathf.Max(masterVolume.Value, 0.0001f)) * 20f;
            master.audioMixer.SetFloat("MasterVolume", dB);
        }
    }
}