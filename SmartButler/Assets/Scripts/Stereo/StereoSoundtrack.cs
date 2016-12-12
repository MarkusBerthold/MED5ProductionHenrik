using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Assets.Scripts.MessageingSystem;

public class StereoSoundtrack : MonoBehaviour {
	private UnityAction _someListener;

	public AudioSource Tyrell;
	public AudioSource Base;
	public AudioSource Kick;
	public AudioSource Piano;
	public AudioSource Pad;
	public AudioSource FallingSource;

	public AudioClip [] pianoclips = new AudioClip[3];
	public AudioClip [] kickclips = new AudioClip[2];


	IEnumerator coroutine1;
	IEnumerator coroutine2;


	void Awake(){
		_someListener = AddBase;
	}
	// Use this for initialization
	void Start () {
		Base.mute = true;
		Kick.mute = true;
		Piano.mute = true;
		Pad.mute = true;

		coroutine1 = AddKick ();
		coroutine2 = AddPad ();
	
	
	}

	void OnEnable(){

		EventManager.StartListening ("PuzzleIsSolved1", _someListener);
		EventManager.StartListening ("PuzzleIsSolved2", AddPiano);
		EventManager.StartListening ("emmafalls", Falling);
	}

	void OnDisable(){
		EventManager.StopListening ("PuzzleIsSolved1", _someListener);
		EventManager.StopListening ("PuzzleIsSolved2", AddPiano);
		EventManager.StopListening ("emmafalls", Falling);
	}
	// Update is called once per frame
	void Update () {
		
		if (!Tyrell.isPlaying) {

			int pianorand = (int) (10 * Random.Range (0, 0.3f));
			int kickrand = (int) (10 * Random.Range (0, 0.2f));
			Piano.clip = pianoclips [pianorand];
			Kick.clip = kickclips [kickrand];

			Tyrell.Play ();
			Base.Play ();
			Kick.Play ();
			Piano.Play ();
			Pad.Play ();
		}
	}

	public void AddBase(){
		Base.mute = false;
		StartCoroutine(coroutine1);
	}
		

	public IEnumerator AddKick(){

		yield return new WaitForSeconds(30);
		Kick.mute = false;
	}

	public void AddPiano(){
		Piano.mute = false;
		StartCoroutine (coroutine2);

	}

	public IEnumerator AddPad(){
		yield return new WaitForSeconds(20);
		Pad.mute = false;
	}
	public void Falling(){
		FallingSource.Play ();
	}
}
