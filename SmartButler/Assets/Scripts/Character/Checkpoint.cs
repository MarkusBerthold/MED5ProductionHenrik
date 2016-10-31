using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.Scripts.Character {
    public class Checkpoint : MonoBehaviour {

        private Despawner _despawner;
        private FirstPersonController FPScontroller;
        private ThirdPersonCharacter TPScontroller;

        // Use this for initialization
        void Start() {
            FPScontroller = FindObjectOfType<FirstPersonController>();
            TPScontroller = FindObjectOfType<ThirdPersonCharacter>();
            _despawner = GameObject.FindObjectOfType<Despawner>();
        }

        // Update is called once per frame
        void Update() {
        }

        //If the player collides with this object, set this to be the new checkpoint
        void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
                FPScontroller.SetCurrentCheckpoint(this.transform.position);
            }
            if (other.gameObject.tag == "ThirdPerson") {
                TPScontroller.SetCurrentCheckpoint(this.transform.position);
            }
        }
    }
}
