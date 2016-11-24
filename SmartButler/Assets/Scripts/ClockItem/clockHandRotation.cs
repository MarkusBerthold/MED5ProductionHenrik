using System.Collections;
using Assets.Scripts.GameManager;
using Assets.Scripts.MessageingSystem;
using Assets.Scripts.ObjectInteraction;
using UnityEngine;

namespace Assets.Scripts.ClockItem {
    public class clockHandRotation : MonoBehaviour {
        [Persistent] private static Quaternion _hourArmRotation;

        [Persistent] private static Quaternion _minuteArmRotation;

        [Persistent] private static Quaternion _secondArmRotation;

        [Persistent] private static bool _loaded;

        private bool _broken = true;


        private int _minuteCounter;
        private int _secondCounter;
        private int _maxAbsSpeed = 5;

        public Transform HourArmTransform;
        public Transform MinuteArmTransform;

        private ObjectRotater objectRotater;
        public Transform SecondArmTransform;

        public float speed;


        public float Speed{
            get{
                return speed;
            }
            set { speed = value; }
        }

        public bool Broken{
            get { return _broken; }
            set { _broken = value; }
        }

        private void Start(){
            if (_loaded){
                HourArmTransform.rotation = _hourArmRotation;
                MinuteArmTransform.rotation = _minuteArmRotation;
                SecondArmTransform.rotation = _secondArmRotation;
            }
            else
                _loaded = true;

            

            if (Broken){
                foreach (ObjectRotater rotater in ObjectRotater.AllObjectRotaters)
                    foreach (RotateableObject o in rotater.AssociatedObject)
                        if (o == RotateableObject.Clock)
                            objectRotater = rotater;
                Speed = 5f;
                EventManager.StartListening(GameStateManager.State.BackFromStereo.ToString(), OnClockFixed);
            }
            else
                Speed = 1;


            StartCoroutine(Clock());
        }

        private void OnClockFixed(){
            Broken = false;
            EventManager.StopListening(GameStateManager.State.BackFromStereo.ToString(), OnClockFixed);
        }


        private IEnumerator Clock(){
            while (gameObject.activeSelf){
                if (Broken){
                    if (objectRotater.HasNewValue)
                        Debug.Log("setting new clock speed:  " + objectRotater.CurrentAngle); //.Remap(0, 180, -2f, 2f));
                    
                        Speed = objectRotater.CurrentAngle.Remap(-180, 180, -2f, 2f);

                }
                else
                    Speed = 1;

                Debug.Log("Clock Speed: " + Speed);
                SecondArmTransform.Rotate(Vector3.forward, 360f/60f);
                _secondCounter++;
                if (_secondCounter == 60){
                    MinuteArmTransform.Rotate(Vector3.forward, 360f/60f);
                    _minuteCounter++;
                    _secondCounter = 0;
                    if (_minuteCounter == 60){
                        HourArmTransform.Rotate(Vector3.forward, 360f/12f);
                        _minuteCounter = 0;
                    }
                }
                yield return new WaitForSeconds(Speed);
            }
        }
    }
}