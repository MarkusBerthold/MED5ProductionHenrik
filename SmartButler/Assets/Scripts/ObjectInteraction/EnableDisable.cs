using UnityEngine;
using System.Collections;
using Assets.Scripts.Highlighting;
using Assets.Scripts.ObjectInteraction;
using UnityEngine.Events;
using Assets.Scripts.MessageingSystem;

public class EnableDisable : MonoBehaviour
{

    private Component[] _highLighterComponents;
    private Component[] _lookAtTargetComponents;

    public Highlighter _highlighter;

    private UnityAction _someListener;

    // Use this for initialization

    void Awake() {
        _someListener = EnableRemote;

    }

    void OnEnable() {

        EventManager.StartListening("coffeebutton", _someListener);
        EventManager.StartListening("remotecontrol", EnableSpeaker);

    }

    void OnDisable() {
        EventManager.StopListening("coffeebutton", _someListener);
        EventManager.StopListening("remotecontrol", EnableSpeaker);
    }

    /// <summary>
    /// Enables all HighLighter and LookAtTarget scripts on itself and its children
    /// </summary>
    void EnableRemote() {
        if (this.tag == "RemoteController") {
            _highLighterComponents = GetComponentsInChildren<Highlighter>();
            _lookAtTargetComponents = GetComponentsInChildren<LookAtTargets>();
            for (int i = 0; i < _highLighterComponents.Length; i++) {
                _highLighterComponents[i].GetComponent<Highlighter>().enabled = true;
                _highLighterComponents[i].GetComponent<Highlighter>().Activated = true;
            }

            for (int i = 0; i < _lookAtTargetComponents.Length; i++) {
                _lookAtTargetComponents[i].GetComponent<LookAtTargets>().enabled = true;
                _lookAtTargetComponents[i].GetComponent<LookAtTargets>().Activated = true;
            }
        }
    }

    void EnableSpeaker()
    {
        if (this.tag == "Speaker") {
            _highLighterComponents = GetComponentsInChildren<Highlighter>();
            _lookAtTargetComponents = GetComponentsInChildren<LookAtTargets>();
            for (int i = 0; i < _highLighterComponents.Length; i++) {
                _highLighterComponents[i].GetComponent<Highlighter>().enabled = true;
                _highLighterComponents[i].GetComponent<Highlighter>().Activated = true;
            }

            for (int i = 0; i < _lookAtTargetComponents.Length; i++) {
                _lookAtTargetComponents[i].GetComponent<LookAtTargets>().enabled = true;
                _lookAtTargetComponents[i].GetComponent<LookAtTargets>().Activated = true;
            }
        }
    }

}
