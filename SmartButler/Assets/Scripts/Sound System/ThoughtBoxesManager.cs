using System;
using System.Collections;
using Assets.Scripts.MessageingSystem;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.ThoughtBoxes{
	public class ThoughtBoxesManager : MonoBehaviour{
		//Define new states here
		public enum StatesEnum{
			Start,
			Coffee,
			SeesRemote,
			RemoteControl,
			SeesStereo,
			Stereo,
			BackFromClock,
			BackFromStereo,
			BackFromLight,
			End
		}

		private bool _introsHasBeenPlayed;
		private int _returnState;

		private IEnumerator[] coroutines;
		private bool doOncePerState;

		private UnityAction _someListener;
		public int _state;
		public State[] Arrayofstates;
		public GameObject UI;
		private Canvas _uiCanvas;
		public GameObject panel;

		private int EndTime;

		private bool WaitForBroadcast = true;
		private IEnumerator coroutine2;

		public bool ShouldListen = true;
		private int doOnceListen = 0;

		private void Awake(){
			_someListener = Coffee;
		}

		// Use this for initialization
		private void Start(){

			_uiCanvas = UI.GetComponentInChildren<Canvas>();

			//AudioSource = GameObject.FindGameObjectWithTag("Thoughts").GetComponent<AudioSource>();
			var nrOfStates = Enum.GetValues(typeof(StatesEnum)).Length;
			Arrayofstates = new State[nrOfStates];

			coroutines = new IEnumerator[nrOfStates];
			coroutine2 = SetWaitForBroadcastCoroutine();
			StartCoroutine (coroutine2);


			for (var i = 0; i < Arrayofstates.Length; i++)
				Arrayofstates[i] = new State((StatesEnum) i);

			for (var i = 0; i < Arrayofstates.Length; i++)
				coroutines [i] = Arrayofstates [i].ResetCues();

			_uiCanvas.worldCamera = Camera.main;
			CloseThoughtBox ();

		}

		// Update is called once per frame
		private void Update(){

			if( Input.GetKeyDown( KeyCode.Return ) )
				CloseThoughtBox();


			if (ShouldListen && doOnceListen == 0) {
				EventManager.StartListening ("coffeebutton", _someListener);
				EventManager.StartListening ("seesremote", SeesRemote);
				EventManager.StartListening ("remotecontrol", RemoteControl);
				EventManager.StartListening ("seesstereo", SeesStereo);
				EventManager.StartListening ("stereo", Stereo);
				ShouldListen = false;
				doOnceListen = 1;
			}

			if (!WaitForBroadcast) {
				//play intros, some state dont have intros, some "interactions" are intros
				for (var i = 0; i < Arrayofstates [_state].AmountOfIntros; i++)
					if (_uiCanvas.enabled == false && !Arrayofstates [_state].StateIntrosBeenPlayed [i]) {
						panel.GetComponent<Text>().text = Arrayofstates [_state].StateIntros [i];
						OpenThoughtBox ();
						Arrayofstates [_state].StateIntrosBeenPlayed [i] = true;
						_introsHasBeenPlayed = true;
					}



				//play cues
				for (var j = 0; j < Arrayofstates [_state].AmountOfCues; j++)
					if (_uiCanvas.enabled == false && !Arrayofstates [_state].StateCuesBeenPlayed [j] && _introsHasBeenPlayed) {
						int rand = (int)(10 * UnityEngine.Random.Range (0.0f, (Arrayofstates [_state].AmountOfCues / 10f)));
						panel.GetComponent<Text>().text = Arrayofstates [_state].StateCues [rand];
						OpenThoughtBox ();
						Arrayofstates [_state].StateCuesBeenPlayed [j] = true;
						if (!doOncePerState) {
							StartCoroutine (coroutines [_state]);
							doOncePerState = true;
							print ("starting coroutine for reseting cues");
						}
					}
				//if (_state == 8) {
				//	End ();
				//	EndTime++;
				//}
			}
		}
		public void CloseThoughtBox(){
			if(_uiCanvas.enabled == true){
				_uiCanvas.enabled = false;
				EventManager.TriggerEvent("EnableControls");
				Time.timeScale = 1.0f;
			}
		}

		public void OpenThoughtBox(){
			//yield return new WaitForSeconds(1.0f);
			_uiCanvas.enabled = true;
			EventManager.TriggerEvent("DisableControls");
			Time.timeScale = 0f;
		}

		/// <summary>
		/// Sets the _state to 1
		/// </summary>
		/// Start,
		private void Coffee(){
			Debug.Log("coffeebutton was pressed");
			_state = 1;
			EventManager.StopListening ("coffeebutton", _someListener);
			doOncePerState = false;
			//AudioSource.Stop ();
		}

		private void SeesRemote(){
			Debug.Log("Remote Control was seen");
			if (_state == 1) {
				_state = 2;
				EventManager.StopListening("seesremote", SeesRemote);
			}
			doOncePerState = false;
			//AudioSource.Stop ();
		}

		/// <summary>
		/// Sets the _state to 2
		/// </summary>
		private void RemoteControl(){
			Debug.Log("Remote Control was pressed");
			if (_state == 2) {
				_state = 3;
				EventManager.StopListening("remotecontrol", RemoteControl);
			}
			doOncePerState = false;
			//AudioSource.Stop ();

		}

		private void SeesStereo(){
			Debug.Log("Stereo was seen");
			if (_state == 3) {
				_state = 4;
				EventManager.StopListening("seesstereo", SeesStereo);
			}
			doOncePerState = false;
			//AudioSource.Stop ();
		}

		private void Stereo(){
			Debug.Log("Stereo was pressed");
			if (_state == 4) {
				_state = 5;
				EventManager.StopListening ("stereo", Stereo);
			}
			doOncePerState = false;
			//AudioSource.Stop ();
		}

		public void BackFromClock(){
			_state = 6;
			EndTime++;
			doOncePerState = false;
			//AudioSource.Stop ();
			StopListening ();
		}


		public void BackFromStereo(){
			_state = 7;
			EndTime++;
			doOncePerState = false;
			//AudioSource.Stop ();
			StopListening ();
		}

		public void BackFromLight(){
			_state = 8;
			EndTime++;
			doOncePerState = false;
			//AudioSource.Stop ();
			StopListening ();
		}

		public void End(){
			_state = 9;
			doOncePerState = false;
			//AudioSource.Stop ();
		}

		//Starts event listening
		private void OnEnable(){
			//	EventManager.StartListening("coffeebutton", _someListener);
			//	EventManager.StartListening("seesremote", SeesRemote);
			//	EventManager.StartListening("remotecontrol", RemoteControl);
			//	EventManager.StartListening("seesstereo", SeesStereo);
			//	EventManager.StartListening("stereo", Stereo);
			//EventManager.StartListening("backfromclock", BackFromClock);
		}

		//Stops event listening
		private void OnDisable(){
			EventManager.StopListening("coffeebutton", _someListener);
			EventManager.StopListening("seesremote", SeesRemote);
			EventManager.StopListening("remotecontrol", RemoteControl);
			EventManager.StopListening("seesstereo", SeesStereo);
			EventManager.StopListening("stereo", Stereo);
			//EventManager.StopListening("backfromclock", BackFromClock);
		}
		public IEnumerator SetWaitForBroadcastCoroutine(){

			yield return new WaitForSeconds(36);
			SetWaitForBroadcast (false);
		}
		public void SetWaitForBroadcast(bool x){
			WaitForBroadcast = x;
		}

		public bool GetWaitForBroadcast(){
			return WaitForBroadcast;
		}

		public void StopListening(){
			EventManager.StopListening("coffeebutton", _someListener);
			EventManager.StopListening("seesremote", SeesRemote);
			EventManager.StopListening("remotecontrol", RemoteControl);
			EventManager.StopListening("seesstereo", SeesStereo);
			EventManager.StopListening("stereo", Stereo);
			print ("STOP LISTENING");

		}

		public class State{
			public int AmountOfCues;
			public int AmountOfIntros;
			public TextAsset[] StateCuesAssets;
			public string[] StateCues;
			public bool[] StateCuesBeenPlayed;
			public TextAsset[] StateIntrosAssets;
			public string[] StateIntros;
			public bool[] StateIntrosBeenPlayed;

			public int Statenr;



			public State(StatesEnum statesEnum){
				Statenr = (int) statesEnum;



				StateIntrosAssets = Resources.LoadAll<TextAsset>("Text/States/" + statesEnum + "/Intros/");
				StateCuesAssets = Resources.LoadAll<TextAsset>("Text/States/" + statesEnum + "/Cues/");

				StateIntros = new string[StateIntrosAssets.Length];
				StateCues = new string[StateCuesAssets.Length];

				if(StateIntrosAssets.Length != 0)
				for(int i = 0; i < StateIntrosAssets.Length; i++){
					StateIntros[i] = StateIntrosAssets[i].text;
				}
				
				if(StateCuesAssets.Length != 0)
				for(int i = 0; i < StateCuesAssets.Length; i++){
					StateCues[i] = StateCuesAssets[i].text;
				}

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