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

        // Use this for initialization
        private void Start(){
            Source = GameObject.FindGameObjectWithTag("Speaker").GetComponent<AudioSource>();
            Source.Play();
            Once = true;
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
        /// Allows for controlling of the stereo object
        /// DEPRECATED
        /// </summary>
        public void ControlStereo(){
            foreach (var objectRotater in Rotaters)
                if (objectRotater.IsActive && objectRotater.IsRotating){
                    //StereoStarted = true;
                }
        }
    }
}