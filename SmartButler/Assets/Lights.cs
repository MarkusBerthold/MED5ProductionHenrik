using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {
    private Transform[] _lightTransforms;
    private Light[] _lights;
    public int lightSpacing = 250; //spacing between light
    public int lightIntensity = 5; //intensity of light

    // Use this for initialization
    void Start(){
        _lightTransforms = GetComponentsInChildren<Transform>();
        _lights = GetComponentsInChildren<Light>();
        for (int i = 0; i < _lightTransforms.Length; i++){
            _lightTransforms[i].position = transform.position + Vector3.left*lightSpacing*i;
            if (_lightTransforms[i].GetComponent<Light>())
                _lightTransforms[i].GetComponent<Light>().color = Color.HSVToRGB(Random.Range(0f,1f), 1, lightIntensity);
            //(float)i/_lightTransforms.Length
        }
    }

    // Update is called once per frame
    void Update(){}
}