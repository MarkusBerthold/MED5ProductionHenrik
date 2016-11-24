using UnityEngine;

namespace Assets.Scripts.LivingRoom.BroadcastSpeaker {
    public class BroadcastSpeaker : MonoBehaviour {

        [Persistent] private static bool _loaded = false;

        public AudioSource AudioSource;   
        void Start(){
            if (_loaded) {
                Destroy(AudioSource);
                Destroy(this);
            }
                AudioSource.Play();
            _loaded = true;
        }
    }
}