using UnityEngine;
using System.Collections;
using Assets.Scripts.Timer;

public class OnClick : MonoBehaviour {

	public GameObject Timerss;

	void Start(){
	}


	void OnMouseDown () {

		Timerss.GetComponent<Timers> ().ClickedClock ();
	
	}

}
