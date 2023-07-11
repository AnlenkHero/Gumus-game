using UnityEngine;

public class SetMainMenuBackgroundMusic : MonoBehaviour
{
    [SerializeField]
    private AudioClip mainMenuAudioClip;
    private void Start()
    {
        if (AudioHandler.Instance.musicAudioSource.clip == mainMenuAudioClip)
        {
            return;
        }
        AudioHandler.Instance.musicAudioSource.clip = mainMenuAudioClip;
        AudioHandler.Instance.musicAudioSource.loop = true;
        AudioHandler.Instance.musicAudioSource.Play();
    }
}
