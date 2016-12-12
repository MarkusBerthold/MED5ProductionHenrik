using UnityEngine;
using System.Collections;
using System;
using System.IO;
using Assets.Scripts.MessageingSystem;
using UnityEngine.Events;
using System.Globalization;


namespace Assets.Scripts.Timer{
	public class Timers : MonoBehaviour{

		int filePathLength09;
		int filePathLength1099;

		int StartTime,StartCoffeeMachine,GoToRemoteControl,StartRemoteControl,GoToStereo,StartStereo,GoToWallClock,
		FromApartmentToStereoLevel,FromApartmentToLightLevel,SystemTimer1,SystemTimer2,SystemTimer3,SystemTimer4,SystemTimer5,
		SystemTimer6, ClockLevel,StereoLevel,LightLevel; //timed times we can write to file

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
			if (!System.IO.Directory.Exists (Application.dataPath + "/TimerLogs/")) {
				System.IO.Directory.CreateDirectory (Application.dataPath + "/TimerLogs/");
			}
		}

		void Start (){


			starttime = DateTime.Now;

			filePathLength09 = Application.dataPath.Length + 23;
			filePathLength1099 = Application.dataPath.Length + 24;

			print ("checking if there are any files");
			if (File.Exists (Application.dataPath + "/TimerLogs/testlog0.txt")) { //does file0 exist?
				FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");  // if yes, directory is not empty, find the latest file
				 print("The newest found file is " + newestfilepath);

				newestfilelines = System.IO.File.ReadAllLines (newestfilepath); //read all the lines as strings in an array

				print ("readin the lines of "+newestfilepath);

				print ("checking what the last line of the file is");
				if (newestfilelines [newestfilelines.Length-1] == ",END OF FILE") { //read the last line, is it END OF FILE?
					print ("it ends in END OF FILE");
					//starttime = System.DateTime.Now;  //time when we start the game
					//print("initial starttime: "+TotalSystemTimeSeconds(starttime));
					_someListener = Coffee;

					//int lengthofnewestfilepath = newestfilepath.Length;
					//print ("LENGTH OF FILE PATH IS "+lengthofnewestfilepath);
					int filenr;
					print ("file path length: "+newestfilepath.Length);

					if (newestfilepath.Length == filePathLength09) {
						filenr = int.Parse (newestfilepath.Substring (newestfilepath.Length - 5, 1)); //if yes, check which number the latest file has
					} else if (newestfilepath.Length == filePathLength1099) {
						filenr = int.Parse (newestfilepath.Substring (newestfilepath.Length - 6, 2)); //if yes, check which number the latest file has
					} else {
						filenr = 666;
					}

					if (newestfilepath.Length == filePathLength09) {
						newPathToAppend = newestfilepath.Substring (0, newestfilepath.Length - 5) + (++filenr) + ".txt"; //and make a new one with that number+1
					} else if (newestfilepath.Length == filePathLength1099) {
						newPathToAppend = newestfilepath.Substring (0, newestfilepath.Length - 6) + (++filenr) + ".txt"; //and make a new one with that number+1
					}

					print ("creating a new file at "+newPathToAppend);
					WriteNewFile (newPathToAppend, "Start of game time," + TotalSystemTimeSeconds(starttime)+" "+DateTime.Now.ToString(new CultureInfo("en-GB")) + Environment.NewLine);
				}else if (newestfilelines [newestfilelines.Length-1].StartsWith("LEFT FOR CLOCK")) { //read the last line, does it start with LEFT FOR CLOCK?
					print ("it ends in LEFT FOR CLOCK");
					BackFromClock (); // we had left for clock, and now we are back
				} else if (newestfilelines [newestfilelines.Length-1].StartsWith("LEFT FOR STEREO")) { //read the last line, does it start with LEFT FOR STEREO?
					print ("it ends in LEFT FOR STEREO");
					print ("calling backfromstereo()");
					BackFromStereo (); // we had left for stereo, and now we are back
				} else if (newestfilelines [newestfilelines.Length-1].StartsWith("LEFT FOR LIGHT")) { //read the last line, does it start with END OF LEFT FOR LIGHT???
					print ("it ends in LEFT FOR LIGHT");
					BackFromLight (); // we had left for light, and now we are back
				} else { 
					return;
				}
			} else { // if there is no file0, then the directory is empty
				//starttime = System.DateTime.Now;  //time when we start the game
				//print("initial starttime: "+TotalSystemTimeSeconds(starttime));
				_someListener = Coffee;
				print ("there were no files, making file0");
				WriteNewFile (Application.dataPath + "/TimerLogs/testlog0.txt", "Start of game time," + TotalSystemTimeSeconds( starttime)+" "+DateTime.Now.ToString(new CultureInfo("en-GB")) + Environment.NewLine); //create file0
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
				if(path.Length == filePathLength09){ //filenumber is 0-9 this is hardcoded and might lead to issues
					int filenr = int.Parse (path.Substring (path.Length - 5, 1)); //get the file number
					result = path.Substring (0, path.Length - 5) + (++filenr) + ".txt"; //increment the number 
					
					FindNewestFile (result); //call function again with new file path
				}else if(path.Length == filePathLength1099){ //filenumber is 10-99 this is hardcoded and might lead to issues
					int filenr = int.Parse (path.Substring (path.Length - 6, 2)); //get the file number
					print("file number is "+filenr);
					result = path.Substring (0, path.Length - 6) + (++filenr) + ".txt"; //increment the number 

					FindNewestFile (result); //call function again with new file path
				}
			} else { //does fileX exist? no
				if(path.Length == filePathLength09){ //filenumber is 0-9 this is hardcoded and might lead to issues
					int filenr = int.Parse (path.Substring (path.Length - 5, 1)); //get the file number
					print("file number "+filenr+" does not exist, going back one step");
					result = path.Substring (0, path.Length - 5) + (--filenr) + ".txt"; //decrement the number

					newestfilepath = result; //change this variable
					print("newest file path is "+newestfilepath);
				}else if(path.Length == filePathLength1099){ //filenumber is 10-99 this is hardcoded and might lead to issues
					int filenr = int.Parse (path.Substring (path.Length - 6, 2)); //get the file number
					print("file number "+filenr+" does not exist, going back one step");
					result = path.Substring (0, path.Length - 6) + (--filenr) + ".txt"; //decrement the number
					newestfilepath = result; //change this variable
					print("newest file path is "+newestfilepath);
				}
			}
		}

