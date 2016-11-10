using UnityEngine;
using System.Collections;

namespace Assets.Scripts.LivingRoom.LivingRoomSoundtrack{
	public class LivingRoomSoundtrack : MonoBehaviour {


		string State;

		public AudioSource Melody;
		public AudioSource DrumA;
		public AudioSource DrumB;
		public AudioSource Pad;
		public AudioSource Noise;

		public AudioClip MelodyClipA;
		public AudioClip MelodyClipB;
		public AudioClip PadA;
		public AudioClip PadB;

		private bool WaitForBroadcast = true;
		private IEnumerator coroutine;

		// Use this for initialization
		void Start () {

			coroutine = SetWaitForBroadcastCoroutine ();
			StartCoroutine (coroutine);
	
		}
	
		// Update is called once per frame
		void Update () {

			switch(State){
			case "Start":
				
				if (!WaitForBroadcast) {
					print ("testaweawdawd");
					Melody.clip = MelodyClipA;
					Pad.clip = PadA;
					if (!Melody.isPlaying)
						Melody.Play ();
					if (!Pad.isPlaying)
						Pad.Play ();
				}
					break;
				
			case "Clock":
				Melody.clip = MelodyClipA;
				Pad.clip = PadA;
				if (!Melody.isPlaying) {
					Melody.Play ();
					DrumA.Play ();
				}
				if (!Pad.isPlaying)
					Pad.Play ();
				break;
			case "Stereo":
				Melody.clip = MelodyClipA;
				Pad.clip = PadA;
				if (!Melody.isPlaying) {
					Melody.Play ();
					DrumA.Play ();
					DrumB.Play ();
				}
				if (!Pad.isPlaying)
					Pad.Play ();

				break;
			case "Light":

				Melody.clip = MelodyClipB;
				Pad.clip = PadB;
				if (!Melody.isPlaying) {
					Melody.Play ();
					DrumA.Play ();
					DrumB.Play ();
					Noise.Play ();
				}
				if (!Pad.isPlaying)
					Pad.Play ();
				break;
				
			}
	
		}

		public IEnumerator SetWaitForBroadcastCoroutine(){
			yield return new WaitForSeconds(33);
			WaitForBroadcast = false;
		}

		public void SetState(string state){

			State = state;
		}
	}
}