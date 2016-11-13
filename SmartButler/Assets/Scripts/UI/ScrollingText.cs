using UnityEngine;
using System.Collections;
using Assets.Scripts.MessageingSystem;

public class ScrollingText : MonoBehaviour {

    Vector2 _originalPosition;

    void OnEnable()
    {
        _originalPosition = transform.position;
    }

    void OnDisable() {
        transform.position = _originalPosition;
    }

    void Update() {
        transform.Translate(Vector2.up);
    }
}
