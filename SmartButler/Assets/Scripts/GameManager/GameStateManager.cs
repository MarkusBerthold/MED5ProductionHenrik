/*
    Script handles the state of the environment
*/

using Assets.Scripts.Controllers;
using UnityEngine;
using UnityEngine.Events;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.GameManager{
    public class GameStateManager : MonoBehaviour {
        public State CurrentState = State.Start;
        public StereoController StereoController;
        public LightSwitcher LightSwitcher;
        /*public bool BackFromStereo;
    public bool BackFromClock;
    public bool BackFromLights;*/
        private SceneLoader _clockLoader;
        private SceneLoader _stereoLoader;
        private SceneLoader _lightLoader;

		/*
		 * private SoundManager _soundManager;
		 */

        public DayNightController DayNightController ;

        // All States should be thought of as post-level or -interaction
        public enum State {
            Start,
            Coffee,
            Clock,
            End,
            Stereo,
            Light
        };

        void Start(){
            _clockLoader = GameObject.Find("wallClock").GetComponent<SceneLoader>();
            Debug.Log(_clockLoader == null);
            _stereoLoader = GameObject.Find("speaker").GetComponent<SceneLoader>();
            Debug.Log(_stereoLoader == null);
            _lightLoader = GameObject.Find("remote").GetComponent<SceneLoader>();
            Debug.Log(_lightLoader == null);

			/*
			 * _soundManager = GameObject.Find("soundmanager").GetComponent<SceneLoader>();
			 */

            ChangeCurrentState(State.Start);
            //DayNightController = FindObjectOfType<DayNightController>();
        }


        /// <summary>
        /// Method to set a new state in the game. Takes a string of the same name as the State
        /// </summary>
        /// <param name="state"></param>
        public void ChangeCurrentState(State state){
            if (!_clockLoader.IsEnterable && !_stereoLoader.IsEnterable && !_lightLoader.IsEnterable)
                CurrentState = State.End;


            switch (state){
                case State.Start:
                    CurrentState = State.Start;
                    _clockLoader.IsEnterable = false;
                    _stereoLoader.IsEnterable = false;
                    _lightLoader.IsEnterable = false;
                    DayNightController.CurrentTimeOfDay = 0;

                    return;

                case State.Coffee:
                    CurrentState = State.Start;
                    _clockLoader.IsEnterable = true;
                    _stereoLoader.IsEnterable = false;
                    _lightLoader.IsEnterable = false;
                    DayNightController.CurrentTimeOfDay = 0.2f;

                    return;
                case State.Clock:
                    CurrentState = State.Clock;
                    /*
                 clock fixed all other levels are enterable
                 */

				/*
				 * _soundManager._state = 5;
				 */
                    _clockLoader.IsEnterable = false;
                    _stereoLoader.IsEnterable = true;
                    _lightLoader.IsEnterable = true;
                    DayNightController.CurrentTimeOfDay = 0.4f;
                    return;

                case State.Stereo:
                    CurrentState = State.Stereo;
                    /*
                 clock fixed all other levels are enterable
                 */

				/*
				* _soundManager._state = 6;
				*/
                    _stereoLoader.IsEnterable = false;
                    DayNightController.CurrentTimeOfDay = 0.6f;
                    return;

                case State.Light:
                    CurrentState = State.Light;
                    /*
                 clock fixed all other levels are enterable
                 */

				/*
				* _soundManager._state = 7;
				*/
                    _lightLoader.IsEnterable = false;
                    return;
                case State.End:
                    CurrentState = State.End;
                    /*
                 all fixed no levels are enterable
                 */

                    return;
            }
        }
    }
}