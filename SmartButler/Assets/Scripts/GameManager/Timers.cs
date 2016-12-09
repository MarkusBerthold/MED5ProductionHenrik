using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Assets.Scripts.MessageingSystem;
using UnityEngine.Events;


namespace Assets.Scripts.Timer{
	public class Timers : MonoBehaviour{


		int StartTime,StartCoffeeMachine,GoToRemoteControl,StartRemoteControl,GoToStereo,StartStereo,GoToWallClock,
		FromApartmentToStereoLevel,FromApartmentToLightLevel,SystemTimer1,SystemTimer2,SystemTimer3,SystemTimer4,SystemTimer5,
		SystemTime6, ClockLevel,StereoLevel,LightLevel; //timed times we can write to file

		int CoffeeMachineOn,ViewRemoteControl,RemoteControlOn,ViewStereo,StereoOn,ClickedClockTime; //other times

		public int state; 

		string newestfilepath,newPathToAppend; //used for knowing what file to create or append
		string [] newestfilelines; // used to read the content of previous files

		public bool ShouldListen = true;
		private int doOnceListen = 0;

		private UnityAction _someListener;

		DateTime starttime;

		private void Awake(){
			//_someListener = Coffee;
		}

		void Start (){



			print ("checking if there are any files");
			if (File.Exists (Application.dataPath + "/TimerLogs/testlog0.txt")) { //does file0 exist?
				FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");  // if yes, directory is not empty, find the latest file
				 print("The newest found file is " + newestfilepath);

				newestfilelines = System.IO.File.ReadAllLines (newestfilepath); //read all the lines as strings in an array

				print ("readin the lines of "+newestfilepath);

				print ("checking what the last line of the file is");
				if (newestfilelines [newestfilelines.Length-1] == "END OF FILE") { //read the last line, is it END OF FILE?
					print ("it ends in END OF FILE");
					starttime = System.DateTime.Now;  //time when we start the game
					_someListener = Coffee;

					//int lengthofnewestfilepath = newestfilepath.Length;
					//print ("LENGTH OF FILE PATH IS "+lengthofnewestfilepath);

					int filenr = int.Parse (newestfilepath.Substring (newestfilepath.Length - 5, 1)); //if yes, check which number the latest file has

					newPathToAppend = newestfilepath.Substring (0, newestfilepath.Length - 5) + (filenr + 1) + ".txt"; //and make a new one with that number+1

					print ("creating a new file at "+newPathToAppend);
					WriteNewFile (newPathToAppend, "Start of game time," + starttime + Environment.NewLine);
				}else if (newestfilelines [newestfilelines.Length-1].StartsWith("LEFT FOR CLOCK")) { //read the last line, does it start with LEFT FOR CLOCK?
					print ("it ends in LEFT FOR CLOCK");
					BackFromClock (); // we had left for clock, and now we are back
				} else if (newestfilelines [newestfilelines.Length-1].StartsWith("LEFT FROM STEREO")) { //read the last line, does it start with LEFT FOR STEREO?
					print ("it ends in LEFT FOR STEREO");
					print ("calling backfromstereo()");
					BackFromStereo (); // we had left for stereo, and now we are back
				} else if (newestfilelines [newestfilelines.Length-1].StartsWith("LEFT FROM LIGHT")) { //read the last line, does it start with END OF LEFT FOR LIGHT???
					print ("it ends in LEFT FOR LIGHT");
					BackFromLight (); // we had left for light, and now we are back
				} else { 
					return;
				}
			} else { // if there is no file0, then the directory is empty
				starttime = System.DateTime.Now;  //time when we start the game
				_someListener = Coffee;
				print ("there were no files, making file0");
				WriteNewFile (Application.dataPath + "/TimerLogs/testlog0.txt", "Start of game time," + starttime + Environment.NewLine); //create file0
			}
		}

