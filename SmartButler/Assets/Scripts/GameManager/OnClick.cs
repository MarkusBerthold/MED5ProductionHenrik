using UnityEngine;
using System.Collections;
using Assets.Scripts.Timer;

public class OnClick : MonoBehaviour {

	public GameObject Timerss;

	void Start(){
	}


	void OnMouseDown () {
		if(this.name == "wallClock_centerFace")
			Timerss.GetComponent<Timers> ().ClickedClock ();

		if(this.name == "speaker")
			Timerss.GetComponent<Timers> ().ClickedStereo ();


		if(this.name == "remote")
			Timerss.GetComponent<Timers> ().ClickedRemote ();
	
	}

}
