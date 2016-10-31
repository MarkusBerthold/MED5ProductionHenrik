using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

namespace Assets.Scripts.Character {
    public class Despawner : MonoBehaviour {
        //private fallingPlatform[] _allFallingPlatforms;
        ///public Transform CurrentCheckpoint;
        public Vector3 Offset = Vector3.up;
        private FirstPersonController _player;

        //Initialise
        private void Start() {
            _player = FindObjectOfType<FirstPersonController>();
            //CurrentCheckpoint = GameObject.Find("RespawnPoint").transform;
            //_allFallingPlatforms = FindObjectsOfType<fallingPlatform>();
        }

        /*
        /// <summary>
        /// Reset falling platforms
        /// </summary>
        private void ResetFallingPlatforms()
        {
            foreach (var f in _allFallingPlatforms) f.resetStartPos();
        }
        */
        /*
        /// <summary>
        /// Changes the current checkpoint to a provided Transform 'trans'.
        /// </summary>
        /// <param name="trans"></param>
        public void SetCurrentCheckpoint(Transform trans)
        {
            CurrentCheckpoint = trans;
        }
        */
        //If the provided Collider 'coll' hits the player, reset the falling platforms
        //and move the player back to the last checpoint
        private void OnTriggerEnter(Collider coll) {
            if (coll.gameObject.tag == "Player") {
                _player.RespawnChar();
                //_player.transform.rotation = Quaternion.Euler(0, 90, 0);
                //_player.transform.position = CurrentCheckpoint.transform.position + Offset;
                //ResetFallingPlatforms();
            }
        }
    }
}