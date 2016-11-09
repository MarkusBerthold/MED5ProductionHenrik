using System.Collections;
using Assets.Scripts.ClockLevel;
using UnityEngine;

namespace Assets.Scripts.ClockItem{
    public class clockHandRotation : MonoBehaviour{
        public float HourHandBrokenRotSpeed; //controls the speed of the hour dial?
        private float _hourHandRotSpeed;
        public float MinuteHandBrokenRotSpeed; //controls the speed of the minute dial?
        private float _minuteHandRotSpeed;
        public float SecondHandBrokenRotSpeed; //controls the speed of the second dial?
        private float _secondHandRotSpeed;

        public bool IsBroken; //sets whether or not if the dials goes with or against the clock?
        //public float Period = 3f; //weren't we suppose to delete thìs part?

        private bool _notDone = true;

        // Use this for initialization
        private void Start(){
       
        }

        // Update is called once per frame
        private void Update(){

            //If the clock is broken, set the rotation speed to that variable
            //otherwise call the unbroken rotation function once.
            if (IsBroken){
                gameObject.transform.FindChild("HourPivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = HourHandBrokenRotSpeed;
                gameObject.transform.FindChild("MinutePivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = MinuteHandBrokenRotSpeed;
                gameObject.transform.FindChild("SecondPivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = SecondHandBrokenRotSpeed;
            }else if (_notDone){
                StartRotationNotBroken();
                _notDone = false;
            }
        }

        /// <summary>
        /// Starts the unbroken rotation of the clock.
        /// Should only be called once.
        /// </summary>
        private void StartRotationNotBroken(){
            /*gameObject.transform.FindChild("HourPivot").
                gameObject.GetComponent<gearRotationZaxis>().RotateWaitHour(_hourHandRotSpeed);
            gameObject.transform.FindChild("MinutePivot").
                gameObject.GetComponent<gearRotationZaxis>().RotateWaitMinute(_minuteHandRotSpeed);
            gameObject.transform.FindChild("SecondPivot").
                gameObject.GetComponent<gearRotationZaxis>().RotateWaitSecond(_secondHandRotSpeed);*/
        }
        }
    }
