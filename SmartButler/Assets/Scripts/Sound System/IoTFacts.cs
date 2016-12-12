using UnityEngine;
using System.Collections;
using Assets.Scripts.MessageingSystem;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.IoTFactsNS{
	public class IoTFacts : MonoBehaviour{

		private UnityAction _someAction;

		public GameObject UI;
		private Canvas _uiCanvas;
		public GameObject TextPanel;
		public GameObject FacePanel, ClickEnterPanel;

		public Sprite emmasFace, radioSpeakerFace;

		public string onEnter1, onEnter2, onExit1, onExit2,onExit3;

		public bool isLightLevel;

		// Use this for initialization
		void Start (){
			_uiCanvas = UI.GetComponentInChildren<Canvas>();
			_someAction = PlayOnExit1;

			StartCoroutine (PlayOnEnter1 ());

			EventManager.StartListening("PuzzleIsSolved2", _someAction);
			CloseThoughtBox ();

			if(!isLightLevel)
				ClickEnterPanel.SetActive (true);

			if(isLightLevel)
				StartCoroutine (CloseThoughtBoxDelayed(15));

		}
	
		// Update is called once per frame
		void Update (){

			if( Input.GetKeyDown( KeyCode.Return ) && !isLightLevel && ClickEnterPanel.activeInHierarchy)
				CloseThoughtBox();
	
		}

		public void CloseThoughtBox(){
			if(_uiCanvas.enabled == true){
				_uiCanvas.enabled = false;
				//EventManager.TriggerEvent("EnableControls");
				//Time.timeScale = 1.0f;
			}
		}

		public void OpenThoughtBox(string Speaker){

			if (Speaker == "emma")
				FacePanel.GetComponent<Image> ().sprite = emmasFace;

			if(Speaker == "radio")
				FacePanel.GetComponent<Image> ().sprite = radioSpeakerFace;

			_uiCanvas.enabled = true;

		}

		IEnumerator CloseThoughtBoxDelayed(int delay){
			yield return new WaitForSeconds (delay);
			CloseThoughtBox ();
		}

		IEnumerator PlayOnEnter1 (){
			yield return new WaitForSeconds (3);
			ClickEnterPanel.SetActive (false);
			TextPanel.GetComponent<Text> ().text = onEnter1;
			OpenThoughtBox ("emma");
			StartCoroutine (PlayOnEnter2());
		}

		IEnumerator PlayOnEnter2 (){
			yield return new WaitForSeconds (5);

			if(!isLightLevel)
			ClickEnterPanel.SetActive (true);
			
			CloseThoughtBox ();
			TextPanel.GetComponent<Text> ().text = onEnter2;
			OpenThoughtBox ("emma");
		}



		public void PlayOnExit1 (){
			ClickEnterPanel.SetActive (false);
			TextPanel.GetComponent<Text> ().text = onExit1;
			OpenThoughtBox ("emma");
			StartCoroutine (PlayOnExit2());
		}
		IEnumerator PlayOnExit2 (){
			yield return new WaitForSeconds (6);
			CloseThoughtBox ();
			TextPanel.GetComponent<Text> ().text = onExit2;
			OpenThoughtBox ("emma");

			if (isLightLevel)
				StartCoroutine (PlayOnExit3());
		}

		IEnumerator PlayOnExit3(){
			yield return new WaitForSeconds (4);
			CloseThoughtBox ();
			TextPanel.GetComponent<Text> ().text = onExit3;
			OpenThoughtBox ("emma");
		}
	}
}