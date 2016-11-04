/*
    Script handles the state of the environment
*/

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Sound_System;


namespace Assets.Scripts.GameManager {
    public class GameStateManager : MonoBehaviour {
        #region SINGLETON PATTERN

        public static GameStateManager Instance;

        #endregion

        public enum State {
            Start,
            Coffee,
            Clock,
            End,
            Stereo,
            Light
        };

        public State GameState = State.Start;

        private static SceneLoader _clockLoader;
        private static SceneLoader _stereoLoader;
        private static SceneLoader _lightLoader;


        public static string LightLoaderGameObjectName = "remote";
        public static string StereoLoaderGameObjectName = "speaker";
        public static string ClockLoaderGameObjectName = "clock 1";

        public static DayNightController DayNightController;

		public static SoundManager _soundManager;

        string previousScene;


        void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
            }
            else {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        void Start() {
            FindReferences();
            ChangeState(State.Start);
        }

        void OnEnable() {
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene oldScene, Scene newScene) {
            previousScene = newScene.name;
            print("Settings previousScene to: " + previousScene);
        }

        private void OnLevelFinishedLoading(Scene newScene, LoadSceneMode mode) {
            
            Debug.Log("GSM:: new scene name: " + newScene.name);
            if (!newScene.name.Equals(SceneManager.GetSceneByName(SceneLoader.Scene.LivingRoom.ToString()).name)) {
                FindObjectOfType<SceneLoader>().IsEnterable = true;
            } else {
                if (FindReferences()) {
                    ChangeState(previousScene);
                    print("Changing states");
                }
            }
        }

        public void ChangeState(string oldSceneName) {
            switch (oldSceneName) {
                case "Clock":
                    GameState = State.Clock;
                    goto default;
                case "Light":
                    GameState = State.Light;
                    goto default;
                case "Stereo":
                    GameState = State.Stereo;
                    goto default;
                default:
                    switch (GameState) {
						case State.Clock:
							_clockLoader.IsEnterable = false;
							_lightLoader.IsEnterable = true;
							_stereoLoader.IsEnterable = true;
							_soundManager.BackFromClock ();
                            goto default;
                        case State.Light:
                            _lightLoader.IsEnterable = false;
							_soundManager.BackFromLight ();
                            goto default;
                        case State.Stereo:
                            _stereoLoader.IsEnterable = false;
							_soundManager.BackFromStereo ();
                            goto default;
                        default:
                            DayNightController.CurrentTimeOfDay += 0.33333f;
                            break;
                    }
                    break;
            }
        }

        public void ChangeState(State newState) {
            switch (newState) {
                case State.Start:
                    GameState = State.Start;
                    _clockLoader.IsEnterable = false;
                    _lightLoader.IsEnterable = false;
                    _stereoLoader.IsEnterable = false;
                    break;
                case State.Coffee:
                    GameState = State.Coffee;
                    _clockLoader.IsEnterable = true;
                    break;
                case State.End:
                    GameState = State.End;
                    break;
            }
            Debug.Log("changed state to: " + newState.ToString());
        }

        private bool FindReferences() {
            _clockLoader = GameObject.Find(ClockLoaderGameObjectName).GetComponent<SceneLoader>();
            _lightLoader = GameObject.Find(LightLoaderGameObjectName).GetComponent<SceneLoader>();
            _stereoLoader = GameObject.Find(StereoLoaderGameObjectName).GetComponent<SceneLoader>();

            DayNightController = FindObjectOfType<DayNightController>();

			_soundManager = FindObjectOfType<SoundManager> ();

			Debug.Log("Loaders Loaded: " + (_lightLoader & _stereoLoader & _clockLoader & _soundManager));

            return _lightLoader & _stereoLoader & _clockLoader & DayNightController;
        }
    }

