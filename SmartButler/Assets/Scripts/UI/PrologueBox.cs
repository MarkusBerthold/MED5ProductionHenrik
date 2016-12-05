using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.MessageingSystem;
using UnityEngine.UI;

public class PrologueBox : MonoBehaviour {

	public GameObject UI;
	private Canvas _uiCanvas;
	private GameObject PrologueBoxPrefab;


	void Awake(){
		PrologueBoxPrefab = Resources.Load("PrologueBoxPrefab") as GameObject;

	}

	// Use this for initialization
	void Start () {		
		if (!(UI = GameObject.FindWithTag("PrologueBox")))
			UI = Instantiate(PrologueBoxPrefab);
		_uiCanvas = UI.GetComponentInChildren<Canvas>();
		_uiCanvas.worldCamera = Camera.main;
		ClosePrologueBox ();
		StartCoroutine (OpenPrologueBox());
	}
	
	// Update is called once per frame
	void Update () {

		if( Input.GetKeyDown( KeyCode.Return ) )
			ClosePrologueBox();
	
	}

	public void ClosePrologueBox(){
		if(_uiCanvas.enabled == true){
		_uiCanvas.enabled = false;
		EventManager.TriggerEvent("EnableControls");
		Time.timeScale = 1.0f;
		}
	}

	public IEnumerator OpenPrologueBox(){
		yield return new WaitForSeconds(1.0f);
		_uiCanvas.enabled = true;
		EventManager.TriggerEvent("DisableControls");
		Time.timeScale = 0f;
	}
}
