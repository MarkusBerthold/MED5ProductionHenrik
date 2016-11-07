using UnityEngine;
using System.Collections;

public class Lights : MonoBehaviour {
    private Transform[] _lightTransforms;
    private Light[] _lights;
    // Use this for initialization
    void Start(){
        _lightTransforms = GetComponentsInChildren<Transform>();
        _lights = GetComponentsInChildren<Light>();
        for (int i = 0; i < _lightTransforms.Length; i++){
            _lightTransforms[i].position = transform.position + Vector3.left*50*i;
            if (_lightTransforms[i].GetComponent<Light>())
                _lightTransforms[i].GetComponent<Light>().color = Color.HSVToRGB(Random.Range(0f,1f), 1, 1);
            //(float)i/_lightTransforms.Length
        }
    }

    // Update is called once per frame
    void Update() {}
}