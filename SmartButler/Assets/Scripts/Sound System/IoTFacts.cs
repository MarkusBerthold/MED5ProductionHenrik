using UnityEngine;
using System.Collections;
using Assets.Scripts.MessageingSystem;
using UnityEngine.Events;

namespace Assets.Scripts.IoTFactsNS{
	public class IoTFacts : MonoBehaviour{

		private UnityAction _someAction;

		public AudioSource audiosource;
		public AudioClip[] clips;

		// Use this for initialization
		void Start (){
			_someAction = PlayOnExit;
			StartCoroutine (PlayOnEnter ());
			EventManager.StartListening("PuzzleIsSolved2", _someAction);
		}
	
		// Update is called once per frame
		void Update (){
	
		}

		IEnumerator PlayOnEnter (){
			yield return new WaitForSeconds (3);
			audiosource.clip = clips [0];
			audiosource.Play ();
		}

		public void PlayOnExit (){
			audiosource.clip = clips [1];
			audiosource.Play ();
		}
	}
}