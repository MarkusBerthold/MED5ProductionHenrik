using UnityEngine;
using System.Collections;
using UnityEngine.Events;
using Assets.Scripts.MessageingSystem;

public class ClockSoundtrack : MonoBehaviour {

	private UnityAction _someListener;

	public AudioClip [] startsounds = new AudioClip[2];

	public AudioSource BassAudioSource;
	public AudioSource GlassAudioSource;
	public AudioSource GearRotateClip;
	public AudioSource Kick;
	public AudioSource Chimes;
	public AudioSource Bells;
	public AudioSource BoneCrackFile;

	private IEnumerator coroutine;

	void Awake(){
		_someListener = GearRotate;
	}

	void Start(){
		Kick.mute = true;
		Chimes.mute = true;
		Bells.mute = true;

		coroutine = AddThirdSound ();
	}
	void Update(){

		if (!BassAudioSource.isPlaying) {
			BassAudioSource.Play ();
			GlassAudioSource.Play ();
			Kick.Play ();
			Chimes.Play ();
			Bells.Play ();
		}


	}

	void OnEnable(){
		EventManager.StartListening ("rotategear", _someListener);
		EventManager.StartListening ("addfirstsound", AddFirstSound);
		EventManager.StartListening ("addsecondsound", AddSecondSound);
		EventManager.StartListening ("emmacollides", BoneCrack);
	}
	void OnDisable(){
		EventManager.StopListening ("rotategear", _someListener);
		EventManager.StopListening ("addfirstsound", AddFirstSound);
		EventManager.StopListening ("addsecondsound", AddSecondSound);
		EventManager.StopListening ("emmacollides", BoneCrack);
	}

	public void GearRotate(){
		GearRotateClip.Play ();
	}

	public void AddFirstSound(){
		Kick.mute = false;
	}

	public void AddSecondSound(){
		Chimes.mute = false;
		StartCoroutine (coroutine);
	}
	public void BoneCrack(){
		BoneCrackFile.Play ();
	}

	public IEnumerator AddThirdSound(){

		yield return new WaitForSeconds(10);
		Bells.mute = false;
	}
}