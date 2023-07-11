using UnityEngine;
using UnityEngine.Audio;

public class VolumeSettingsSetUp : MonoBehaviour
{
    [SerializeField] 
    private AudioMixer audioMixer;

    [SerializeField] 
    private float mutedVolumeValue = -80f;

    [SerializeField] 
    private float defaultVolumeValue = -30f;

    [SerializeField]
    private string[] audioMixerGroupNames;
    
    private const string VolumePrefsKey = "_volume";
    private const string MutedPrefsKey = "_muted";

    private void Start()
    {
        foreach (var audioMixerGroupName in audioMixerGroupNames)
        {
            if (PlayerPrefs.HasKey(audioMixerGroupName+VolumePrefsKey))
            {
                LoadVolumeSettingsFromPlayerPrefs(audioMixerGroupName+VolumePrefsKey);
            }
            else
                SetDefaultVolumeSettings(audioMixerGroupName+VolumePrefsKey);
        }
    }

    private void LoadVolumeSettingsFromPlayerPrefs(string param)
    {
        if (PlayerPrefs.GetInt(MutedPrefsKey) == 1)
        {
            audioMixer.SetFloat(param, mutedVolumeValue);
            return;
        }

        var volumeValue = PlayerPrefs.GetFloat(param);

        audioMixer.SetFloat(param, volumeValue);  
    }

    private void SetDefaultVolumeSettings(string param)
    {
        audioMixer.SetFloat(param, defaultVolumeValue);
    }
}