		void Update(){

			// listen to events
			if (ShouldListen && doOnceListen == 0 && _someListener != null) {
				EventManager.StartListening ("coffeebutton", _someListener);
				EventManager.StartListening ("seesremote", SeesRemote);
				EventManager.StartListening ("remotecontrol", RemoteControl);
				EventManager.StartListening ("seesstereo", SeesStereo);
				EventManager.StartListening ("stereo", Stereo);
				ShouldListen = false;
				doOnceListen = 1;
			}
		}

		/**
		 * Function looks for file0, when file0 is the input
		 * Function finds file0 and repeats for file1, et.c recursively 
		 * when it can no longer find another file, it will set newestfilepath to the currect search-1
		 * example, it could find file97, but not file 98, it set newestfilepath to file97
		 */
		public void FindNewestFile(string path){ //is only called with file0
			
			string result;

			if (File.Exists (path)) {  //does fileX exist? yes
					if(path.Length == 88){ //filenumber is 0-9
					int filenr = int.Parse (path.Substring (path.Length - 5, 1)); //get the file number
					result = path.Substring (0, path.Length - 5) + (filenr+1) + ".txt"; //increment the number 
					
					FindNewestFile (result); //call function again with new file path
				}else if(path.Length == 89){ //filenumber sis 10-99
					int filenr = int.Parse (path.Substring (path.Length - 6, 2)); //get the file number
					result = path.Substring (0, path.Length - 6) + (filenr+1) + ".txt"; //increment the number 

					FindNewestFile (result); //call function again with new file path
				}
			} else { //does fileX exist? no
				if(path.Length == 88){ //filenumber is 0-9
					int filenr = int.Parse (path.Substring (path.Length - 5, 1)); //get the file number
					result = path.Substring (0, path.Length - 5) + (filenr-1) + ".txt"; //decrement the number
					newestfilepath = result; //change this variable
				}else if(path.Length == 89){ //filenumber is 10-99
					int filenr = int.Parse (path.Substring (path.Length - 6, 2)); //get the file number
					result = path.Substring (0, path.Length - 6) + (filenr-1) + ".txt"; //decrement the number
					newestfilepath = result; //change this variable
				}
			}
		}

		/**
		 * Function writes a new file at the path with the string
		 */
		public void WriteNewFile(string path,string textToWrite){

			System.IO.File.WriteAllText (path, "," + textToWrite);
			

		/**
		 * Function appends a file at the path with the string
		 */
		}

		public void AppendFile(string path,string textToWrite){

			System.IO.File.AppendAllText (path, "," + textToWrite);
		}


		private void Coffee(){
			state = 1;
			EventManager.StopListening ("coffeebutton", _someListener);

			CoffeeMachineOn = TotalSystemTimeSeconds (DateTime.Now);

			StartCoffeeMachine = CoffeeMachineOn - TotalSystemTimeSeconds(starttime);

			if (newPathToAppend != null) {
				AppendFile (newPathToAppend, "StartCoffeeMachine," + StartCoffeeMachine+Environment.NewLine);
			} else {
				AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "StartCoffeeMachine," + StartCoffeeMachine+Environment.NewLine);
			}
		}

