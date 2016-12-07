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
		string newestfilepath,newPathToAppend;
		string [] newestfilelines;

		public bool ShouldListen = true;
		private int doOnceListen = 0;

		private UnityAction _someListener;

		DateTime starttime;

		private void Awake(){
			_someListener = Coffee;
		}

		void Start (){

			starttime = System.DateTime.Now;

			print ("checking if there are any files");
			if (File.Exists (Application.dataPath + "/TimerLogs/testlog0.txt")) {
				FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");
				print ("The newest found file is " + newestfilepath);

				newestfilelines = System.IO.File.ReadAllLines (newestfilepath);

				print ("readin the lines of "+newestfilepath);

				print ("checking what the last line of the file is");
				if (newestfilelines [newestfilelines.Length-1] == "END OF FILE") {
					print ("it ends in END OF FILE");

					int filenr = int.Parse (newestfilepath.Substring (newestfilepath.Length - 5, 1));

					newPathToAppend = newestfilepath.Substring (0, newestfilepath.Length - 5) + (filenr + 1) + ".txt";

					print ("creating a new file at "+newPathToAppend);
					WriteNewFile (newPathToAppend, "Start of game time: " + starttime + Environment.NewLine);
				}else if (newestfilelines [newestfilelines.Length-1] == "LEFT FOR CLOCK") {
					print ("it ends in LEFT FOR CLOCK");
					BackFromClock ();
				} else if (newestfilelines [newestfilelines.Length-1] == "LEFT FOR STEREO") {
					print ("it ends in LEFT FOR STEREO");
					print ("calling backfromstereo()");
					BackFromStereo ();
				} else if (newestfilelines [newestfilelines.Length-1] == "LEFT FOR LIGHT") {
					print ("it ends in LEFT FOR LIGHT");
					BackFromLight ();
				} else {
					return;
				}
			} else {
				print ("there were no files, making file0");
				WriteNewFile (Application.dataPath + "/TimerLogs/testlog0.txt", "Start of game time: " + starttime + Environment.NewLine);
			}
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

		public void FindNewestFile(string path){
			
			string result;

			if (File.Exists (path)) {

				int filenr = int.Parse (path.Substring (path.Length - 5, 1));
				result = path.Substring (0, path.Length - 5) + (filenr+1) + ".txt";

				FindNewestFile (result);
			} else {
				int filenr = int.Parse (path.Substring (path.Length - 5, 1));
				result = path.Substring (0, path.Length - 5) + (filenr-1) + ".txt";
				newestfilepath = result;
			}
		}


		public void WriteNewFile(string path,string textToWrite){

			System.IO.File.WriteAllText (path, "," + textToWrite);
			
				
		}

		public void AppendFile(string path,string textToWrite){

			System.IO.File.AppendAllText (path, "," + textToWrite);
		}


		private void Coffee(){
			state = 1;
			EventManager.StopListening ("coffeebutton", _someListener);

			CoffeeTime = timer;
			if (newPathToAppend != null) {
				AppendFile (newPathToAppend, "CoffeeTime," + CoffeeTime+Environment.NewLine);
			} else {
				AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "CoffeeTime," + CoffeeTime+Environment.NewLine);
			}
		}

		private void SeesRemote(){
			if (state == 1) {
				state = 2;
				EventManager.StopListening("seesremote", SeesRemote);
				SeesRemoteTime = timer - CoffeeTime;
				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "SeesRemoteTime," + SeesRemoteTime+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "SeesRemoteTime," + SeesRemoteTime+Environment.NewLine);
				}
			}
		}


		private void RemoteControl(){
			if (state == 2) {
				state = 3;
				EventManager.StopListening("remotecontrol", RemoteControl);
				RemoteTime = timer - SeesRemoteTime-CoffeeTime;
				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "RemoteTime," + RemoteTime+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "RemoteTime," + RemoteTime+Environment.NewLine);
				}
			}
		}

		private void SeesStereo(){
			if (state == 3) {
				state = 4;
				EventManager.StopListening("seesstereo", SeesStereo);
				SeesStereoTime = timer - SeesRemoteTime-CoffeeTime-RemoteTime;
				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "SeesStereoTime," + SeesStereoTime+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "SeesStereoTime," + SeesStereoTime+Environment.NewLine);
				}
			}
		}

		private void Stereo(){
			if (state == 4) {
				state = 5;
				EventManager.StopListening ("stereo", Stereo);
				StereoTime = timer - SeesRemoteTime-CoffeeTime-RemoteTime-SeesStereoTime;
				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "StereoTime," + StereoTime+Environment.NewLine+"LEFT FOR CLOCK"+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "StereoTime," + StereoTime+Environment.NewLine+"LEFT FOR CLOCK"+Environment.NewLine);
				}
			}
		}

		public void BackFromClock(){
			state = 6;
			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");
			print ("appending "+newestfilepath);
			AppendFile (newestfilepath, "BackFromClock," + DateTime.Now + Environment.NewLine + "LEFT FOR STEREO" + Environment.NewLine);
		}


		public void BackFromStereo(){
			state = 7;
			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");
			print ("appending "+newestfilepath);
			AppendFile (newestfilepath, "BackFromStereo,"+DateTime.Now+Environment.NewLine+"LEFT FOR LIGHT"+Environment.NewLine);
		}

		public void BackFromLight(){
			state = 8;
			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");
			print ("appending "+newestfilepath);
			AppendFile (newestfilepath, "BackFromLight,"+DateTime.Now+Environment.NewLine+"END OF FILE"+Environment.NewLine);
		}
	}
}