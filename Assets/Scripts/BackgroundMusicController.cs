using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BackgroundMusicController : MonoBehaviour
{
    [SerializeField]
    private List<AudioClip> backgroundMusicClips;
    private int _lastClipIndex=-1;

    private void Start()
    {
        AudioHandler.Instance.musicAudioSource.loop = false;
        PlayRandomClip();
    }

    private void PlayRandomClip()
    {
        _lastClipIndex = GetRandomIndex();
        AudioHandler.Instance.musicAudioSource.clip = backgroundMusicClips[_lastClipIndex];
        AudioHandler.Instance.musicAudioSource.Play();
        StartCoroutine(CheckMusicEnd());
    }

    private IEnumerator CheckMusicEnd()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            if (!AudioHandler.Instance.musicAudioSource.isPlaying)
            {
                PlayRandomClip();
                yield break;
            }
        }
    }

    private int GetRandomIndex()
    {
        int currentIndex;
        do
        {
           currentIndex = Random.Range(0, backgroundMusicClips.Count);
        } while (currentIndex == _lastClipIndex);

        return currentIndex;
    }
}