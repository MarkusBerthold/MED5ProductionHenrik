using UnityEngine;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.PlacementGear{
    public class MoveSecondBridge : MonoBehaviour{
        private int _doOnce;
        private bool _doOnceSecondGear = true;

        private float _timer;

        public GameObject CogPlacer;
        //private float startTime = Time.deltaTime;

        // Use this for initialization
        private void Start(){
            _doOnce = 0;
        }

        // Update is called once per frame
        private void Update(){
            if (CogPlacer.GetComponent<PlaceSecondGear>().GetConnected())
                if (CogPlacer.GetComponent<PlaceSecondGear>().Placeable.tag == "SecondGear"){
                    if (_timer < 9.4f)
                        _timer += Time.deltaTime;


				if (_timer < 9.4f)
					transform.Rotate (new Vector3 (0, 1, 0) * Time.deltaTime * -20, Space.Self);
				else if (_timer >= 9.4f) {
					CogPlacer.GetComponent<PlaceSecondGear> ().SetShouldRotate (false);
					EventManager.TriggerEvent ("addsecondsound");
				}
                }
        }
    }
}