		/**
		 * Function writes a new file at the path with the string
		 */
		public void WriteNewFile(string path,string textToWrite){

			System.IO.File.WriteAllText (path, "," + textToWrite);
		}
		public void WriteNewCleanFile(string path,string textToWrite){

			System.IO.File.WriteAllText (path, textToWrite+Environment.NewLine);
		}
		/**
		 * Function appends a file at the path with the string
		 */

		public void AppendFile(string path,string textToWrite){

			System.IO.File.AppendAllText (path, "," + textToWrite);
		}

		public void AppendCleanFile(string path,string textToWrite){

			System.IO.File.AppendAllText (path, textToWrite+Environment.NewLine);
		}


		private void Coffee(){
			state = 1;
			EventManager.StopListening ("coffeebutton", _someListener);

			CoffeeMachineOn = TotalSystemTimeSeconds (DateTime.Now);
			print ("CoffeeMachineOn: "+CoffeeMachineOn);
			print ("StartTime: "+TotalSystemTimeSeconds(starttime));

			StartCoffeeMachine = CoffeeMachineOn - TotalSystemTimeSeconds(starttime);

			print ("CoffeeMachineOn minus StartTime is: "+StartCoffeeMachine);
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
				print ("ViewRemoteControl: "+ViewRemoteControl);
				print ("CoffeeMachineOn: "+CoffeeMachineOn);

				GoToRemoteControl = ViewRemoteControl-CoffeeMachineOn;
				print ("ViewRemoteControl minus CoffeeMachineOn is: "+GoToRemoteControl);

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
			//print ("appending "+newestfilepath);

			int lengthoflastlinenumbers = (newestfilelines [newestfilelines.Length - 1].Length)-18;
			int leftat = int.Parse(newestfilelines [newestfilelines.Length - 1].Substring (18,lengthoflastlinenumbers));

			SystemTimer1 = leftat;
			SystemTimer2 = TotalSystemTimeSeconds (DateTime.Now);
			AppendFile (newestfilepath, "SystemTimer1," + SystemTimer1 + Environment.NewLine);
			AppendFile (newestfilepath, "SystemTimer2," + SystemTimer2 + Environment.NewLine);
		}

