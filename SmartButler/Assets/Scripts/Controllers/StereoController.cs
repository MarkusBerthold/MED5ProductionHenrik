using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class StereoController {
        public StereoController(AudioClip note, AudioSource source){
            _note = note;
            _source = source;
            Initialize();
        }

        [Persistent] private static bool _loaded;

        [Persistent] private static float _savedVolume;
        [Persistent] private static float _savedPitch;

        [Persistent] private static bool _savedIsOn;

        private readonly float _maxVolume = 0.4f;

        private readonly AudioClip _note;
        public readonly AudioSource _source;
        private bool _isOn;
        private float _pitch;
        private float _volume = 0.4f;

        private void Initialize(){
            Debug.Log(" _savedVolume;" + _savedVolume);
            Debug.Log(" _savedPitch;" + _savedPitch);
            Debug.Log("_savedIsOn;" + _savedIsOn);

            if (!_source.clip.Equals(_note))
                _source.clip = _note;

            if (_loaded){
                Debug.Log("Stereo controller was reloaded");
                ChangeVolume(_savedVolume);
                ChangePitch(_savedPitch);
                SetActive(_savedIsOn);
            }
            else{
                ChangeVolume(1);
                ChangePitch(90);
                _loaded = true;
            }
        }


        public void ChangeVolume(float volume){
            _volume = _maxVolume*volume;
            _savedVolume = _volume;
            _source.volume = _volume;
        }

        public void ChangePitch(float pitch){
            _pitch = pitch.Remap(90, 180, -1, 1);
            _savedPitch = _pitch;
            _source.pitch = _pitch;
        }


        public void FlipOnOff(){
            _isOn = !_isOn;
            _savedIsOn = _isOn;
            SetActive(_isOn);
        }

        private void SetActive(bool enable){
            if (enable){
                _source.Play();
                Debug.Log("playing source");
            }
            else{
                _source.Pause();
            }
        }
    }
}

public static class ExtensionMethods {
    public static float Remap(this float value, float from1, float to1, float from2, float to2){
        return (value - from1)/(to1 - from1)*(to2 - from2) + from2;
    }
}

/*
  public static StereoController Instance;

        private ObjectRotater[] objectRotaters = new ObjectRotater[2];

        public float Degrees;
        public GameStateManager GameStateManager;
        public AudioClip Note;
        public float pitch;
        private AudioSource _source;
        private float _volume;
        public float MaxVolume;
        private int _currentlyActiveRotater = 0;  


        void Awake(){
            if (Instance != null && Instance != this){
                Destroy(gameObject);
            }
            else{
                Instance = this;
            }
        }


        // Use this for initialization
        private void Start(){
            //Source = GameObject.FindGameObjectWithTag("Speaker").GetComponent<AudioSource>();
			Degrees = 90;
			Volume = 0.5f;

            int i = 0;
            foreach (ObjectRotater rotater in ObjectRotater.AllObjectRotaters) {
                if (rotater.AssociatedObject == RotateableObject.Stereo)
                    objectRotaters[i++] = rotater;
            }

            Debug.Log("found: " + (objectRotaters[0] && objectRotaters[1]));
            EventManager.StartListening("StereoButton", OnStereoButton);
        }

        private void OnStereoButton(){
            // turn on or off
        }

        // Update is called once per frame
        private void Update(){

            if (objectRotaters[_currentlyActiveRotater].HasNewValue){
                
            }
            pitch = Degrees/100;
            _source.pitch = pitch;

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
            _volume = volume*MaxVolume;
            _source.volume = _volume;
        }

        /// <summary>
        /// Starts and stops playback
        /// </summary>
        public void StartStopPlayback(bool play){
            if (play){
                _source.Play();
            }
            else{
                _source.Pause();
            }
        }

        public void MessUp(){
            if (!_source.isPlaying)
                _source.PlayScheduled(10);
        }
    }
     
     
     */