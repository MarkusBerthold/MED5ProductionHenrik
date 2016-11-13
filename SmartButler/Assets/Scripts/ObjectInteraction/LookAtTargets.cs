using Assets.Characters.FirstPersonCharacter.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.Highlighting;
using UnityEngine;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.ObjectInteraction {
    public class LookAtTargets : MonoBehaviour {

        //Apply this script on the targets (Coffee Machine, Stereo)

        public float FixedDistance;
        public GameObject Cam;
        public GameObject Player;
        private bool _isLooking;
        public bool RemoteClicked;
        public Vector3 Offset;
        private float _dist;
        private bool _remoteHasBeenPickedUp; //this makes sure that the remote can't be moved around when clicking on it multiple times!

        private float _distance = 1.0f;
        private Vector3 _remoteControllerStartPosition;
        private Quaternion _remoteControllerStartRotation;

        //buttons for coffee machine
        private GameObject _coffeeButton0;
        private GameObject _coffeeButton1;
        private GameObject _coffeeButton2;
        
        //buttons for remote controller
        private GameObject _remoteKnob;
        private GameObject _remoteSlider;
        private GameObject _remoteButton0;
        private GameObject _remoteButton1;

        //speaker (that is a stero) buttons
        private GameObject _speakerButton0;
        private GameObject _speakerKnob;

		public MoveSlider _RemoteMoveSlider;
        
        // Use this for initialization
        void Start() {
            _isLooking = false;
            _remoteControllerStartPosition = this.transform.position;
            _remoteControllerStartRotation = this.transform.rotation;

            _coffeeButton0 = GameObject.Find("coffeeMachine_button");
            _coffeeButton1 = GameObject.Find("coffeeMachine_knob");
            _coffeeButton2 = GameObject.Find("coffeeMachine_slider");
            
            //_remote = GameObject.Find("Cylinder");
            _remoteKnob = GameObject.Find("lightRemote_hueManipulator");
            _remoteSlider = GameObject.Find("lightRemote_slider");
            _remoteButton0 = GameObject.Find("lightRemote_hueButton");
            _remoteButton1 = GameObject.Find("lightRemote_onoffButton");

            _speakerKnob = GameObject.Find("speaker_volumeKnob");
            _speakerButton0 = GameObject.Find("speaker_onoffButton");

            //////////////////////////////

            _coffeeButton0.GetComponent<Highlighter>().DistanceThreshold = 0;
            _coffeeButton1.GetComponent<Highlighter>().DistanceThreshold = 0;
            _coffeeButton2.GetComponent<Highlighter>().DistanceThreshold = 0;

            _remoteKnob.GetComponent<Highlighter>().DistanceThreshold = 0;
            _remoteSlider.GetComponent<Highlighter>().DistanceThreshold = 0;
            _remoteButton0.GetComponent<Highlighter>().DistanceThreshold = 0;
            _remoteButton1.GetComponent<Highlighter>().DistanceThreshold = 0;

            _speakerKnob.GetComponent<Highlighter>().DistanceThreshold = 0;
            _speakerButton0.GetComponent<Highlighter>().DistanceThreshold = 0;

        }

        // Update is called once per frame
        void Update() {
            if (_isLooking) {
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) ||
                    Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space)) {
                    _isLooking = false;
                    Player.GetComponent<FirstPersonController>().enabled = true;
                    Cursor.visible = false;
                    Cursor.lockState = CursorLockMode.Locked;

                    //this runs whenever you break off the remote, this returns the remote back to its start position
                    if (this.tag == "RemoteController") {
                        _remoteHasBeenPickedUp = false;
                        print("Tried to reset position of remote");
                        this.transform.position = _remoteControllerStartPosition;
                        this.transform.localRotation = _remoteControllerStartRotation;

                        _remoteKnob.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _remoteSlider.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _remoteButton0.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _remoteButton1.GetComponent<Highlighter>().DistanceThreshold = 0;

                        _remoteKnob.GetComponent<CapsuleCollider>().enabled = false; //this doesn't seem to do anything, i guess it disables the input given by turning the knob?
						_RemoteMoveSlider.ResetLowerandOpper();
					}

                    //Distance threshold reset so that buttons do not highlight
                    if (this.tag == "CoffeeMachine") {
                        _coffeeButton0.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _coffeeButton1.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _coffeeButton2.GetComponent<Highlighter>().DistanceThreshold = 0;

                        /*
                        _coffeeButton0.GetComponent<MeshCollider>().enabled = false;
                        _coffeeButton1.GetComponent<MeshCollider>().enabled = false;
                        _coffeeButton2.GetComponent<MeshCollider>().enabled = false;
                        */
                    }

                    if (this.tag == "Speaker") {
                        _speakerKnob.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _speakerButton0.GetComponent<Highlighter>().DistanceThreshold = 0;
						EventManager.TriggerEvent ("stereo");
                    }

                }//end inputs
                if(!Cam)
                    Cam = Cam = Camera.main.gameObject;
                Cam.transform.LookAt(transform.position + Offset);

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }//end _isLooking
        }//end update

        //Checks the players position and check if the player is interacting with a target
        void OnMouseDown() {
            if(!Player)
                Player = GameObject.FindGameObjectWithTag("Player");
            _dist = Vector3.Distance(transform.position, Player.transform.position);
            print(_dist);

            if (_dist < FixedDistance) {
                Player.GetComponent<FirstPersonController>().enabled = false;
                _isLooking = true;

                //this runs when the player picks up the remote
                if (this.tag == "RemoteController" && _remoteHasBeenPickedUp == false) {
                    print("hit RemoteController");
					EventManager.TriggerEvent ("seesremote");
                    _remoteHasBeenPickedUp = true;
                    RemoteClicked = true;
                    this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)); //???
                    this.transform.position += new Vector3(0.4f, 0, 0); //offset the remote from the player's face?

                    //this.transform.LookAt(GameObject.FindWithTag("Player").transform);
                    this.transform.LookAt(Cam.transform);
                    this.transform.Rotate(new Vector3(0f, 90f, 45f));

					Vector3 v = transform.rotation.eulerAngles;
					transform.rotation = Quaternion.Euler (v.x, v.y, 90);
                    //this.transform.LookAt(Cam.transform.position + new Vector3(this.transform.position.x, this.transform.position.y, 30f));
                    //this.transform.LookAt(Cam.transform.rotation * new Quaternion(this.transform.rotation.x, this.transform.rotation.y, 45f, 0f));
                    //this.transform.eulerAngles = new Vector3(0f, 0f, 45f); //tilts the remote a little

                    //this.transform.localRotation = new Quaternion(0f,0f,225f,1f);
                    //this.transform.position = Camera.main.transform.position + Camera.main.transform.forward * _distance;
                    //this.transform.rotation = new Quaternion(0.0f, Camera.main.transform.rotation.y, 0.0f, Camera.main.transform.rotation.w);

                    //_remoteKnob.GetComponent<CapsuleCollider>().enabled = true;

                    _remoteKnob.GetComponent<Highlighter>().DistanceThreshold = 3;
                    _remoteSlider.GetComponent<Highlighter>().DistanceThreshold = 3;
                    _remoteButton0.GetComponent<Highlighter>().DistanceThreshold = 3;
                    _remoteButton1.GetComponent<Highlighter>().DistanceThreshold = 3;
					_RemoteMoveSlider.SetLowerandOpper ();
                }

                //A distance is set so that the button will be highlighted
                if (this.tag == "CoffeeMachine") {
                    print("hit CoffeeMachine");

                    _coffeeButton0.GetComponent<Highlighter>().DistanceThreshold = 3;
                    _coffeeButton1.GetComponent<Highlighter>().DistanceThreshold = 3;
                    _coffeeButton2.GetComponent<Highlighter>().DistanceThreshold = 3;
                    /*
                    _coffeeButton0.GetComponent<MeshCollider>().enabled = true;
                    _coffeeButton1.GetComponent<MeshCollider>().enabled = true;
                    _coffeeButton2.GetComponent<MeshCollider>().enabled = true;
                    */
                }

                if (this.tag == "Speaker") {
                    _speakerKnob.GetComponent<Highlighter>().DistanceThreshold = 3;
                    _speakerButton0.GetComponent<Highlighter>().DistanceThreshold = 3;
					EventManager.TriggerEvent ("seesstereo");
                }
            }
        }//end onmousedown
    }//end class
}//end namespace