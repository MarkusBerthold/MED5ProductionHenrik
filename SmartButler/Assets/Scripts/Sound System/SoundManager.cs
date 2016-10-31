using System;
using System.Collections;
using Assets.Scripts.MessageingSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Sound_System{
    public class SoundManager : MonoBehaviour{
        //Define new states here
        public enum StatesEnum{
            Start,
            Coffee,
            RemoteControl
        }

        private bool _introsHasBeenPlayed;
        private int _returnState;

        private UnityAction _someListener;
        private int _state;
        public State[] Arrayofstates;
        public AudioSource AudioSource;

        private void Awake(){
            _someListener = Coffee;
        }

        // Use this for initialization
        private void Start(){
            AudioSource = GameObject.FindGameObjectWithTag("Thoughts").GetComponent<AudioSource>();
            var nrOfStates = Enum.GetValues(typeof(StatesEnum)).Length;
            Arrayofstates = new State[nrOfStates];

            for (var i = 0; i < Arrayofstates.Length; i++)
                Arrayofstates[i] = new State((StatesEnum) i);
            for (var i = 0; i < Arrayofstates.Length; i++)
                StartCoroutine(Arrayofstates[i].ResetCues());
        }

        // Update is called once per frame
        private void Update(){
            //play intros, some state dont have intros, some "interactions" are intros
            for (var j = 0; j < Arrayofstates[_state].AmountOfIntros; j++)
                if (!AudioSource.isPlaying && !Arrayofstates[_state].StateIntrosBeenPlayed[j]){
                    AudioSource.clip = Arrayofstates[_state].StateIntros[j];
                    AudioSource.Play();
                    Arrayofstates[_state].StateIntrosBeenPlayed[j] = true;
                    _introsHasBeenPlayed = true;
                }

            //play cues
            for (var j = 0; j < Arrayofstates[_state].AmountOfCues; j++)
                if (!AudioSource.isPlaying && !Arrayofstates[_state].StateCuesBeenPlayed[j] && _introsHasBeenPlayed){
                    AudioSource.clip = Arrayofstates[_state].StateCues[j];
                    AudioSource.Play();
                    Arrayofstates[_state].StateCuesBeenPlayed[j] = true;
                }
        }

        /// <summary>
        /// Sets the _state to 1
        /// </summary>
        private void Coffee(){
            Debug.Log("coffeebutton was pressed");
            _state = 1;
        }

        /// <summary>
        /// Sets the _state to 2
        /// </summary>
        private void RemoteControl(){
            Debug.Log("Remote Control was pressed");
            _state = 2;
        }

        //Starts event listening
        private void OnEnable(){
            EventManager.StartListening("coffeebutton", _someListener);
            EventManager.StartListening("remotecontrol", RemoteControl);
        }

        //Stops event listening
        private void OnDisable(){
            EventManager.StopListening("coffeebutton", _someListener);
            EventManager.StopListening("remotecontrol", RemoteControl);
        }

        public class State{
            public int AmountOfCues;
            public int AmountOfIntros;
            public AudioClip[] StateCues;
            public bool[] StateCuesBeenPlayed;
            public AudioClip[] StateIntros;
            public bool[] StateIntrosBeenPlayed;

            public int Statenr;


            public State(StatesEnum statesEnum){
                Statenr = (int) statesEnum;


                StateIntros = Resources.LoadAll<AudioClip>("Audio/States/" + statesEnum + "/Intros/");
                StateCues = Resources.LoadAll<AudioClip>("Audio/States/" + statesEnum + "/Cues/");

                AmountOfIntros = StateIntros.Length;
                AmountOfCues = StateCues.Length;

                StateIntrosBeenPlayed = new bool[AmountOfIntros];
                StateCuesBeenPlayed = new bool[AmountOfCues];
            }


            public IEnumerator ResetCues(){
                if (StateCuesBeenPlayed.Length > 0)
                    while (true)
                        for (var i = 0; i < StateCuesBeenPlayed.Length; i++){
                            StateCuesBeenPlayed[i] = false;
                            yield return new WaitForSeconds(20);
                        }
            }
        }
    }
}