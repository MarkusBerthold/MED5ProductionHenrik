using UnityEngine;
using System.Collections;

namespace Assets.Scripts.IoTFactsNS{
	public class IoTFacts : MonoBehaviour{

		public AudioSource audiosource;
		public AudioClip[] clips;

		// Use this for initialization
		void Start (){
			StartCoroutine (PlayOnEnter ());
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