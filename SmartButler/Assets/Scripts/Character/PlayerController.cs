using UnityEngine;

namespace Assets.Scripts.Character{
    public class PlayerController : MonoBehaviour{
        private readonly float _lookSmoothDamp = 0.1f;
        private float _currentXRotation;
        private float _currentYRotation;

        public float LookSensitivity = 5f;

        public float MoveSpeed = 10f;
        public float TurnSpeed = 50f;
        private float _xRotation;
        private float _xRotationV;
        private float _yRotation;
        private float _yRotationV;

        // Update is called once per frame
        private void Update(){
            //For mouse input/orientation
            _yRotation += Input.GetAxis("Mouse X")*LookSensitivity;
            _xRotation -= Input.GetAxis("Mouse Y")*LookSensitivity;

            _currentXRotation = Mathf.SmoothDamp(_currentXRotation, _xRotation, ref _xRotationV, _lookSmoothDamp);
            _currentYRotation = Mathf.SmoothDamp(_currentYRotation, _xRotation, ref _xRotationV, _lookSmoothDamp);

            transform.rotation = Quaternion.Euler(_xRotation, _yRotation, 0);

            //For keyboard controlls/movement
            if (Input.GetKey(KeyCode.UpArrow))
                transform.Translate(Vector3.forward*MoveSpeed*Time.deltaTime);

            if (Input.GetKey(KeyCode.DownArrow))
                transform.Translate(-Vector3.forward*MoveSpeed*Time.deltaTime);

            if (Input.GetKey(KeyCode.LeftArrow))
                transform.Translate(Vector3.left*MoveSpeed*Time.deltaTime);

            if (Input.GetKey(KeyCode.RightArrow))
                transform.Translate(Vector3.right*MoveSpeed*Time.deltaTime);
        }
    }
}