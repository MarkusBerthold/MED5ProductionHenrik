using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using UnityStandardAssets.Characters.ThirdPerson;

namespace Assets.Scripts.Character {
    public class Checkpoint : MonoBehaviour {

        private Despawner _despawner;
        private FirstPersonController _FPScontroller;
        private ThirdPersonCharacter _TPScontroller;

        // Use this for initialization
        void Start() {
            _FPScontroller = FindObjectOfType<FirstPersonController>();
            _TPScontroller = FindObjectOfType<ThirdPersonCharacter>();
            _despawner = GameObject.FindObjectOfType<Despawner>();
        }

        // Update is called once per frame
        void Update() {
        }

        //If the player collides with this object, set this to be the new checkpoint
        void OnTriggerEnter(Collider other) {
            if (other.gameObject.tag == "Player") {
                _FPScontroller.SetCurrentCheckpoint(this.transform.position);
                this.GetComponent<BoxCollider>().enabled = false; //disables the checkpoints you've reached so you can't reach it again
            }
            if (other.gameObject.tag == "ThirdPerson") {
                _TPScontroller.SetCurrentCheckpoint(this.transform.position);
            }
        }
    }
}
