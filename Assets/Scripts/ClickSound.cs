using Main_Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Global
{
    public class ClickSound : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField]
        private AudioClip soundClip;

        private AudioSource _audioEffectSource;
        
        void Start()
        {
            _audioEffectSource = AudioHandler.Instance.audioEffectSource;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            _audioEffectSource.clip = soundClip;
            _audioEffectSource.Play();
        }
    }
}