/*
    Script handles the state of the environment
*/

using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;
using Assets.Scripts.Sound_System;
using Assets.Scripts.LivingRoom.BroadcastSpeaker;
using Assets.Scripts.LivingRoom.LivingRoomSoundtrack;
using Assets.Scripts.Controllers;
using Assets.Scripts.Highlighting;


namespace Assets.Scripts.GameManager {
    public class GameStateManager : MonoBehaviour {
        #region SINGLETON PATTERN

        public static GameStateManager Instance;

        #endregion

        public enum State {
            Start,
            Coffee,
			Stereo,
            BackFromClock,
            End,
            BackFromStereo,
            BackFromLight
        };

        public State GameState = State.Start;

        private static SceneLoader _clockLoader;
        private static SceneLoader _stereoLoader;
        private static SceneLoader _lightLoader;


        public static string LightLoaderGameObjectName = "remote";
        public static string StereoLoaderGameObjectName = "speaker";
        public static string ClockLoaderGameObjectName = "wallClock_centerFace";

        public static DayNightController DayNightController;

		public static SoundManager _soundManager;

		public static BroadcastSpeaker _BroadcastSpeaker;
		public static LivingRoomSoundtrack _LivingRoomsSoundtrack;

		public static StereoController _StereoController;

        private static Highlighter _wallClockHighlighter;
        public static string WallClockHighlighterObjectName = "wallClock_centerFace";

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

				print ("Test123");
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
                    switch (GameState) {
					case State.BackFromClock:
							_clockLoader.IsEnterable = false;
									//_lightLoader.IsEnterable = true;
							_stereoLoader.IsEnterable = true;
							_soundManager.BackFromClock ();
							_soundManager.SetWaitForBroadcast (false);
							_BroadcastSpeaker.SetShouldPlay (false);
							_soundManager.StopListening ();
							_soundManager.ShouldListen = false;
							_StereoController.MessUp ();
							_LivingRoomsSoundtrack.SetState ("Clock");
                            goto default;
					case State.BackFromLight:
                            _lightLoader.IsEnterable = false;
							_soundManager.BackFromLight ();
							_soundManager.SetWaitForBroadcast (false);
							_BroadcastSpeaker.SetShouldPlay (false);
							_soundManager.StopListening ();
							_soundManager.ShouldListen = false;
							_LivingRoomsSoundtrack.SetState ("Light");
                            goto default;
					case State.BackFromStereo:
							_stereoLoader.IsEnterable = false;
							_lightLoader.IsEnterable = true;
							_soundManager.BackFromStereo ();
							_soundManager.SetWaitForBroadcast (false);
							_BroadcastSpeaker.SetShouldPlay (false);
							_soundManager.StopListening ();
							_soundManager.ShouldListen = false;
							_LivingRoomsSoundtrack.SetState ("Stereo");
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
					_soundManager.SetWaitForBroadcast (true);
					_BroadcastSpeaker.SetShouldPlay (true);
					_LivingRoomsSoundtrack.SetState ("Start");
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
					_soundManager.End ();
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

			_BroadcastSpeaker = FindObjectOfType <BroadcastSpeaker> ();

			_LivingRoomsSoundtrack = FindObjectOfType<LivingRoomSoundtrack> ();

			_StereoController = FindObjectOfType<StereoController> ();

            _wallClockHighlighter = GameObject.Find(WallClockHighlighterObjectName).GetComponent<Highlighter>();

			Debug.Log("Loaders Loaded: " + (_lightLoader & _stereoLoader & _clockLoader & _soundManager & _BroadcastSpeaker & _LivingRoomsSoundtrack & _StereoController & _wallClockHighlighter));

			return _lightLoader & _stereoLoader & _clockLoader & DayNightController & _soundManager & _BroadcastSpeaker & _LivingRoomsSoundtrack & _StereoController & _wallClockHighlighter;
        }
    }
}