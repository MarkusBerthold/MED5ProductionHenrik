using UnityEngine;
using System.Collections;

public class MoveDeathFloor2 : MonoBehaviour {

    public GameObject DeathFloor;

    void OnTriggerEnter(Collider target) {
        if (target.gameObject.tag == "Player") {
            DeathFloor.transform.position = new Vector3(0, 17, 0);
            Debug.Log("DeathFloor has been moved to: " + DeathFloor.transform.position);
        }
    }
}
