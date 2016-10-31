using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.Scripts.Character {
    public class Checkpoint : MonoBehaviour {

        private Despawner _despawner;
        private FirstPersonController FPScontroller;

        // Use this for initialization
        void Start() {
            FPScontroller = FindObjectOfType<FirstPersonController>();
            _despawner = GameObject.FindObjectOfType<Despawner>();
        }

        // Update is called once per frame
        void Update() {
        }

        //If the player collides with this object, set this to be the new checkpoint
        void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
                FPScontroller.SetCurrentCheckpoint(this.transform.position);
                this.GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
