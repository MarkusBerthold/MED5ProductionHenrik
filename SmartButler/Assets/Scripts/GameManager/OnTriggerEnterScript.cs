using UnityEngine;
using System.Collections;
using Assets.Scripts.IoTFactsNS;

public class OnTriggerEnterScript : MonoBehaviour {

	public GameObject IoTFact;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void OnTriggerEnter(){
		IoTFact.GetComponent<IoTFacts> ().PlayOnExit1 ();
	}
}
