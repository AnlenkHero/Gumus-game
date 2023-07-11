using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Playables;

public class SubtitleTrackMixer : PlayableBehaviour
{
    private Dictionary<int, bool> isTextSetPerClip = new Dictionary<int, bool>();

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        TextMeshProUGUI text = playerData as TextMeshProUGUI;
        string currentText = "";
        float currentAlpha = 0f;
        float typeSpeed = 0f;

        if (!text) return;

        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0f)
            {
                ScriptPlayable<SubtitleBehaviour> inputPlayable =
                    (ScriptPlayable<SubtitleBehaviour>) playable.GetInput(i);
                SubtitleBehaviour input = inputPlayable.GetBehaviour();

                if (!isTextSetPerClip.ContainsKey(i) || !isTextSetPerClip[i])
                {
                    currentText = input.subtitleText;
                    currentAlpha = inputWeight;
                    typeSpeed = input.typeSpeed;
                    text.GetComponent<SubtitleTypewriter>().StartTypewriter(currentText, typeSpeed);
                    isTextSetPerClip[i] = true;
                }
            }
            else
            {
                isTextSetPerClip[i] = false;
            }
        }
    }
}
