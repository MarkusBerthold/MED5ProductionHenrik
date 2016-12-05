using UnityEngine;
using System.Collections;

public class FlareColor : MonoBehaviour {

    [SerializeField]
    private Light light;

    private LensFlare flare;


    void Start(){
        flare = GetComponent<LensFlare>();
    }
	// Update is called once per frame
	void Update () {
	    if (!light.enabled){
	        flare.enabled = false;
	    }else{
	        flare.enabled = true;
	        flare.color = light.color;
	    }
	}
}
