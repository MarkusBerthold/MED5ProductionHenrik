using UnityEngine;
using System.Collections;
using Assets.Scripts.MessageingSystem;

public class FallingScript : MonoBehaviour {

	private void OnTriggerEnter(Collider coll){

		if(coll.tag == "Player"){
			EventManager.TriggerEvent ("emmafalls");

		}
	}
}