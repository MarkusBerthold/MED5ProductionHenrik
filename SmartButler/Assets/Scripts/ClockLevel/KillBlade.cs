using UnityEngine;
using System.Collections;

namespace ClockLevel {
    public class KillBlade : MonoBehaviour {

        void Update() {
            transform.Rotate(Vector3.up * Time.deltaTime * -12, Space.World);
        }
    }
}