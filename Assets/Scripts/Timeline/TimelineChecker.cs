using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

public class TimelineChecker : MonoBehaviour 
{
    [SerializeField]
    private PlayableDirector director;
    [SerializeField]
    private GameObject button;
    [SerializeField]
    private GameObject confetti;

    private void Update()
    {
        if (director.state != PlayState.Playing || director.time >= 253f)
        {
            confetti.SetActive(true);
            button.SetActive(true);
        }
        else
        {
            confetti.SetActive(false);
            button.SetActive(false);
        }
    }
}
