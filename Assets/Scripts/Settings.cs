using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

// Settings class that inherits from MonoBehaviour, used for managing game settings
public class Settings : MonoBehaviour
{
    // Public AudioMixer for controlling audio
    public AudioMixer audioMixer;

    // Public TMP_Dropdown for graphics quality
    public TMP_Dropdown qualityDropdown;

    // Public Slider for controlling audio volume
    public Slider volumeSlider;

    // Constants holding keys for PlayerPrefs
    private const string QualityPrefKey = "GraphicsQuality";
    private const string VolumePrefKey = "VolumeLevel";

    // Start method is called when the script begins to execute
    void Start()
    {
        // Loads settings
        LoadSettings();
    }

    // Method for loading settings
    public void LoadSettings()
    {
        // Initializes the qualityDropdown value from PlayerPrefs
        if (PlayerPrefs.HasKey(QualityPrefKey))
        {
            int savedQuality = PlayerPrefs.GetInt(QualityPrefKey);
            qualityDropdown.SetValueWithoutNotify(savedQuality); // Sets the value without triggering an event
        }

        // Initializes the volumeSlider value from PlayerPrefs
        if (PlayerPrefs.HasKey(VolumePrefKey))
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumePrefKey);
            volumeSlider.value = savedVolume;
            SetVolume(savedVolume); // Immediately updates the volume
        }
    }

    // Method for setting the audio volume
    public void SetVolume(float volume)
    {
        // Sets the volume in the AudioMixer
        audioMixer.SetFloat("Volume", volume);

        // Saves the volume level in PlayerPrefs
        PlayerPrefs.SetFloat(VolumePrefKey, volume);
        PlayerPrefs.Save();
    }

    // Method for setting graphics quality
    public void SetQuality(int qualityIndex)
    {
        // Sets the graphics quality based on the selected index
        switch (qualityIndex)
        {
            case 0:
                QualitySettings.SetQualityLevel(5);
                break;
            case 1:
                QualitySettings.SetQualityLevel(2);
                break;
            case 2:
                QualitySettings.SetQualityLevel(0);
                break;
        }

        // Saves the selected graphics quality index in PlayerPrefs
        PlayerPrefs.SetInt(QualityPrefKey, qualityIndex);
        PlayerPrefs.Save();
    }
}
