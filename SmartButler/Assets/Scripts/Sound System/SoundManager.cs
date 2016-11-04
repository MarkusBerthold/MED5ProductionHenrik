﻿using System;
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
			SeesRemote,
			RemoteControl,
			SeesStereo,
			Stereo,
			BackFromClock,
			BackFromLight,
			BackFromStereo,
			End
		}

		private bool _introsHasBeenPlayed;
		private int _returnState;

		private IEnumerator[] coroutines;
		private bool doOncePerState;

		private UnityAction _someListener;
		public int _state;
		public State[] Arrayofstates;
		public AudioSource AudioSource;

		private int EndTime;

		private void Awake(){
			_someListener = Coffee;
			//DontDestroyOnLoad (transform.gameObject);
		}

		// Use this for initialization
		private void Start(){

			AudioSource = GameObject.FindGameObjectWithTag("Thoughts").GetComponent<AudioSource>();
			var nrOfStates = Enum.GetValues(typeof(StatesEnum)).Length;
			Arrayofstates = new State[nrOfStates];

			coroutines = new IEnumerator[nrOfStates];

			for (var i = 0; i < Arrayofstates.Length; i++)
				Arrayofstates[i] = new State((StatesEnum) i);

			for (var i = 0; i < Arrayofstates.Length; i++)
				coroutines [i] = Arrayofstates [i].ResetCues();

			/**
			 * if (mortens code == hasBeenToClock)
			 * _state = 5;
			*/
		}

		// Update is called once per frame
		private void Update(){
			//play intros, some state dont have intros, some "interactions" are intros
			for (var i = 0; i < Arrayofstates[_state].AmountOfIntros; i++)
				if (!AudioSource.isPlaying && !Arrayofstates[_state].StateIntrosBeenPlayed[i]){
					AudioSource.clip = Arrayofstates[_state].StateIntros[i];
					AudioSource.Play();
					Arrayofstates[_state].StateIntrosBeenPlayed[i] = true;
					_introsHasBeenPlayed = true;
				}



			//play cues
			for (var j = 0; j < Arrayofstates[_state].AmountOfCues; j++)
				if (!AudioSource.isPlaying && !Arrayofstates[_state].StateCuesBeenPlayed[j] && _introsHasBeenPlayed){
					int rand = (int)(10 * UnityEngine.Random.Range (0.0f, (Arrayofstates[_state].AmountOfCues / 10f)));
					AudioSource.clip = Arrayofstates[_state].StateCues[j];
					AudioSource.Play();
					Arrayofstates[_state].StateCuesBeenPlayed[j] = true;
					if (!doOncePerState) {
						StartCoroutine (coroutines [_state]);
						doOncePerState = true;
						print("starting coroutine for reseting cues");
					}
				}
			if (EndTime == 3) {
				End ();
				EndTime++;
			}
		}

		/// <summary>
		/// Sets the _state to 1
		/// </summary>
		/// Start,
		private void Coffee(){
			Debug.Log("coffeebutton was pressed");
			_state = 1;
			doOncePerState = false;
		}

		private void SeesRemote(){
			Debug.Log("Remote Control was seen");
			if (_state == 1) {
				_state = 2;
				EventManager.StopListening ("coffeebutton", _someListener);
			}
			doOncePerState = false;
		}

		/// <summary>
		/// Sets the _state to 2
		/// </summary>
		private void RemoteControl(){
			Debug.Log("Remote Control was pressed");
			if (_state == 2) {
				_state = 3;
				EventManager.StopListening ("seeingremote", _someListener);
			}
			doOncePerState = false;

		}

		private void SeesStereo(){
			Debug.Log("Stereo was seen");
			if (_state == 3) {
				_state = 4;
				EventManager.StopListening ("remotecontrol", _someListener);
			}
			doOncePerState = false;
		}

		private void Stereo(){
			Debug.Log("Stereo was pressed");
			if (_state == 4) {
				_state = 5;
				EventManager.StopListening ("stereo", _someListener);
			}
			doOncePerState = false;
		}
	
		public void BackFromClock(){
			_state = 6;
			EndTime++;
			doOncePerState = false;
		}
		public void BackFromLight(){
			_state = 7;
			EndTime++;
			doOncePerState = false;
		}
		public void BackFromStereo(){
			_state = 8;
			EndTime++;
			doOncePerState = false;
		}
		private void End(){
			_state = 9;
			doOncePerState = false;
		}

		//Starts event listening
		private void OnEnable(){
			EventManager.StartListening("coffeebutton", _someListener);
			EventManager.StartListening("remotecontrol", RemoteControl);
			//EventManager.StartListening("backfromclock", BackFromClock);
		}

		//Stops event listening
		private void OnDisable(){
			EventManager.StopListening("coffeebutton", _someListener);
			EventManager.StopListening("remotecontrol", RemoteControl);
			//EventManager.StopListening("backfromclock", BackFromClock);
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
							yield return new WaitForSeconds(20);
							StateCuesBeenPlayed[i] = false;
						}
				
			}
				

		}

	}

}