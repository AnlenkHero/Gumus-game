using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuController : MonoBehaviour
{
   // [SerializeField] 
   // private AudioMixer audioMixer;

    [SerializeField] 
    private TMP_Dropdown resolutionDropdown;

    [SerializeField] 
    private Toggle fullscreenToggle;
    
    [SerializeField]
    private List<AudioSettings> audioSettingsList;

  //  [SerializeField] 
//    private Slider volumeSlider;

  //  [SerializeField] 
//    private Transform volumeLineParent;

  //  [SerializeField] 
//    private Toggle muteToggle;

    private void Awake()
    {
        fullscreenToggle.isOn = Screen.fullScreen;
    }

    void Start()
    {
        SetupResolutionDropdown();
        foreach (var audioSettings in audioSettingsList)
        {
            audioSettings.SetupVolume();
        }
      //  SetupVolume();
    }

  /*  private void SetupVolume()
    {
        if (PlayerPrefs.GetInt("muted") == 1)
        {
            muteToggle.isOn = true;
            return;
        }

        if (audioMixer.GetFloat("volume", out var volume))
        {
            volumeSlider.value = volume;
        }
    }*/

    private void SetupResolutionDropdown()
    {
        var options = GetResolutionsList();

        resolutionDropdown.AddOptions(options);

        int currentResolutionIndex;

        if (PlayerPrefs.HasKey("resolution"))
            currentResolutionIndex = PlayerPrefs.GetInt("resolution");
        else
            currentResolutionIndex = Screen.resolutions.Length - 1;

        resolutionDropdown.value = currentResolutionIndex;
            
        resolutionDropdown.RefreshShownValue();
    }

    private List<string> GetResolutionsList()
    {
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        foreach (var resolution in Screen.resolutions)
        {
            var option = resolution.width + " x " + resolution.height + " @ " + resolution.refreshRate + "hz";
            options.Add(option);
        }

        return options;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = Screen.resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        PlayerPrefs.SetInt("resolution", resolutionDropdown.value);
        PlayerPrefs.Save();
    }

/*    private void UpdateLines(int linesCount)
    {
        for (int i = 0; i < 3; i++)
        {
            volumeLineParent.GetChild(i).gameObject.SetActive(i <= linesCount - 1);
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
            
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volume", volume);

        if (muteToggle.isOn)
        {
            muteToggle.isOn = false;
        }
    }

    public void ToggleMute(bool muting)
    {
        if (muting)
        {
            volumeSlider.value = volumeSlider.minValue;
            AudioListener.volume = 0;
            UpdateLines(0);
            PlayerPrefs.SetInt("muted", 1);
            return;
        }

        volumeSlider.value = PlayerPrefs.GetFloat("volume");
        AudioListener.volume = 1;
        PlayerPrefs.SetInt("muted", 0);
    }*/

    public void ToggleFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}