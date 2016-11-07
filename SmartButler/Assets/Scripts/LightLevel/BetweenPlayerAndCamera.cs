using UnityEngine;
using System.Collections;
using Assets.Scripts.Character;

public class BetweenPlayerAndCamera : MonoBehaviour {

    /// <summary>
    /// Checks if an obstacle is between the player and the camera. If so, then disable the renderer of that obstacle
    /// </summary>
    /// <param name="coll"></param>
    void OnTriggerEnter(Collider coll)
    {
        if (coll.GetComponent<Despawner>() != null)
        {
            coll.GetComponent<Renderer>().enabled = false;
            StartCoroutine(ResetRenderer(coll));
        }
    }

    IEnumerator ResetRenderer(Collider coll)
    {
        yield return new WaitForSeconds(2);

        coll.GetComponent<Renderer>().enabled = true;


    }
}
