using Assets.Scripts.GameManager;
using Assets.Scripts.ObjectInteraction;
using UnityEngine;

namespace Assets.Scripts.Controllers{
    public class StereoController : MonoBehaviour{
        public float Degrees;
        public GameStateManager GameStateManager;
        public AudioClip Note;
        public bool Once;
        public float Pitch;
        public ObjectRotater[] Rotaters;
        public AudioSource Source;
        public bool StereoStarted;
        public float Volume;

        // Use this for initialization
        private void Start(){
            //Source = GameObject.FindGameObjectWithTag("Speaker").GetComponent<AudioSource>();
			Degrees = 90;
        }

        // Update is called once per frame
        private void Update(){
            Pitch = Degrees/100;
            Source.pitch = Pitch;
        }

        /// <summary>
        /// Updates the degrees variable
        /// Takes an int which is the degrees a cylinder is turned
        /// </summary>
        /// <param name="degrees"></param>
        public void UpdateStereoDegrees(int degrees){
            Degrees = degrees;
        }

        /// <summary>
        /// Updates the volume variable
        /// Takes an int which is the degrees a cylinder is turned
        /// </summary>
        /// <param name="degrees"></param>
        public void UpdateStereoVolume(float volume){
            Volume = volume;
        }

        /// <summary>
        /// Starts and stops playback
        /// </summary>
        public void StartStopPlayback(bool play){
            if (play){
                Source.Play();
            }
            else{
                Source.Pause();
            }
                }

		public void MessUp(){

			if (!Source.isPlaying)
				Source.PlayScheduled (10);

		}
        }
    }
