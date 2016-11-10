using UnityEngine;
using System.Collections;

namespace Assets.Scripts.LivingRoom.BroadcastSpeaker{
public class BroadcastSpeaker : MonoBehaviour {

	public AudioSource AudioSource;

	private bool ShouldPlay;

	// Use this for initialization
	void Start () {
	

	}
	
	// Update is called once per frame
	void Update () {

			if (GetShouldPlay()) {
				AudioSource.Play ();
				SetShouldPlay (false);
			}
	
	}

		public void SetShouldPlay (bool x){

			ShouldPlay = x;
		}

		public bool GetShouldPlay(){

			return ShouldPlay;
		}
	}
}