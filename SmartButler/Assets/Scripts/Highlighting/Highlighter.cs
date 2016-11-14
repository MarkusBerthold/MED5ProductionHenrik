using UnityEngine;

namespace Assets.Scripts.Highlighting {
    public class Highlighter : MonoBehaviour {
        private GameObject Player;
        public float DistanceThreshold = 0.0f;
        private bool _isWithinDistance;
        public bool Activated = false;     

        private void OnMouseOver() {
            if (Activated)
            {
                if (!Player)
                {
                    Player = GameObject.FindGameObjectWithTag("Player");
                    if (!Player)
                        return;
                }
                if (Vector3.Distance(transform.position, Player.transform.position) < DistanceThreshold)
                {
                    gameObject.layer = 8;
                }
                else
                {
                    gameObject.layer = 0;
                }
            }
        }
        private void OnMouseExit() {
            gameObject.layer = 0;
        }

        public void Update()
        {
            
        }
    }
}