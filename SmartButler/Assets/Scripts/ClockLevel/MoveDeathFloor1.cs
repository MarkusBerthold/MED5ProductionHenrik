using UnityEngine;
using System.Collections;

public class MoveDeathFloor1 : MonoBehaviour {

    public GameObject DeathFloor;

    void OnTriggerEnter(Collider target) {
        if (target.gameObject.tag == "Player") {
            DeathFloor.transform.position = new Vector3(0,9,0);
            Debug.Log("DeathFloor has been moved to: " + DeathFloor.transform.position);
        }
    }
}
