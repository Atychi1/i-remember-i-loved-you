using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

public class MuteToggle : MonoBehaviour
{
    public AudioMixerGroup mixerGroup; // Reference to the Audio Mixer Group to mute
    public Toggle toggle; // Reference to the Toggle UI element

    void Start()
    {
        toggle.onValueChanged.AddListener(delegate { ToggleMute(); });

        // Initialize toggle state based on the current volume level
        bool isMuted = IsMuted();
        toggle.isOn = isMuted;
    }

    // Method to toggle mute/unmute of the Audio Mixer Group
    public void ToggleMute()
    {
        bool isMuted = toggle.isOn;
        float volume = isMuted ? -80f : 0f; // Mute by setting volume to -80 dB, unmute by setting it to 0 dB
        mixerGroup.audioMixer.SetFloat(mixerGroup.name + "Volume", volume);
    }

    // Method to check if the Audio Mixer Group is currently muted
    public bool IsMuted()
    {
        float volume;
        mixerGroup.audioMixer.GetFloat(mixerGroup.name + "Volume", out volume);
        return volume <= -80f; // If volume is -80 dB or lower, consider it as muted
    }
}
