using UnityEngine;

namespace Assets.Scripts.Highlighting {
    public class Highlighter : MonoBehaviour {
        private GameObject Player;
        public float DistanceThreshold = 0.0f;
        private bool _isWithinDistance;

        void Start () {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        void Update () {
            if (Vector3.Distance(transform.position, Player.transform.position) < DistanceThreshold) {
                _isWithinDistance = true;
            } else {
                _isWithinDistance = false;
            }
        }

        private void OnMouseOver() {
            //print(Vector3.Distance(transform.position, Player.transform.position));
            if (_isWithinDistance) {
                gameObject.layer = 8;
            } else {
                gameObject.layer = 0;
            }
        }
        private void OnMouseExit() {
            gameObject.layer = 0;
        }
    }
}