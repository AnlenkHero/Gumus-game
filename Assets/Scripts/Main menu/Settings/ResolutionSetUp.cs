using UnityEngine;

namespace Main_Menu.SettingsSetUp
{
    public class ResolutionSetUp : MonoBehaviour
    {
        private const string RESOLUTION_PREFS_KEY = "resolution";
        void Awake()
        {
            if (PlayerPrefs.HasKey(RESOLUTION_PREFS_KEY))
                LoadResolutionSettingsFromPlayerPrefs();
            else
                SetDefaultResolution();
        }

        private void LoadResolutionSettingsFromPlayerPrefs()
        {
            var savedResolutionIndex = PlayerPrefs.GetInt(RESOLUTION_PREFS_KEY);
            SetResolutionFromIndex(savedResolutionIndex);
        }

        private void SetDefaultResolution()
        {
            SetResolutionFromIndex(Screen.resolutions.Length-1);
        }

        private void SetResolutionFromIndex(int index)
        {
            var resolution = Screen.resolutions[index];
            SetResolution(resolution);
        }

        private void SetResolution(Resolution resolution)
        {
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        }
    }
}