		public void ClickedStereo(){
			if (state == 7) {
				SystemTimer3 = TotalSystemTimeSeconds (DateTime.Now);
				AppendFile (newestfilepath, "SystemTimer3," + SystemTimer3 + Environment.NewLine);
				int FromApartmentToStereo = TotalSystemTimeSeconds (DateTime.Now) - SystemTimer2;
				AppendFile (newestfilepath, "FromApartmentToStereo," + FromApartmentToStereo + Environment.NewLine+"LEFT FOR STEREO AT: "+TotalSystemTimeSeconds (DateTime.Now)+Environment.NewLine);
			}
		}


		public void BackFromStereo(){
			state = 8;
			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");

			int lengthoflastlinenumbers = (newestfilelines [newestfilelines.Length - 1].Length)-19;
			int leftat = int.Parse(newestfilelines [newestfilelines.Length - 1].Substring (19,lengthoflastlinenumbers));

			SystemTimer4 = TotalSystemTimeSeconds (DateTime.Now);
			AppendFile (newestfilepath, "SystemTimer4," + SystemTimer4 + Environment.NewLine);
			print ("I think you left for stereo at: "+leftat);

		}

		public void ClickedRemote(){
			if (state == 8) {
				state = 9;
				SystemTimer5 = TotalSystemTimeSeconds (DateTime.Now);
				AppendFile (newestfilepath, "SystemTimer5," + SystemTimer5 + Environment.NewLine);
				int FromApartmentToLight = TotalSystemTimeSeconds (DateTime.Now) - SystemTimer4;
				AppendFile (newestfilepath, "FromApartmentToLight," + FromApartmentToLight + Environment.NewLine+"LEFT FOR LIGHT AT: "+TotalSystemTimeSeconds (DateTime.Now)+Environment.NewLine);
			}
		}

