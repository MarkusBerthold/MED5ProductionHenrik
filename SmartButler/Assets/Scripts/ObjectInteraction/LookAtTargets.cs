using Assets.Scripts.Controllers;
using Assets.Scripts.Highlighting;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
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

        private GameObject _button0;
        private GameObject _button1;
        private GameObject _button2;
        private GameObject _remote;

        // Use this for initialization
        void Start() {
            _isLooking = false;
            _remoteControllerStartPosition = this.transform.position;
            _remoteControllerStartRotation = this.transform.rotation;

            _button0 = GameObject.Find("coffeeMachine_button");
            _button1 = GameObject.Find("coffeeMachine_knob");
            _button2 = GameObject.Find("coffeeMachine_slider");
            //_remote = GameObject.Find("Cylinder");
            _remote = GameObject.Find("lightRemote_hueManipulator");

            _button0.GetComponent<Highlighter>().DistanceThreshold = 0;
            _button1.GetComponent<Highlighter>().DistanceThreshold = 0;
            _button2.GetComponent<Highlighter>().DistanceThreshold = 0;

            _button0.GetComponent<MeshCollider>().enabled = false;
            _button1.GetComponent<MeshCollider>().enabled = false;
            _button2.GetComponent<MeshCollider>().enabled = false;
            _remote.GetComponent<CapsuleCollider>().enabled = false;
            //_remote.GetComponent<MeshCollider>().enabled = false;

            Cam = Camera.main.gameObject;
            Player = GameObject.FindGameObjectWithTag("Player");
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
                        //EventManager.TriggerEvent ("remotecontrol"); //what does this do exactly? it doesn't seem to do anything, also i can't find anything named 'remotecontrol'
                        print("Tried to reset position of remote");
                        this.transform.position = _remoteControllerStartPosition;
                        this.transform.localRotation = _remoteControllerStartRotation;

                        _remote.GetComponent<CapsuleCollider>().enabled = false; //this doesn't seem to do anything, i guess it disables the input given by turning the knob?
                    }

                    //Distance threshold reset so that buttons do not highlight
                    if (this.tag == "CoffeeMachine") {
                        _button0.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _button1.GetComponent<Highlighter>().DistanceThreshold = 0;
                        _button2.GetComponent<Highlighter>().DistanceThreshold = 0;

                        _button0.GetComponent<MeshCollider>().enabled = false;
                        _button1.GetComponent<MeshCollider>().enabled = false;
                        _button2.GetComponent<MeshCollider>().enabled = false;
                    }
                }//end inputs
                Cam.transform.LookAt(transform.position + Offset);

                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }//end _isLooking
        }//end update

        //Checks the players position and check if the player is interacting with a target
        void OnMouseDown() {
            _dist = Vector3.Distance(transform.position, Player.transform.position);
            print(_dist);

            if (_dist < FixedDistance) {
                Player.GetComponent<FirstPersonController>().enabled = false;
                _isLooking = true;

                //this runs when the player picks up the remote
                if (this.tag == "RemoteController" && _remoteHasBeenPickedUp == false) {
                    print("hit RemoteController");
                    _remoteHasBeenPickedUp = true;
                    RemoteClicked = true;
                    this.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane)); //???
                    this.transform.position += new Vector3(0.2f, 0, 0); //offset the remote from the player's face?

                    this.transform.LookAt(Cam.transform);
                    this.transform.Rotate(new Vector3(0f, 90f, 45f));

                    _remote.GetComponent<CapsuleCollider>().enabled = true;
                }

                //A distance is set so that the button will be highlighted
                if (this.tag == "CoffeeMachine") {
                    print("hit CoffeeMachine");

                    _button0.GetComponent<Highlighter>().DistanceThreshold = 3;
                    _button1.GetComponent<Highlighter>().DistanceThreshold = 3;
                    _button2.GetComponent<Highlighter>().DistanceThreshold = 3;

                    _button0.GetComponent<MeshCollider>().enabled = true;
                    _button1.GetComponent<MeshCollider>().enabled = true;
                    _button2.GetComponent<MeshCollider>().enabled = true;
                }
            }
        }//end onmousedown
    }//end class
}//end namespace