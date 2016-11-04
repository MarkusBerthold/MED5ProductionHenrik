﻿using System.Collections;
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

        private float _nextActionTime;
        public bool IsBroken; //sets whether or not if the dials goes with or against the clock?
        public float Period = 3f; //weren't we suppose to delete thìs part?

        private bool _notDone = true;

        // Use this for initialization
        private void Start(){
            IsBroken = true;

            //Set rotation speeds for the different gears. These are never changed
            gameObject.transform.GetChild(0).gameObject.GetComponent<gearRotationZaxis>().Rotspeed = -4;
            gameObject.transform.GetChild(1).gameObject.GetComponent<gearRotationZaxis>().Rotspeed = 4;
            gameObject.transform.GetChild(2).gameObject.GetComponent<GearRotationYAxis>().Rotspeed = 5;
            gameObject.transform.GetChild(3).gameObject.GetComponent<GearRotationYAxis>().Rotspeed = 4;
            gameObject.transform.GetChild(7).gameObject.GetComponent<GearRotationYAxis>().Rotspeed = 3;
            gameObject.transform.GetChild(8).gameObject.GetComponent<gearRotationZaxis>().Rotspeed = 3;
            gameObject.transform.GetChild(9).gameObject.GetComponent<gearRotationZaxis>().Rotspeed = 3;
            gameObject.transform.GetChild(11).gameObject.GetComponent<GearRotationYAxis>().Rotspeed = -3;

            //Rotation speed of the clock when NOT broken
            //This is high because it waits in-between rotating
            _hourHandRotSpeed = 1000;
            _minuteHandRotSpeed = 600;
            _secondHandRotSpeed = 200;
            
        }

        // Update is called once per frame
        private void Update(){
            if (Time.time > _nextActionTime){
                _nextActionTime += Period;
                HourHandBrokenRotSpeed = Random.Range(-100f, 100f);
                MinuteHandBrokenRotSpeed = Random.Range(-50f, 50f);
                SecondHandBrokenRotSpeed = Random.Range(-25f, 25f);
            }

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
            gameObject.transform.FindChild("HourPivot").
                gameObject.GetComponent<gearRotationZaxis>().RotateWaitHour(_hourHandRotSpeed);
            gameObject.transform.FindChild("MinutePivot").
                gameObject.GetComponent<gearRotationZaxis>().RotateWaitMinute(_minuteHandRotSpeed);
            gameObject.transform.FindChild("SecondPivot").
                gameObject.GetComponent<gearRotationZaxis>().RotateWaitSecond(_secondHandRotSpeed);
        }
        }
    }
