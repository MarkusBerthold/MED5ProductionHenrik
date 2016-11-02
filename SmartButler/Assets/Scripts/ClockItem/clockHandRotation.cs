using Assets.Scripts.ClockLevel;
using UnityEngine;

namespace Assets.Scripts.ClockItem{
    public class clockHandRotation : MonoBehaviour{
        private float _hourHandBrokenRotSpeed;
        private float _hourHandRotSpeed;
        private float _minuteHandBrokenRotSpeed;
        private float _minuteHandRotSpeed;
        private float _secondHandBrokenRotSpeed;
        private float _secondHandRotSpeed;

        private float _nextActionTime;
        public bool IsBroken;
        public float Period = 3f;

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
            _hourHandRotSpeed = 0.5f;
            _minuteHandRotSpeed = _hourHandRotSpeed*6;
            _secondHandRotSpeed = _minuteHandRotSpeed*6;
        }

        // Update is called once per frame
        private void Update(){
            if (Time.time > _nextActionTime){
                _nextActionTime += Period;
                _hourHandBrokenRotSpeed = Random.Range(-100f, 100f);
                _minuteHandBrokenRotSpeed = Random.Range(-50f, 50f);
                _secondHandBrokenRotSpeed = Random.Range(-10f, 10f);
            }

            //If statements for changing rotation speeds if the clock is broken
            if (IsBroken)
                gameObject.transform.FindChild("HourPivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = _hourHandBrokenRotSpeed;
            else
                gameObject.transform.FindChild("HourPivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = _hourHandRotSpeed;
            if (IsBroken)
                gameObject.transform.FindChild("MinutePivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = _minuteHandBrokenRotSpeed;
            else
                gameObject.transform.FindChild("MinutePivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = _minuteHandRotSpeed;
            if (IsBroken)
                gameObject.transform.FindChild("SecondPivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = _secondHandBrokenRotSpeed;
            else
                gameObject.transform.FindChild("SecondPivot").
                    gameObject.GetComponent<gearRotationZaxis>().Rotspeed = _secondHandRotSpeed;
        }
    }
}