		private void SeesRemote(){
			if (state == 1) {
				state = 2;
				EventManager.StopListening("seesremote", SeesRemote);

				ViewRemoteControl = TotalSystemTimeSeconds (DateTime.Now);

				GoToRemoteControl = ViewRemoteControl-CoffeeMachineOn;

				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "GoToRemoteControl," + GoToRemoteControl+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "GoToRemoteControl," + GoToRemoteControl+Environment.NewLine);
				}
			}
		}


		private void RemoteControl(){
			if (state == 2) {
				state = 3;
				EventManager.StopListening("remotecontrol", RemoteControl);

				RemoteControlOn = TotalSystemTimeSeconds (DateTime.Now);

				StartRemoteControl = RemoteControlOn-ViewRemoteControl;

				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "StartRemoteControl," + StartRemoteControl+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "StartRemoteControl," + StartRemoteControl+Environment.NewLine);
				}
			}
		}

		private void SeesStereo(){
			if (state == 3) {
				state = 4;
				EventManager.StopListening("seesstereo", SeesStereo);

				ViewStereo = TotalSystemTimeSeconds (DateTime.Now);

				print ("first ViewStereo is: "+ViewStereo);

				GoToStereo = ViewStereo-RemoteControlOn;

				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "GoToStereo," + GoToStereo+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "GoToStereo," + GoToStereo+Environment.NewLine);
				}
			}
		}

		private void Stereo(){
			if (state == 4) {
				state = 5;
				EventManager.StopListening ("stereo", Stereo);

				StereoOn = TotalSystemTimeSeconds (DateTime.Now);
				print ("First StereoOn is: "+StereoOn);

				print ("Second ViewStereo is:"+ViewStereo );

				StartStereo = StereoOn - ViewStereo;

				print ("StereoOn minus ViewStereo is"+StartStereo);


				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "StartStereo," + StartStereo+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "StartStereo," + StartStereo+Environment.NewLine);
				}
			}
		}

		public void ClickedClock(){
			if (state == 5) {
				state = 6;

				ClickedClockTime = TotalSystemTimeSeconds (DateTime.Now);

				GoToWallClock = ClickedClockTime - StereoOn;

				if (newPathToAppend != null) {
					AppendFile (newPathToAppend, "GoToWallClock," + GoToWallClock+Environment.NewLine+"LEFT FOR CLOCK AT: "+ClickedClockTime+Environment.NewLine);
				} else {
					AppendFile (Application.dataPath+"/TimerLogs/testlog0.txt", "GoToWallClock," + GoToWallClock+Environment.NewLine+"LEFT FOR CLOCK AT: "+ClickedClockTime+Environment.NewLine);
				}
			}
		}

		public void BackFromClock(){
			state = 7;
			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");
			print ("appending "+newestfilepath);

			int lengthoflastlinenumbers = (newestfilelines [newestfilelines.Length - 1].Length)-18;
			string leftatstring = newestfilelines [newestfilelines.Length - 1].Substring (18,lengthoflastlinenumbers);

			print ("I think that you left for the clock level at: "+leftatstring);
			AppendFile (newestfilepath, "BackFromClock," + DateTime.Now + Environment.NewLine + "LEFT FOR STEREO" + Environment.NewLine);
		}


		public void BackFromStereo(){
			state = 8;
			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");
			print ("appending "+newestfilepath);
			AppendFile (newestfilepath, "BackFromStereo,"+DateTime.Now+Environment.NewLine+"LEFT FOR LIGHT"+Environment.NewLine);
		}

		public void BackFromLight(){
			state = 9;
			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");
			print ("appending "+newestfilepath);
			AppendFile (newestfilepath, "BackFromLight,"+DateTime.Now+Environment.NewLine+"END OF FILE"+Environment.NewLine);
		}

		public int SystemTimeHours(DateTime datetime){
			string firstcut = datetime.ToString();
			string secondcut = firstcut.Substring (11,2);


			return int.Parse(secondcut);
		}

		public int SystemTimeMinutes(DateTime datetime){
			string firstcut = datetime.ToString();
			string secondcut = firstcut.Substring (14,2);


			return int.Parse(secondcut);
		}
		public int SystemTimeSeconds(DateTime datetime){
			string firstcut = datetime.ToString();
			string secondcut = firstcut.Substring (17,2);

			return int.Parse(secondcut);
		}
		public int TotalSystemTimeSeconds(DateTime datetime){

			int hoursToSeconds = (SystemTimeHours (datetime)*60)*60;
			int minutesToSeconds = SystemTimeHours (datetime)*60;

			return hoursToSeconds + minutesToSeconds + SystemTimeSeconds (datetime);
			
		}
	}
}