using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    public AudioMixerGroup mixerGroup; // Reference to the Audio Mixer Group to control
    public Slider slider; // Reference to the Slider UI element

    private const string VolumePrefsKey = "VolumeLevel";

    void Start()
    {
        if (slider == null)
        {
            Debug.LogError("VolumeSlider script attached to a GameObject without a Slider component.");
            return;
        }

        // Add listener to the slider's value change event
        slider.onValueChanged.AddListener(delegate { SetVolume(); });

        // Initialize slider value to saved volume level or default to maximum
        float savedVolume = PlayerPrefs.GetFloat(VolumePrefsKey, 1f);
        slider.value = savedVolume;

        // Set the initial volume
        SetVolume();
    }

    // Method to set the volume of the Audio Mixer Group based on the slider's value
    public void SetVolume()
    {
        float volume = slider.value;
        mixerGroup.audioMixer.SetFloat(mixerGroup.name + "Volume", volume);

        // Save the volume level
        PlayerPrefs.SetFloat(VolumePrefsKey, volume);
    }

    // Method to get the current volume level of the Audio Mixer Group
    public float GetVolume()
    {
        float volume;
        mixerGroup.audioMixer.GetFloat(mixerGroup.name + "Volume", out volume);
        return volume;
    }
}