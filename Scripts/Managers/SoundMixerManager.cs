using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private VolumeSelector musicVolumeSelector;
    [SerializeField] private VolumeSelector soundFXVolumeSelector;
    [SerializeField] private float volumeSelectAmount;

    private void Update()
    {
        SetMusicVolume(musicVolumeSelector.currentVolume / volumeSelectAmount + 0.0001f);
        SetSoundFXVolume(soundFXVolumeSelector.currentVolume / volumeSelectAmount + 0.0001f);
    }

    public void SetSoundFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFXVolume", Mathf.Log10(level) * 20f);
    }
    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(level) * 20f);
    }
}
