using UnityEngine;
using System.Collections;
using UnityStandardAssets.Characters.ThirdPerson;



public class DrumSounds : MonoBehaviour {

	public AudioClip [] drumaudioclips = new AudioClip[4];
	public AudioClip [] kalimbaaudioclips = new AudioClip[3];

	private AudioSource DrumsAudioSource;
	private AudioSource OrganAudioSource;
	private AudioSource KalimbaAudioSource;
	private AudioSource LightpadAudioSource;
	public AudioSource falling;

	public ThirdPersonCharacter tpc;


	// Use this for initialization
	void Start () {
	
		DrumsAudioSource = GameObject.FindGameObjectWithTag("drums").GetComponent<AudioSource>();
		OrganAudioSource = GameObject.FindGameObjectWithTag("organ").GetComponent<AudioSource>();
		KalimbaAudioSource = GameObject.FindGameObjectWithTag("kalimba").GetComponent<AudioSource>();
		LightpadAudioSource = GameObject.FindGameObjectWithTag("lightpad").GetComponent<AudioSource>();

		tpc = FindObjectOfType<ThirdPersonCharacter> ();
	}
	
	// Update is called once per frame
	void Update () {


		if (!DrumsAudioSource.isPlaying) {

			int drumsrand = (int) (10f * Random.Range (0, 0.4f));
			int kalimbarand = (int) (10f * Random.Range (0, 0.3f));
			float lightpadprobability = Random.Range (0f, 1f);

			print ("drumsrand is: "+drumsrand);
			print ("kalimbarand is: "+kalimbarand);

			DrumsAudioSource.clip = drumaudioclips [drumsrand];
			DrumsAudioSource.Play ();

			KalimbaAudioSource.clip = kalimbaaudioclips [kalimbarand];
			KalimbaAudioSource.Play ();
			OrganAudioSource.Play ();

			if (lightpadprobability >= 0.5f) {
				print ("doing lightpad");
				LightpadAudioSource.Play ();
			}

		}

		if (!tpc.GetIsGrounded()) {
			if (!falling.isPlaying) {
				falling.Play ();
			}
			
		}



	
	}

	public void ResetSounds(){

		DrumsAudioSource.Stop ();
		KalimbaAudioSource.Stop ();
		OrganAudioSource.Stop ();
		LightpadAudioSource.Stop ();
		}
	}
