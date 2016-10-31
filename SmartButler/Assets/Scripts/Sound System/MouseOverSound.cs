using UnityEngine;

namespace Assets.Scripts.Sound_System{
    public class MouseOverSound : MonoBehaviour{
        private AudioSource _audioSource;
        private bool _played;


        private float _startTime;

        public AudioClip Clip;
        public int MouseOverDelay;


        private void Awake(){
            _audioSource = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>();
        }

        private void OnMouseEnter(){
            _startTime = Time.time;
        }

        private void OnMouseOver(){
            if (!_played && (Time.time - _startTime >= MouseOverDelay)){
                _audioSource.PlayOneShot(Clip);
                _played = true;
            }
        }
    }
}