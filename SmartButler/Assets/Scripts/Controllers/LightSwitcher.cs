using Assets.Scripts.GameManager;
using Assets.Scripts.MessageingSystem;
using Assets.Scripts.ObjectInteraction;
using UnityEngine;

namespace Assets.Scripts.Controllers {
    public class LightSwitcher : MonoBehaviour {
        public static LightSwitcher Instance;

        [Persistent] private static bool _isFixed;


        private int _currentlyActiveRotater;

        private readonly ObjectRotater[] objectRotaters = new ObjectRotater[2];


        public int CurrentlyActiveRotater{
            set { _currentlyActiveRotater = value; }
        }

        public LightController Controller { get; private set; }

        public static bool IsFixed{
            get { return _isFixed; }
        }


        private void Awake(){
            if (Instance != null && Instance != this){
                Destroy(gameObject);
            }
            else{
                Instance = this;
            }

            if (!IsFixed){
                EventManager.StartListening("coffeebutton", OnLightsButton);
                EventManager.StartListening(GameStateManager.State.BackFromLight.ToString(), OnLightFixed);
            }
            else{
                _currentlyActiveRotater = 1;
                EventManager.StartListening("remotecontrol", OnLightsButton);
            }
        }

        private void Start(){
            Controller = new LightController(0.8f);

            int i = 0;
            foreach (ObjectRotater rotater in ObjectRotater.AllObjectRotaters){
                for (int j = 0; j < 2; j++)
                    if (rotater.AssociatedObject[j] == RotateableObject.Light){
                        objectRotaters[i++] = rotater;
                    }
            }

            Debug.Log("light is broken: "+ !IsFixed);
            Debug.Log("light is controlled by: " + objectRotaters[_currentlyActiveRotater].gameObject.name);
        }


        private void OnLightFixed() {
            Debug.Log("Lights have been fixed");
            _currentlyActiveRotater = 1;
            _isFixed = true;
            EventManager.StopListening(GameStateManager.State.BackFromStereo.ToString(), OnLightFixed);
        }


        private void Update(){
            if (objectRotaters[_currentlyActiveRotater].HasNewValue){
                Controller.ChangeHue(Color.HSVToRGB(objectRotaters[_currentlyActiveRotater].CurrentAngle / 360f, 1, 1));
            }
        }


        private void OnLightsButton(){
            Controller.FlipLights();
        }
    }
}