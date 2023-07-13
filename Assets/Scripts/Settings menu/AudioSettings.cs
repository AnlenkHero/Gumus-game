using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettings : MonoBehaviour
{
    [SerializeField]
    private AudioMixer audioMixer;
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Toggle muteToggle;
    [SerializeField]
    private Transform volumeLineParent;
    [SerializeField]
    private string audioMixerGroupName;

    public void SetupVolume()
    {
        if (PlayerPrefs.GetInt(audioMixerGroupName + "_muted") == 1)
        {
            muteToggle.isOn = true;
            return;
        }

        if (audioMixer.GetFloat(audioMixerGroupName +"_volume", out var volume))
        {
            volumeSlider.value = volume;
        }
    }

    public void SetVolume(float volume)
    {
        if (volume <= volumeSlider.minValue)
        {
            muteToggle.isOn = true;
            return;
        }

        float oneThird = volumeSlider.minValue / 3f;
        int linesCount = 3 - Mathf.FloorToInt(volume / oneThird);
        UpdateLines(linesCount);
        
        audioMixer.SetFloat(audioMixerGroupName +"_volume", volume);
        PlayerPrefs.SetFloat(audioMixerGroupName + "_volume", volume);

        if (muteToggle.isOn)
        {
            muteToggle.isOn = false;
        }
    }
    
    private void UpdateLines(int linesCount)
    {
        for (int i = 0; i < 3; i++)
        {
            volumeLineParent.GetChild(i).gameObject.SetActive(i <= linesCount - 1);
        }
    }

    public void ToggleMute(bool muting)
    {
        if (muting)
        {
            volumeSlider.value = volumeSlider.minValue;
            audioMixer.SetFloat(audioMixerGroupName +"_volume", -80f);
            UpdateLines(0);
            PlayerPrefs.SetInt(audioMixerGroupName + "_muted", 1);
        }
        else
        {
            float savedVolume = PlayerPrefs.GetFloat(audioMixerGroupName + "_volume");
            volumeSlider.value = savedVolume;
            audioMixer.SetFloat(audioMixerGroupName +"_volume", savedVolume);
            PlayerPrefs.SetInt(audioMixerGroupName + "_muted", 0);
        }
    }
}