		public void BackFromLight(){
			state = 10;
			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");

			int lengthoflastlinenumbers = (newestfilelines [newestfilelines.Length - 1].Length)-18;
			int leftat = int.Parse(newestfilelines [newestfilelines.Length - 1].Substring (18,lengthoflastlinenumbers));

			SystemTimer6 = TotalSystemTimeSeconds (DateTime.Now);
			AppendFile (newestfilepath, "SystemTimer6," + SystemTimer6 + Environment.NewLine);
			print ("I think you left for light at: "+leftat);

			AppendFile (newestfilepath,"END OF FILE"+Environment.NewLine);
			CleanUpFile (newestfilepath);
		}
		void CleanUpFile(string path){

			int TimeInClockLevel, TimeInStereoLevel, TimeInLightLevel, StartedGame, TotalGameTime;

			newestfilelines = System.IO.File.ReadAllLines (newestfilepath); //read all the lines as strings in an array

			string[] cleanfile = new string[newestfilelines.Length-5]; // i think 19 - 5 = 14


			SystemTimer1 = int.Parse(newestfilelines [8].Substring (14,5));
			SystemTimer2 = int.Parse(newestfilelines [9].Substring (14,5));
			SystemTimer3 = int.Parse(newestfilelines [10].Substring (14,5));
			SystemTimer4 = int.Parse(newestfilelines [13].Substring (14,5));
			SystemTimer5 = int.Parse(newestfilelines [14].Substring (14,5));
			SystemTimer6 = int.Parse(newestfilelines [17].Substring (14,5));

			StartedGame = int.Parse(newestfilelines [0].Substring (20,5));
			TotalGameTime = TotalSystemTimeSeconds (DateTime.Now) - StartedGame;

			TimeInClockLevel = SystemTimer2 - SystemTimer1;
			TimeInStereoLevel = SystemTimer4 - SystemTimer3;
			TimeInLightLevel = SystemTimer6 - SystemTimer5;

			cleanfile [0] = newestfilelines [0]; //writing start of game
			cleanfile [1] = newestfilelines [1]; //writing StartCoffeeMachine
			cleanfile [2] = newestfilelines [2]; //writing GoToRemoteControl
			cleanfile [3] = newestfilelines [3]; //writing StartRemoteControl
			cleanfile [4] = newestfilelines [4]; //writing GoToStereo
			cleanfile [5] = newestfilelines [5]; //writing StartStereo
			cleanfile [6] = newestfilelines [6]; //writing GoToWallClock
			cleanfile [7] = ",TimeInClockLevel," + TimeInClockLevel; //writing time in clock level
			cleanfile [8] = newestfilelines [11]; //writing FromApartmentToStereo
			cleanfile [9] = ",TimeInStereoLevel," + TimeInStereoLevel;
			cleanfile [10] = newestfilelines [15]; //writing FromApartmentToLight
			cleanfile [11] = ",TimeInLightLevel," + TimeInLightLevel;
			cleanfile [12] = ",TotalTimeInGame," + TotalGameTime;
			cleanfile [13] = newestfilelines [newestfilelines.Length - 1]; //end of file

			System.IO.File.Delete (newestfilepath);

			//write the first line as a new file
			WriteNewCleanFile (newestfilepath, cleanfile[0]);

			for (int i = 1; i < cleanfile.Length; i++){
				//append the rest of the lines
				AppendCleanFile (newestfilepath, cleanfile [i]);
			}

		}

		public int SystemTimeHours(DateTime datetime){
			string firstcut = datetime.ToString(new CultureInfo("en-GB"));
			string secondcut = firstcut.Substring (11,2); //extract hours, should work every time

			print ("input "+datetime.ToString(new CultureInfo("en-GB")));
			print ("cutting at 11, 2 steps");

			print ("trying to parse "+secondcut);
			return int.Parse(secondcut); //convert string to int, 07 -> 7, 00 ->0 et.c
		}

		public int SystemTimeMinutes(DateTime datetime){
			string firstcut = datetime.ToString(new CultureInfo("en-GB"));
			string secondcut = firstcut.Substring (14,2);


			return int.Parse(secondcut);
		}
		public int SystemTimeSeconds(DateTime datetime){
			string firstcut = datetime.ToString(new CultureInfo("en-GB"));
			string secondcut = firstcut.Substring (17,2);

			return int.Parse(secondcut);
		}
		public int TotalSystemTimeSeconds(DateTime datetime){

			int hoursToSeconds = (SystemTimeHours (datetime)*60)*60;
			int minutesToSeconds = SystemTimeMinutes (datetime)*60;
		

			return hoursToSeconds + minutesToSeconds + SystemTimeSeconds (datetime);
			
		}
		void OnApplicationQuit() {

			FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");  // if yes, directory is not empty, find the latest file

			newestfilelines = System.IO.File.ReadAllLines (newestfilepath); //read all the lines as strings in an array

			if(newestfilelines[newestfilelines.Length-1] != ",END OF FILE"){
				AppendFile (newestfilepath,"Game ended early"+Environment.NewLine);
				AppendFile (newestfilepath,"END OF FILE"+Environment.NewLine);
			}

			Application.OpenURL("https://goo.gl/forms/XwVz5raNFZF7Xl1O2");

		}
	}
}