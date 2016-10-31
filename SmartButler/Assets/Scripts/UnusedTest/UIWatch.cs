using UnityEngine;
using System.Collections;

public class UIWatch : MonoBehaviour
{

    private bool isWatchOpen = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	    if (Input.GetKeyDown(KeyCode.I))
	    {
	        isWatchOpen = !isWatchOpen;
	    }

	}

    void OnGUI()
    {
        if (isWatchOpen) {
            GUI.Box(new Rect(10, 10, 100, 90), "Watch");
        }
    }
}
