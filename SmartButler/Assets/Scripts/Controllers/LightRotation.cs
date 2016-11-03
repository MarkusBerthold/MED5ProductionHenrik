using System.Collections;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.Scripts.Controllers {
    public class LightRotation : MonoBehaviour {
        private Vector3 _currentRotationEuler;

        private bool _isRotating; //makes rotation work like a step by step function rather than flute motion?
        private readonly float _rotationThreshold = 2f; //no idea...

        private Quaternion _target;
        private Vector3 _targetRotationEuler; //is the center of the rotation? it doesn't look like it's used anywhere in the script...
        private int _rotationPos;

        public GameObject Rotator; //this is the tunnel?

        public float RotSpeed = 10f; //sets how fast the rotation is carried out?

        private bool isGrounded = true; //makes sure you can only move if you touch the tunnel?

        public Quaternion savedRotation;
        public int savedRotationPos;

        //private bool isLerping;

        // Use this for initialization
        private void Start() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            Physics.gravity = Physics.gravity*10;
        }

        // Update is called once per frame
        private void Update() {
            var horizontalInput = Input.GetAxis("Horizontal");
            
            if ((horizontalInput != 0) && !_isRotating &&
                Input.GetButtonDown("Horizontal") && isGrounded){

                savedRotation = Rotator.transform.rotation; //saves the rotation
                savedRotationPos = _rotationPos; 

                if (horizontalInput < 0)
                    horizontalInput = -1; //makes sure the tunnel rotate to the right side when going left?
                else if (horizontalInput > 0)
                    horizontalInput = 1; //makes sure the tunnel rotate to the right side when going right?

                if (_rotationPos == 8)
                    _rotationPos = 0;
                else if (_rotationPos < 0)
                    _rotationPos = 7;
                _rotationPos += (int) horizontalInput;
                _target = Quaternion.Euler(45*_rotationPos, 0, 0);

                //why did we have to use coroutine? couldn't a method work too?
                StartCoroutine(RotateStep());
            }
        }

        /// <summary>
        /// Calculates the rotation steps
        /// </summary>
        /// <returns></returns>
        private IEnumerator RotateStep() {
            var rotationStep = 0.01f;
            _isRotating = true;
            while (_isRotating &&
                   (Quaternion.Angle(Rotator.transform.rotation, _target) > _rotationThreshold) &&
                   (rotationStep <= 1)) {
                Rotator.transform.rotation = Quaternion.Slerp(Rotator.transform.rotation,
                    _target, rotationStep);

                rotationStep += 0.01f;
                yield return null;
            }
            Rotator.transform.rotation = _target;
            _isRotating = false;
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="collision"></param>
        void OnCollisionEnter(Collision collision){
            isGrounded = true;
        }

        /// <summary>
        /// ?
        /// </summary>
        /// <param name="collisionInfo"></param>
        void OnCollisionExit(Collision collisionInfo){
            isGrounded = false;
        }

        /// <summary>
        /// Reset values of the level to save
        /// </summary>
        public void ResetLevel(){
            Rotator.transform.rotation = savedRotation;
            _rotationPos = savedRotationPos;
        }
    }
}