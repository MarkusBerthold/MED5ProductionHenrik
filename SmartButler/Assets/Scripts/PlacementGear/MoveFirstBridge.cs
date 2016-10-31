using UnityEngine;

namespace Assets.Scripts.PlacementGear{
    public class MoveFirstBridge : MonoBehaviour{
        private float _currentLerpTime;

        private int _doOnce;
        private bool _doOnceFirstGear = true;
        private Vector3 _endPos;

        private readonly float _lerpTime = 7f;

        private Vector3 _startPos;

        public GameObject CogPlacer;

        // Use this for initialization
        private void Start(){
            _startPos = transform.localPosition;

            _doOnce = 0;

            _endPos = new Vector3(0.2f, 0f, 0.1377234f);
        }

        // Update is called once per frame
        private void Update(){
            if (CogPlacer.GetComponent<PlaceFirstGear>().GetConnected())
                if ((CogPlacer.GetComponent<PlaceFirstGear>().Placeable.tag == "FirstGear") && _doOnceFirstGear){
                    while (_doOnce == 0){
                        _currentLerpTime = 0f;
                        _doOnce = 1;
                    }

                    _currentLerpTime += Time.deltaTime;
                    if (_currentLerpTime > _lerpTime)
                        _currentLerpTime = _lerpTime;

                    //lerp!
                    var perc = _currentLerpTime/_lerpTime;
                    transform.localPosition = Vector3.Lerp(_startPos, _endPos, perc);

                    print(perc);

                    if (perc >= 1)
                        _doOnceFirstGear = false;
                }
        }
    }
}