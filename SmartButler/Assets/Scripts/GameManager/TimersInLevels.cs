using UnityEngine;
using System.Collections;
using System;
using System.IO;

public class TimersInLevels : MonoBehaviour {

	int filePathLength09;
	int filePathLength1099;

	string newestfilepath;

	public string NameOfLevel;

	// Use this for initialization
	void Start () {
		filePathLength09 = Application.dataPath.Length + 23;
		filePathLength1099 = Application.dataPath.Length + 24;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnApplicationQuit() {
		FindNewestFile (Application.dataPath + "/TimerLogs/testlog0.txt");
		AppendFile (newestfilepath,"Game ended early in "+NameOfLevel+Environment.NewLine);
		AppendFile (newestfilepath,"END OF FILE"+Environment.NewLine);
		
	}
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
	public void AppendFile(string path,string textToWrite){

		System.IO.File.AppendAllText (path, "," + textToWrite);
	}
}