    /*

        public static State CurrentState = State.Start;
        private static SceneLoader.Scene currentScene, destinationScene;
        private static SceneLoader _clockLoader;
        private static SceneLoader _stereoLoader;
        private static SceneLoader _lightLoader;


        public static string LightLoaderGameObjectName = "remote";
        public static string StereoLoaderGameObjectName = "speaker";
        public static string ClockLoaderGameObjectName = "clock 1";


        public static DayNightController DayNightController;
        public static GameStateManager Gamestatemanager;

        // All States should be thought of as post-level or -interaction
        public enum State {
            Start,
            Coffee,
            Clock,
            End,
            Stereo,
            Light
        };



        void Awake() {
            if (Gamestatemanager == null) {
                Gamestatemanager = this;
                DontDestroyOnLoad(gameObject);
            } else Destroy(this);
        }

        void Start() {
            Init();
        }

        void OnEnable() {
            //Tell our 'OnLevelFinishedLoading' function to start listening for a scene change as soon as this script is enabled.
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1) {
            if (!arg0.name.Equals(SceneManager.GetSceneAt(0))){
            //means we are loading to living room
            }
        }


        void OnDisable() {
            //Tell our 'OnLevelFinishedLoading' function to stop listening for a scene change as soon as this script is disabled. Remember to always have an unsubscription for every delegate you subscribe to!
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;

        }

        void OnLevelFinishedLoading(Scene scene, LoadSceneMode mode) {
            Debug.Log("game state manager finished loading" + scene.name);


        }

        public static void Init() {

            foreach (SceneLoader sceneLoader in FindObjectsOfType<SceneLoader>() ) {
                string name = sceneLoader.name;

                if (name.Equals(ClockLoaderGameObjectName)) 
                    _clockLoader = sceneLoader;
                 else if (name.Equals(StereoLoaderGameObjectName)) 
                    _stereoLoader = sceneLoader;
                 else if (name.Equals(LightLoaderGameObjectName)) 
                    _lightLoader = sceneLoader;               
            }

            Debug.Log("Loaders Loaded: " + (_lightLoader & _stereoLoader & _clockLoader));

            SceneLoader.Scene? sceneFromString = SceneLoader.GetSceneFromString(SceneManager.GetActiveScene().name);
            if (sceneFromString != null)
                currentScene = (SceneLoader.Scene) sceneFromString;
            print("scene is: " + currentScene);

            DayNightController = FindObjectOfType<DayNightController>();
        
            
            //  _soundManager = GameObject.Find("soundmanager").GetComponent<SceneLoader>();
             

            ChangeCurrentState(State.Start);
        }

        /// <summary>
        /// Method to set a new state in the game. Takes a string of the same name as the State
        /// </summary>
        /// <param name="state"></param>
        public static void ChangeCurrentState(State state) {


           //if (!_clockLoader.IsEnterable && !_stereoLoader.IsEnterable && !_lightLoader.IsEnterable)
           //    CurrentState = State.End;


            switch (state) {
                case State.Start: {
                    CurrentState = State.Start;
                    _clockLoader.IsEnterable = false;
                    _stereoLoader.IsEnterable = false;
                    _lightLoader.IsEnterable = false;
                    DayNightController.CurrentTimeOfDay = 0;

                    return;
                }

                case State.Coffee: {
                    CurrentState = State.Start;
                    _clockLoader.IsEnterable = true;
                    _stereoLoader.IsEnterable = false;
                    _lightLoader.IsEnterable = false;
                    DayNightController.CurrentTimeOfDay = 0.2f;

                    return;
                }
                case State.Clock: {
                    CurrentState = State.Clock;

                    _clockLoader.IsEnterable = false;
                    _stereoLoader.IsEnterable = true;
                    _lightLoader.IsEnterable = true;
                    DayNightController.CurrentTimeOfDay = 0.4f;
                    return;
                }

                case State.Stereo: {
                    CurrentState = State.Stereo;

                    _stereoLoader.IsEnterable = false;
                    DayNightController.CurrentTimeOfDay = 0.6f;
                    return;
                }

                case State.Light: {
                    CurrentState = State.Light;


                    _lightLoader.IsEnterable = false;
                    return;
                }
                case State.End: {
                    CurrentState = State.End;


                    return;
                }
            }
        }
    }*/
}