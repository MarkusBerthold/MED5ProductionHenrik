using Assets.Scripts.ClockItem;
using Assets.Scripts.Controllers;
//using Assets.Scripts.MessageingSystem;
using UnityEngine;

namespace Assets.Scripts.ObjectInteraction{
    public class ObjectRotater : MonoBehaviour {

        public float CurrentAngle;
        private Vector3 _mouseOffset;
        private Vector3 _mouseReference;
        private Vector3 _rotation;
        private float _sensitivity;
        public Axis RotationalAxis;
        public LightSwitcher LightSwitcher;
        public StereoController StereoController;
        public clockHandRotation ClockRotater;

        public enum Axis {
            X,
            Y,
            Z
        };

        public bool IsRotating { get; private set; }
        public bool IsActive = true;

        private void Start() {
            _sensitivity = 0.4f;
            _rotation = Vector3.zero;

        }

        //Runs once per frame
        private void Update() {

            // Offset
            _mouseOffset = Input.mousePosition - _mouseReference;

            if (IsRotating) {
                switch (RotationalAxis) {
                    case Axis.X:
                        _rotation.x = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
                        break;
                    case Axis.Y:
                        _rotation.y = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
                        break;
                    case Axis.Z:
                        _rotation.z = -(_mouseOffset.x + _mouseOffset.y) * _sensitivity;
                        break;
                }
            
                // rotate
                transform.Rotate(_rotation);

                // store mouse
                _mouseReference = Input.mousePosition;
                GetAngle();

                if (gameObject.name == "coffeeMachine_knob"){
                    LightSwitcher.IsActive = true;
                    LightSwitcher.UpdateLightsHue((int) CurrentAngle);
                }
                else if(this.tag == "RemoteController"){
                    StereoController.UpdateStereoDegrees((int) CurrentAngle);
                }else if (gameObject.name == "knob"){
                    ClockRotater.HourHandBrokenRotSpeed = (CurrentAngle);
                    ClockRotater.MinuteHandBrokenRotSpeed = (CurrentAngle *2);
                    ClockRotater.SecondHandBrokenRotSpeed = (CurrentAngle *4);
                }

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
        }
    }
}