using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer audioMixer; // Reference to the AudioMixer
    public Slider volumeSlider; // Reference to the UI Slider
    private const string VolumePrefKey = "GameVolume"; // Key to save the volume setting

    private void Start()
    {
        // Load the saved volume setting
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey, 0.75f); // Default volume is 0.75
        SetVolume(savedVolume);

        // Set the slider value to the saved volume
        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(SetVolume);
        }
    }

    public void SetVolume(float volume)
    {
        // Set the volume in the AudioMixer
        audioMixer.SetFloat("Volume", Mathf.Log10(volume) * 20);

        // Save the volume setting
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
    }
}