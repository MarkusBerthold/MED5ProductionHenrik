using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Assets.Scripts.MessageingSystem;
using UnityEngine.Events;


namespace Assets.Scripts.Timers{
	public class Timers : MonoBehaviour{


		float StartTime,CoffeeTime,SeesRemoteTime,RemoteTime;

		public int state;

		float timer;
		string newfile;

		public bool ShouldListen = true;
		private int doOnceListen = 0;

		private UnityAction _someListener;

		private void Awake(){
			_someListener = Coffee;
		}

		void Start (){
			StartTime = timer;
		}

		void Update(){

			if (ShouldListen && doOnceListen == 0) {
				EventManager.StartListening ("coffeebutton", _someListener);
				EventManager.StartListening ("seesremote", SeesRemote);
				EventManager.StartListening ("remotecontrol", RemoteControl);
				EventManager.StartListening ("seesstereo", SeesStereo);
				EventManager.StartListening ("stereo", Stereo);
				ShouldListen = false;
				doOnceListen = 1;
			}
			timer += Time.deltaTime; 
		}


		public void WriteNewFile(string path,string textToWrite){

			if (File.Exists (path)) {

				string filenr = path.Substring (path.Length-5, 1);
				int newfilenr = int.Parse(filenr);
				newfile = path.Substring (0, path.Length - 5) + (newfilenr+1) + ".txt";
				WriteNewFile (newfile, textToWrite);

			} else {
				System.IO.File.WriteAllText (path, "," + textToWrite);
			}
				
		}

		public void AppendFile(string path,string textToWrite){

			System.IO.File.AppendAllText (path, "," + textToWrite);
		}


		private void Coffee(){
			state = 1;
			EventManager.StopListening ("coffeebutton", _someListener);

			CoffeeTime = timer;
			WriteNewFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "CoffeeTime,"+CoffeeTime+Environment.NewLine);

		}

		private void SeesRemote(){
			if (state == 1) {
				state = 2;
				EventManager.StopListening("seesremote", SeesRemote);
				SeesRemoteTime = timer - CoffeeTime;
				if (newfile != null) {
					AppendFile (newfile, "SeesRemoteTime," + SeesRemoteTime+Environment.NewLine);
				} else {
					AppendFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "SeesRemoteTime," + SeesRemoteTime+Environment.NewLine);
				}
			}
		}


		private void RemoteControl(){
			if (state == 2) {
				state = 3;
				EventManager.StopListening("remotecontrol", RemoteControl);
				RemoteTime = timer - SeesRemoteTime-CoffeeTime;
				if (newfile != null) {
					AppendFile (newfile, "RemoteTime," + RemoteTime+Environment.NewLine);
				} else {
					AppendFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "RemoteTime," + RemoteTime+Environment.NewLine);
				}
			}
		}

		private void SeesStereo(){
			if (state == 3) {
				state = 4;
				EventManager.StopListening("seesstereo", SeesStereo);
			}
		}

		private void Stereo(){
			if (state == 4) {
				state = 5;
				EventManager.StopListening ("stereo", Stereo);
			}
		}

		public void BackFromClock(){
			state = 6;
		}


		public void BackFromStereo(){
			state = 7;
		}

		public void BackFromLight(){
			state = 8;
		}

		public void End(){
			state = 9;
		}
	}
}