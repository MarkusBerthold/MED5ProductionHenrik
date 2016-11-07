using Assets.Scripts.GameManager;
using UnityEngine;

namespace Assets.Scripts.PlacementGear{
    public class MoveLastBridge : MonoBehaviour{
        private float _currentLerpTime;

        private int _doOnce;
        private bool _doOnceLastGear = true;
        private Vector3 _endPos;

        private readonly float _lerpTime = 5f;
        private SceneLoader _sceneLoader;

        private Vector3 _startPos;

        public GameObject CogPlacer;

        // Use this for initialization
        private void Start(){
            _startPos = transform.localPosition;

            _doOnce = 0;

            _endPos = new Vector3(-0.1068f, 0.2051299f, -0.5089f);
            _sceneLoader = FindObjectOfType<SceneLoader>();
        }

        // Update is called once per frame
        private void Update(){
            if (CogPlacer.GetComponent<placeLastGear>().GetConnected())
                if ((CogPlacer.GetComponent<placeLastGear>().Placeable.tag == "LastGear") && _doOnceLastGear){
                    while (_doOnce == 0){
                        _currentLerpTime = 0f;
                        _doOnce = 1;
                    }

                    _currentLerpTime += Time.deltaTime;
                    if (_currentLerpTime > _lerpTime) _currentLerpTime = _lerpTime;

                    //lerp!
                    var perc = _currentLerpTime/_lerpTime;
                    transform.localPosition = Vector3.Lerp(_startPos, _endPos, perc);

                    if (perc >= 1){
                        _doOnceLastGear = false;
                        // do loading
                        _sceneLoader.LoadScene();
                    }
                }
        }
    }
}