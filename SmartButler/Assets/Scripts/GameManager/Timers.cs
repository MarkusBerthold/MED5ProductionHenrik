using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Assets.Scripts.MessageingSystem;
using UnityEngine.Events;


namespace Assets.Scripts.Timer{
	public class Timers : MonoBehaviour{


		float StartTime,CoffeeTime,SeesRemoteTime,RemoteTime,SeesStereoTime,StereoTime,BackFromClockTime;

		public int state;

		float timer;
		string newfile;

		public bool ShouldListen = true;
		private int doOnceListen = 0;

		private UnityAction _someListener;

		DateTime starttime;

		private void Awake(){
			_someListener = Coffee;
		}

		void Start (){
			starttime = System.DateTime.Now;
			WriteNewFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "Start of game time: " + starttime+Environment.NewLine);
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
			if (newfile != null) {
				AppendFile (newfile, "CoffeeTime," + CoffeeTime+Environment.NewLine);
			} else {
				AppendFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "CoffeeTime," + CoffeeTime+Environment.NewLine);
			}
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
				SeesStereoTime = timer - SeesRemoteTime-CoffeeTime-RemoteTime;
				if (newfile != null) {
					AppendFile (newfile, "SeesStereoTime," + SeesStereoTime+Environment.NewLine);
				} else {
					AppendFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "SeesStereoTime," + SeesStereoTime+Environment.NewLine);
				}
			}
		}

		private void Stereo(){
			if (state == 4) {
				state = 5;
				EventManager.StopListening ("stereo", Stereo);
				StereoTime = timer - SeesRemoteTime-CoffeeTime-RemoteTime-SeesStereoTime;
				if (newfile != null) {
					AppendFile (newfile, "StereoTime," + StereoTime+Environment.NewLine);
				} else {
					AppendFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "StereoTime," + StereoTime+Environment.NewLine);
				}
			}
		}

		public void BackFromClock(){
			state = 6;
//			BackFromClockTime = starttime - SeesRemoteTime-CoffeeTime;
			if (newfile != null) {
				AppendFile (newfile, "BackFromClockTime," + RemoteTime+","+DateTime.Now+Environment.NewLine);
			} else {
				AppendFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "RemoteTime," + RemoteTime+","+DateTime.Now+Environment.NewLine);
			}
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