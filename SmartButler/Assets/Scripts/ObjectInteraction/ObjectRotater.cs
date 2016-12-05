using System.Collections.Generic;
using Assets.Scripts.GameManager;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ObjectInteraction {
    public enum Axis {
        X,
        Y,
        Z
    }

    public enum RotateableObject {
        Stereo,
        Light,
        Clock,
        None
    }

    public class ObjectRotater : MonoBehaviour {
        private static readonly List<ObjectRotater> allObjectRotaters = new List<ObjectRotater>();
        private float _currentAngle;

        private Vector3 _mouseOffset;
        private Vector3 _mouseReference;
        private Vector3 _rotationalAxis;

        public RotateableObject[] AssociatedObject = new RotateableObject[2];
        //the associated objects that this rotater interacts with

        public Axis RotationAxisChoice;
        public float Sensitivity;


        public float CurrentAngle{
            get{
                HasNewValue = false;
                switch (RotationAxisChoice){
                    case Axis.X:
                        return _currentAngle = transform.rotation.eulerAngles.x;
                    case Axis.Y:
                        return _currentAngle = transform.rotation.eulerAngles.y;
                    case Axis.Z:
                        return _currentAngle = transform.rotation.eulerAngles.z;
                }
                return 0;
            }
            set { _currentAngle = value; }
        }


        private Vector3 RotationalAxis{
            get{
                switch (RotationAxisChoice){
                    case Axis.X:
                        return transform.right;
                    case Axis.Y:
                        return transform.up;
                    case Axis.Z:
                        return transform.forward;
                }
                return Vector3.zero;
            }
        }


        public RotateableObject[] Object{
            get { return AssociatedObject; }
        }

        public static List<ObjectRotater> AllObjectRotaters{
            get { return allObjectRotaters; }
        }

        public bool IsRotating { get; private set; }

        public bool HasNewValue { get; private set; }

        private void Awake(){
            allObjectRotaters.Add(this);
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
        }

        private void OnLevelFinishedLoading(Scene newScene, LoadSceneMode mode){
            if (!newScene.name.Equals(SceneManager.GetSceneByName(SceneLoader.Scene.LivingRoom.ToString()).name))
                AllObjectRotaters.Clear();
        }

        private void Start(){
            _rotationalAxis = RotationalAxis;
        }

        private void OnMouseDrag(){
            Vector3 objScreenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mouseVector = Input.mousePosition;
            if (objScreenPoint.x > mouseVector.x){
                transform.Rotate(_rotationalAxis, -Sensitivity);
            }
            else if (objScreenPoint.x < mouseVector.x){
                transform.Rotate(_rotationalAxis, Sensitivity);
            }
            HasNewValue = true;
        }


        private void OnMouseUp(){
            IsRotating = false;
        }
    }
}

/*
        public float CurrentAngle;
        private Vector3 _rotation;

        [Persistent]
        private float _angleFromLastScene;
        [Persistent]
        private Vector3 _rotationFromLastScene;


        private Vector3 _mouseOffset;
        private Vector3 _mouseReference;
        private float Sensitivity = 0.4f;
        public Axis RotationalAxis;
        public LightSwitcher LightSwitcher;
        public StereoController StereoController;
        public GearRotationYAxis GearRotationYAxis;
        public gearRotationZaxis GearRotationZaxis;
        public GearRotationYAxis[] gearsY;
        public gearRotationZaxis[] gearsZ;

        public enum Axis {
            X,
            Y,
            Z
        };

        public bool IsRotating { get; private set; }
        public bool IsActive = true;

        private void Start(){
            CurrentAngle = _angleFromLastScene;
            _rotation = _rotationFromLastScene;
            LightSwitcher = FindObjectOfType<LightSwitcher>();
            gearsY = FindObjectsOfType<GearRotationYAxis>();
            gearsZ = FindObjectsOfType<gearRotationZaxis>();
            PerformTask();
        }

        //Runs once per frame
        private void Update() {

            // Offset
            _mouseOffset = Input.mousePosition - _mouseReference;

            if (IsRotating) {
                switch (RotationalAxis) {
                    case Axis.X:
                        _rotation.x = -(_mouseOffset.x + _mouseOffset.y) * Sensitivity;
                        break;
                    case Axis.Y:
                        _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * Sensitivity;
                        break;
                    case Axis.Z:
                        _rotation.z = -(_mouseOffset.x + _mouseOffset.y) * Sensitivity;
                        break;
                }
            
                // rotate
                transform.Rotate(_rotation);

                // store mouse
                _mouseReference = Input.mousePosition;
                GetAngle();
                PerformTask();



            }
        }

        private void PerformTask(){
            if (gameObject.name == "coffeeMachine_knob") {
                Debug.Log("coffee knob active");
                LightSwitcher.IsActive = true;
                LightSwitcher.UpdateLightsHue((int)CurrentAngle);
            }
            else if (this.tag == "RemoteController") {
                StereoController.UpdateStereoDegrees((int)CurrentAngle);
            }
            else if (this.tag == "Speaker") {
                foreach (gearRotationZaxis Z in gearsZ)
                    Z.UpdateRotspeed((int)CurrentAngle);
                foreach (GearRotationYAxis Y in gearsY)
                    Y.UpdateRotspeedY((int)CurrentAngle);
            }
        }
        
        //Triggers the remotecontrol event
        private void OnMouseDown() {
        
            // rotating flag
            IsRotating = true;

            // store mouse
            _mouseReference = Input.mousePosition;
        }

        private void OnMouseUp() {
            // rotating flag
            IsRotating = false;
        }

        /// <summary>
        /// Gets an eulerAngle and stores it
        /// </summary>
        private void GetAngle() {

            switch (RotationalAxis) {
                case Axis.X:
                    CurrentAngle = transform.rotation.eulerAngles.x;
                    break;
                case Axis.Y:
                    CurrentAngle = transform.rotation.eulerAngles.y;
                    break;
                case Axis.Z:
                    CurrentAngle = transform.rotation.eulerAngles.z;
                    break;
            }
        }*/