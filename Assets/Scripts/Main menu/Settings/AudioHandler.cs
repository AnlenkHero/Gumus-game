using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public static AudioHandler Instance;
    public AudioSource musicAudioSource;
    public AudioSource audioEffectSource;
        
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
}