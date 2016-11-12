using UnityEngine;
using System.Collections;

/// <summary>
/// this will automatic add the audio source that belongs to the video
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class VideoController : MonoBehaviour {

    MovieTexture movie;

    void Start() {
        movie = GetComponent<Renderer>().material.mainTexture as MovieTexture;

        // plays video and its sound once
        movie.Play(); // commet this out if you want to use delay
        movie.loop = true;
    }
}