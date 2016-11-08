using UnityEngine;
using System.Collections;

public class DrumSounds : MonoBehaviour {

	public AudioClip [] audioclips = new AudioClip[4];
	public AudioClip organ;

	private AudioSource DrumsAudioSource;
	private AudioSource OrganAudioSource;

	// Use this for initialization
	void Start () {
	
		DrumsAudioSource = GameObject.FindGameObjectWithTag("drums").GetComponent<AudioSource>();
		OrganAudioSource = GameObject.FindGameObjectWithTag("organ").GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {


		if (!DrumsAudioSource.isPlaying) {

			int rand = (int) (10f * Random.Range (0, 0.3f));
			print (rand);
			DrumsAudioSource.clip = audioclips [rand];
			DrumsAudioSource.Play ();
			OrganAudioSource.Play ();
		}




	
	}
}
