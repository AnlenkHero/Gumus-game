using Main_Menu;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Global
{
    public class HoverSound : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public AudioClip soundClip; 
        private AudioSource _audioEffectSource;
        
        void Start()
        {
            _audioEffectSource = AudioHandler.Instance.audioEffectSource;
            _audioEffectSource.clip = soundClip;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _audioEffectSource.clip = soundClip;
            _audioEffectSource.Play();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            ;
        }
    }
}