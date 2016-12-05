using System.Collections;
using UnityEngine;

namespace Assets.Scripts.LivingRoom.LivingRoomSoundtrack {
    public class LivingRoomSoundtrack : MonoBehaviour {
        private IEnumerator coroutine;
        public AudioSource DrumA;
        public AudioSource DrumB;

        public AudioSource Melody;

        public AudioClip MelodyClipA;
        public AudioClip MelodyClipB;
        public AudioSource Noise;
        public AudioSource Pad;
        public AudioClip PadA;
        public AudioClip PadB;


        private string State;

        private bool WaitForBroadcast = true;

        // Use this for initialization
        private void Start(){
            StartCoroutine(SetWaitForBroadcastCoroutine());
        }

        // Update is called once per frame
        private void Update(){
            switch (State){
                case "Start":

                    if (!WaitForBroadcast){
                        Melody.clip = MelodyClipA;
                        Pad.clip = PadA;
                        if (!Melody.isPlaying)
                            Melody.Play();
                        if (!Pad.isPlaying)
                            Pad.Play();
                    }
                    break;

                case "Clock":
                    Melody.clip = MelodyClipA;
                    Pad.clip = PadA;
                    if (!Melody.isPlaying){
                        Melody.Play();
                        DrumA.Play();
                    }
                    if (!Pad.isPlaying)
                        Pad.Play();
                    break;
                case "Stereo":
                    Melody.clip = MelodyClipA;
                    Pad.clip = PadA;
                    if (!Melody.isPlaying){
                        Melody.Play();
                        DrumA.Play();
                        DrumB.Play();
                    }
                    if (!Pad.isPlaying)
                        Pad.Play();

                    break;
                case "Light":

                    Melody.clip = MelodyClipB;
                    Pad.clip = PadB;
                    if (!Melody.isPlaying){
                        Melody.Play();
                        DrumA.Play();
                        DrumB.Play();
                        Noise.Play();
                    }
                    if (!Pad.isPlaying)
                        Pad.Play();
                    break;
            }
        }

        public IEnumerator SetWaitForBroadcastCoroutine(){
            yield return new WaitForSeconds(33);
            WaitForBroadcast = false;
        }

        public void SetState(string state){
            State = state;
        }
    }
}