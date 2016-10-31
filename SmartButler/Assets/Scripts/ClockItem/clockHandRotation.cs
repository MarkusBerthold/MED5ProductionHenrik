using Assets.Scripts.ClockLevel;
using UnityEngine;

namespace Assets.Scripts.ClockItem{
    public class clockHandRotation : MonoBehaviour{
        private float _largeHandBrokenRotSpeed;

        private float _largeHandRotSpeed;

        private float _nextActionTime;
        private float _smallHandBrokenRotSpeed;
        private float _smallHandRotSpeed;

        public bool IsBroken;
        public float Period = 3f;

        // Use this for initialization
        private void Start(){
            IsBroken = true;

            _largeHandRotSpeed = 1;
            _smallHandRotSpeed = _largeHandRotSpeed*60;
        }

        // Update is called once per frame
        private void Update(){
            if (Time.time > _nextActionTime){
                _nextActionTime += Period;
                _largeHandBrokenRotSpeed = Random.Range(-100f, 100f);
                _smallHandBrokenRotSpeed = Random.Range(-10f, 10f);
            }


            if (IsBroken)
                gameObject.transform.GetChild(0).
                    gameObject.GetComponent<GearRotationZaxis>().Rotspeed = _largeHandBrokenRotSpeed;
            else
                gameObject.transform.GetChild(0).
                    gameObject.GetComponent<GearRotationZaxis>().Rotspeed = _largeHandRotSpeed;
            if (IsBroken)
                gameObject.transform.GetChild(3).
                    gameObject.GetComponent<GearRotationZaxis>().Rotspeed = _smallHandBrokenRotSpeed;
            else
                gameObject.transform.GetChild(3).
                    gameObject.GetComponent<GearRotationZaxis>().Rotspeed = _smallHandRotSpeed;
        }
    }
}