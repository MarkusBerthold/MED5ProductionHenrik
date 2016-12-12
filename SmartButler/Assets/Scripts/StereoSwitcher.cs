using Assets.Scripts.GameManager;
using Assets.Scripts.MessageingSystem;
using Assets.Scripts.ObjectInteraction;
using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class StereoSwitcher : MonoBehaviour {
        [Persistent] private static bool _savedBroken;

        public static StereoSwitcher Instance;

        private  int _currentlyActiveRotater = 0;
        public AudioSource source;

       private ObjectRotater[] objectRotaters = new ObjectRotater[2];

        private bool _broken;
        private bool _loaded;

        public AudioClip Note;


        public static bool SavedBroken{
            get { return _savedBroken; }
            set { _savedBroken = value; }
        }

        public StereoController Controller { get; private set; }

        public bool Broken{
            get { return _broken; }
        }
		public void setBroken(bool x){
			_broken = x;
		}


        private void Awake(){
            if (Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;
        }

        private void Start(){
            source = GetComponent<AudioSource>();
            Controller = new StereoController(Note, source);
            if (_loaded) {
                _broken = SavedBroken;
            }
            else {
                _broken = true;
                _loaded = true;
            }

            int i = 0;
            foreach (ObjectRotater rotater in ObjectRotater.AllObjectRotaters){
                for (int j = 0; j < objectRotaters.Length ; j++)                       
                    if (rotater.AssociatedObject[j] == RotateableObject.Stereo & i < objectRotaters.Length) {  //find the object rotaters for the stereo
                        objectRotaters[i++] = rotater;
                        print("number of stereo rotaters:  " + j);
                    }
            }


            EventManager.StartListening(GameStateManager.State.BackFromStereo.ToString(),OnStereoFixed);

            EventManager.StartListening("StereoButton", OnStereoButton);
        }

        private void OnStereoFixed(){
            _currentlyActiveRotater = 1;
            EventManager.StopListening(GameStateManager.State.BackFromStereo.ToString(),OnStereoFixed);
        }

        private void OnStereoButton(){
            Debug.Log("StereoButton was pressed");
            Controller.FlipOnOff();
        }

        private void Update(){
            if (objectRotaters[_currentlyActiveRotater].HasNewValue){
                if (Broken){
                  //  Debug.Log("stereo rotaters angle: " + objectRotaters[_currentlyActiveRotater].CurrentAngle);
                    Controller.ChangePitch(objectRotaters[_currentlyActiveRotater].CurrentAngle);
					print ("stereo is broken");
                }
                else{
                    Controller.ChangeVolume(objectRotaters[_currentlyActiveRotater].CurrentAngle/ 360);
					print ("stereo is not broken");
                }
            }
        }
    }
}