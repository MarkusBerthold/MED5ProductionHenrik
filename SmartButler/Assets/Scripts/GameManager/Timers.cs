using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


namespace Assets.Scripts.Timers{
	public class Timers : MonoBehaviour{

		float timer;


		// Use this for initialization
		void Start (){
			//WriteNewFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "yo");
	
		}

		void Update(){
			timer += Time.deltaTime; //Time.deltaTime will increase the value with 1 every second


			if (Input.GetKeyDown (KeyCode.L)) {
				WriteNewFile ("/Users/emil/Documents/git/MED5ProductionHenrik/SmartButler/Assets/TimerLogs/testlog0.txt", "Player pressed L key after "+timer+" seconds");
			}
		}


		public void WriteNewFile(string path,string textToWrite){

			if (File.Exists (path)) {

				string filenr = path.Substring (path.Length-5, 1);
				int newfilenr = int.Parse(filenr);
				string newfile = path.Substring (0, path.Length - 5) + (newfilenr+1) + ".txt";
				WriteNewFile (newfile, textToWrite);

				return;
			} else {
				System.IO.File.WriteAllText (path, "," + textToWrite);
			}
		}
	}
}