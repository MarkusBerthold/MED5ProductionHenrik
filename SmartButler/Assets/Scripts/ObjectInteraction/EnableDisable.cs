using UnityEngine;
using System.Collections;
using Assets.Scripts.Highlighting;
using Assets.Scripts.ObjectInteraction;

public class EnableDisable : MonoBehaviour
{

    private Component[] _highLighterComponents;
    private Component[] _lookAtTargetComponents;
   
	// Use this for initialization


        /// <summary>
        /// Disables all HighLighter and LookAtTarget scripts on itself and its children
        /// </summary>
    void Start()
    {

        if (this.tag == "RemoteController" || this.tag == "Speaker")
        {
            _highLighterComponents = GetComponentsInChildren<Highlighter>();
            _lookAtTargetComponents = GetComponentsInChildren<LookAtTargets>();
            for (int i = 0; i < _highLighterComponents.Length; i++)
            {
                _highLighterComponents[i].GetComponent<Highlighter>().enabled = false;
            }

            for (int i = 0; i < _lookAtTargetComponents.Length; i++)
            {
                _lookAtTargetComponents[i].GetComponent<LookAtTargets>().enabled = false;
            }
        }
    }
}
