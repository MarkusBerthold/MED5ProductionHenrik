using Assets.Characters.FirstPersonCharacter.Scripts;
using Assets.Scripts.Highlighting;
using Assets.Scripts.MessageingSystem;
using UnityEngine;

namespace Assets.Scripts.ObjectInteraction {
    public class LookAtTargets : MonoBehaviour {
        private static bool StartCameraAnimation = true;

        //buttons for coffee machine
        private GameObject _coffeeButton0;
        private GameObject _coffeeButton1;
        private GameObject _coffeeButton2;
        private float _dist;

        private float _distance = 1.0f;
        private bool _isLooking;
        private GameObject _remoteButton0;
        private GameObject _remoteButton1;
        private Vector3 _remoteControllerStartPosition;
        private Quaternion _remoteControllerStartRotation;

        private bool _remoteHasBeenPickedUp;
        //this makes sure that the remote can't be moved around when clicking on it multiple times!

        //buttons for remote controller
        private GameObject _remoteKnob;

        public MoveSlider _RemoteMoveSlider;
        private GameObject _remoteSlider;

        //speaker (that is a stero) buttons
        private GameObject _speakerButton0;
        private GameObject _speakerKnob;

        public bool Activated;
        public GameObject Cam;
       // public GameObject CameraAnimation;

        //Apply this script on the targets (Coffee Machine, Stereo)

        public float FixedDistance;
        public Vector3 Offset;
        public GameObject Player;
        public bool RemoteClicked;

        // Use this for initialization
        private void Start(){
            _isLooking = false;
            _remoteControllerStartPosition = transform.position;
            _remoteControllerStartRotation = transform.rotation;

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
            if (!Player){
                Player = FindObjectOfType<FirstPersonController>().gameObject;
                Cam = Player.gameObject.GetComponentInChildren<Camera>().gameObject;
            }
            if (tag == "CoffeeMachine"){
                GetComponent<LookAtTargets>().Activated = true;
                GetComponent<Highlighter>().Activated = true;
            }
        }


        // Update is called once per frame
        private void Update(){
            if (_isLooking){
                if (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Escape) ||
                    Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) ||
                    Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.Space)){
                    _isLooking = false;
                    Player.GetComponent<FirstPersonController>().enabled = true;

                    LockCursor.DisableCursor();

                    //this runs whenever you break off the remote, this returns the remote back to its start position
                    if (tag == "RemoteController"){
                        _remoteHasBeenPickedUp = false;
                      //  print("Tried to reset position of remote");
                        transform.position = _remoteControllerStartPosition;
                        transform.localRotation = _remoteControllerStartRotation;


                        _remoteKnob.GetComponent<CapsuleCollider>().enabled = false;
                        //this doesn't seem to do anything, i guess it disables the input given by turning the knob?
                       // _RemoteMoveSlider.ResetLowerandOpper();
                    }

                    //Distance threshold reset so that buttons do not highlight
                    if (tag == "CoffeeMachine"){
                        _coffeeButton0.GetComponent<Highlighter>().Activated = false;
                        _coffeeButton1.GetComponent<Highlighter>().Activated = false;
                        _coffeeButton2.GetComponent<Highlighter>().Activated = false;
                    }

                    if (tag == "Speaker"){
                        _speakerKnob.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _speakerButton0.GetComponent<Highlighter>().DistanceThreshold = 0;
                        EventManager.TriggerEvent("stereo");
                    }
                } //end inputs

                Cam.transform.LookAt(transform.position + Offset);

                LockCursor.EnableCursor();
            } //end _isLooking

           // if (StartCameraAnimation){          we find the correct player and camera in start no need for this check
           //     CheckStartCameraAnimation();    we find the correct player and camera in start no need for this check
           // }                                   we find the correct player and camera in start no need for this check
        } //end update

      //  private void CheckStartCameraAnimation(){
      //      if (CameraAnimation == null){
      //          StartCameraAnimation = false;
      //          //if (!Cam)
      //          Cam = Camera.main.gameObject;
      //          //if (!Player) 
      //          Player = Cam.transform.parent.gameObject;
      //      }
      //  }

        //Checks the players position and check if the player is interacting with a target
        private void OnMouseDown(){
            if (Activated){
                _dist = Vector3.Distance(transform.position, Player.transform.position);
//                print(_dist);

                if (_dist < FixedDistance){
                    Player.GetComponent<FirstPersonController>().enabled = false;
                    _isLooking = true;

                    //this runs when the player picks up the remote
                    if (tag == "RemoteController" && _remoteHasBeenPickedUp == false){
                      //  print("hit RemoteController");
                        EventManager.TriggerEvent("seesremote");
                        _remoteHasBeenPickedUp = true;
                        RemoteClicked = true;
                        transform.position =
                            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,
                                Camera.main.nearClipPlane)); //???
                        transform.position += new Vector3(0.4f, 0, 0); //offset the remote from the player's face?

                        transform.LookAt(Cam.transform);
                        transform.Rotate(new Vector3(0f, 90f, 45f));

                        Vector3 v = transform.rotation.eulerAngles;
                        transform.rotation = Quaternion.Euler(v.x, v.y, 90);


                        _remoteKnob.GetComponent<Highlighter>().DistanceThreshold = 3;
                        _remoteSlider.GetComponent<Highlighter>().DistanceThreshold = 3;
                        _remoteButton0.GetComponent<Highlighter>().DistanceThreshold = 3;
                        _remoteButton1.GetComponent<Highlighter>().DistanceThreshold = 3;

                        _RemoteMoveSlider.SetLowerandOpper();
                    }

                    //A distance is set so that the button will be highlighted
                    if (tag == "CoffeeMachine"){
                       // print("hit CoffeeMachine");

                        _coffeeButton0.GetComponent<Highlighter>().DistanceThreshold = 3;
                        _coffeeButton1.GetComponent<Highlighter>().DistanceThreshold = 3;
                        _coffeeButton2.GetComponent<Highlighter>().DistanceThreshold = 3;


                        _coffeeButton0.GetComponent<Highlighter>().Activated = true;
                        _coffeeButton1.GetComponent<Highlighter>().Activated = true;
                        _coffeeButton2.GetComponent<Highlighter>().Activated = true;
                    }

                    if (tag == "Speaker"){
                        _speakerKnob.GetComponent<Highlighter>().DistanceThreshold = 3;
                        _speakerButton0.GetComponent<Highlighter>().DistanceThreshold = 3;


                        EventManager.TriggerEvent("seesstereo");
                    }
                }
            }
        } //end onmousedown
    } //end class
} //end namespace