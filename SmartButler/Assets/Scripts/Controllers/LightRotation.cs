using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class LightRotation : MonoBehaviour {
        private Vector3 _currentRotationEuler;

        private bool _isRotating;
        private readonly float _rotationThreshold = 2f;

        private Quaternion _target;
        private Vector3 _targetRotationEuler;
        private int _rotationPos;

        public GameObject Rotator;

        public float RotSpeed = 10f;


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
                Input.GetButtonDown("Horizontal")) {
                if (horizontalInput < 0)
                    horizontalInput = 1;
                else if (horizontalInput > 0)
                    horizontalInput = -1;

                if (_rotationPos == 8)
                    _rotationPos = 0;
                else if (_rotationPos < 0)
                    _rotationPos = 7;
                _rotationPos += (int) horizontalInput;
                _target = Quaternion.Euler(45*_rotationPos, 0, 0);

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
    }
}