using System.Collections;
using Assets.Scripts.ClockLevel;
using UnityEngine;

namespace Assets.Scripts.ClockItem {
    public class clockHandRotation : MonoBehaviour {
        public Transform HourArmTransform;
        public Transform MinuteArmTransform;
        public Transform SecondArmTransform;

        private float speed;
        private int _secondCounter = 0;
        private int _minuteCounter = 0;

        void Start(){
            Speed = 1;
            StartCoroutine(Clock());
        }

        private IEnumerator Clock(){
            while (gameObject.activeSelf){
                SecondArmTransform.Rotate(Vector3.forward,360f/60f);
                _secondCounter++;
                if (_secondCounter == 60) {
                    MinuteArmTransform.Rotate(Vector3.forward, 360f / 60f);
                    _minuteCounter++;
                    _secondCounter = 0;
                    if (_minuteCounter == 60) {
                        HourArmTransform.Rotate(Vector3.forward, 360f / 12f);
                        _minuteCounter = 0;
                    }
                }
                yield return new WaitForSeconds(Speed);
            }

        }


        public float Speed{
            get { return 1/speed; }
            set { speed = value; }
        }
    }
}