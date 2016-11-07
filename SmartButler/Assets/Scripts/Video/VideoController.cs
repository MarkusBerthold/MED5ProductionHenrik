using UnityEngine;
using System.Collections;

/// <summary>
/// this will automatic add the audio source that belongs to the video
/// </summary>
[RequireComponent (typeof(AudioSource))]

public class VideoController : MonoBehaviour {

    MovieTexture movie;
    new AudioSource audio;
    //public float waitTime = 3f; // uncommet if you want to use delay

    void Start() {
        //MovieTexture movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;

        audio = GetComponent<AudioSource>();
        //GetComponent<AudioSource>().clip = movie.audioClip;
        audio.clip = movie.audioClip;

        // plays video and its sound once
        movie.Play(); // commet this out if you want to use delay
        //GetComponent<AudioSource>().Play();
        audio.Play(); // commet this out if you want to use delay

        // use this line of code below if you want to use delay
        //StartCoroutine(Delay(waitTime));
    }

    IEnumerator Delay (float _waitTime) {
        yield return new WaitForSeconds(_waitTime);
        movie.Play();
        audio.Play();
    }
}
