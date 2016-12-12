/*
    Script handles the state of the environment
*/

using System.Collections;
using Assets.Scripts.Highlighting;
using Assets.Scripts.LivingRoom.LivingRoomSoundtrack;
using Assets.Scripts.MessageingSystem;
using Assets.Scripts.Sound_System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManager {
    public class GameStateManager : MonoBehaviour {
        public enum State {
            Start,
            Coffee,
            Stereo,
            BackFromClock,
            End,
            BackFromStereo,
            BackFromLight
        }

        #region SINGLETON PATTERN

        public static GameStateManager Instance;

        #endregion

        private static SceneLoader _clockLoader;
        private static SceneLoader _stereoLoader;
        private static SceneLoader _lightLoader;


        public static string LightLoaderGameObjectName = "remote";
        public static string StereoLoaderGameObjectName = "speaker";
        public static string ClockLoaderGameObjectName = "wallClock_centerFace";

        public static DayNightController DayNightController;

        public static SoundManager _soundManager;

        //public static BroadcastSpeaker _BroadcastSpeaker;
        public static LivingRoomSoundtrack _LivingRoomsSoundtrack;

        private static Highlighter _wallClockHighlighter;
        public static string WallClockHighlighterObjectName = "wallClock_centerFace";

        public State GameState = State.Start;

        private string previousScene;


        private void Awake(){
            if (Instance != null && Instance != this){
                Destroy(gameObject);
            }
            else{
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }

//#if UNITY_EDITOR //the following piece of code will only run if we are in editor mode. 
            PlayerPrefs.DeleteAll();
//#endif
        }

        private void Start(){
            FindReferences();
            ChangeState(State.Start);
        }

        private void OnEnable(){
            SceneManager.sceneLoaded += OnLevelFinishedLoading;
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnDisable(){
            SceneManager.sceneLoaded -= OnLevelFinishedLoading;
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene oldScene, Scene newScene){
            previousScene = newScene.name;
            print("PreviousScene is: " + previousScene);
        }

        private void OnLevelFinishedLoading(Scene newScene, LoadSceneMode mode){
            Debug.Log("GSM:: new scene: " + newScene.name);

            if (!newScene.name.Equals(SceneManager.GetSceneByName(SceneLoader.Scene.LivingRoom.ToString()).name))
                FindObjectOfType<SceneLoader>().IsEnterable = true;
            else{
                if (FindReferences())
                    ChangeState(previousScene);
            }
        }

        public void ChangeState(string oldSceneName){
            switch (oldSceneName){
                case "Clock":
                    GameState = State.BackFromClock;
                    goto default;
                case "Light":
                    GameState = State.BackFromLight;
                    goto default;
                case "Stereo":
                    GameState = State.BackFromStereo;
                    goto default;
                default:
                    if (!_LivingRoomsSoundtrack)
                        _LivingRoomsSoundtrack = FindObjectOfType<LivingRoomSoundtrack>();
                    switch (GameState){
                        case State.BackFromClock:
                            _clockLoader.IsEnterable = false;
                            //_lightLoader.IsEnterable = true;
                            _stereoLoader.IsEnterable = true;
                            _soundManager.BackFromClock();
                            _soundManager.SetWaitForBroadcast(false);
                            //_BroadcastSpeaker.SetShouldPlay(false); //the broadcast speaker knows if it should play 
                            _soundManager.StopListening();
                            _soundManager.ShouldListen = false;
                            //_StereoController.MessUp ();   //not needed with persistence
                            _LivingRoomsSoundtrack.SetState("Clock");
                            goto default;
                        case State.BackFromLight:
                            _lightLoader.IsEnterable = false;
                            _soundManager.BackFromLight();
                            _soundManager.SetWaitForBroadcast(false);
                            // _BroadcastSpeaker.SetShouldPlay(false);
                            _soundManager.StopListening();
                            _soundManager.ShouldListen = false;
                            _LivingRoomsSoundtrack.SetState("Light");
                            StartCoroutine(EndGame());
                            
                            goto default;
                        case State.BackFromStereo:
                            _stereoLoader.IsEnterable = false;
                            _lightLoader.IsEnterable = true;
                            _soundManager.BackFromStereo();
                            _soundManager.SetWaitForBroadcast(false);
                            // _BroadcastSpeaker.SetShouldPlay(false);
                            _soundManager.StopListening();
                            _soundManager.ShouldListen = false;
                            _LivingRoomsSoundtrack.SetState("Stereo");
                            goto default;
                        default:
                            DayNightController.CurrentTimeOfDay += 0.33333f;
                            EventManager.TriggerEvent(GameState.ToString());   //trigger an event with the same name as the state we are in.
                            break;
                    }
                    break;
            }
        }

        public void ChangeState(State newState){
            switch (newState){
                case State.Start:
                    GameState = State.Start;
                    _clockLoader.IsEnterable = false;
                    _lightLoader.IsEnterable = false;
                    _stereoLoader.IsEnterable = false;
                    _soundManager.SetWaitForBroadcast(true);
                    // _BroadcastSpeaker.SetShouldPlay(true);
                    _LivingRoomsSoundtrack.SetState("Start");
                    break;
                case State.Coffee:
                    GameState = State.Coffee;
                    break;
                case State.Stereo:
                    GameState = State.Stereo;
                    _clockLoader.IsEnterable = true;
                    _wallClockHighlighter.Activated = true;
                    break;
                case State.End:
                    GameState = State.End;
                    _soundManager.End();
                    break;
            }
            Debug.Log("changed state to: " + newState);
        }

        private bool FindReferences(){
            _clockLoader = GameObject.Find(ClockLoaderGameObjectName).GetComponent<SceneLoader>();
            _lightLoader = GameObject.Find(LightLoaderGameObjectName).GetComponent<SceneLoader>();
            _stereoLoader = GameObject.Find(StereoLoaderGameObjectName).GetComponent<SceneLoader>();

            DayNightController = FindObjectOfType<DayNightController>();

            _soundManager = FindObjectOfType<SoundManager>();

            //_BroadcastSpeaker = FindObjectOfType<BroadcastSpeaker>();

            _LivingRoomsSoundtrack = FindObjectOfType<LivingRoomSoundtrack>();

            //_StereoController = FindObjectOfType<StereoController> ();

            _wallClockHighlighter = GameObject.Find(WallClockHighlighterObjectName).GetComponent<Highlighter>();

            Debug.Log("Loaders Loaded: " +
                      (_lightLoader & _stereoLoader & _clockLoader & _soundManager & _LivingRoomsSoundtrack &
                       _wallClockHighlighter));

            return _lightLoader & _stereoLoader & _clockLoader & DayNightController & _soundManager &
                   _LivingRoomsSoundtrack & _wallClockHighlighter;
        }

       IEnumerator EndGame()
       {
           yield return new WaitForSeconds(10);
			Application.Quit ();
       }
    }
}