using UnityEngine;
using System.Collections;

public class GameControlDontDestroy : MonoBehaviour {

	// Use this for initialization
	void Awake () {
	

		Object.DontDestroyOnLoad (transform.gameObject);

		if (FindObjectsOfType (GetType ()).Length > 1) {
			Destroy (gameObject);
		}
	}

}
