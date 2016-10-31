using UnityEngine;

namespace Assets.Scripts.PickUp{
    public class Pickupable : MonoBehaviour{
        private bool _dropped;
        //private bool isColliding = false;

        // Use this for initialization
        private void Start(){
            _dropped = false;
        }

        // Update is called once per frame
        private void Update(){
        }

        /// <summary>
        /// Sets the _dropped boolean to be equal to x
        /// Takes a boolean x
        /// </summary>
        /// <param name="x"></param>
        public void SetDropped(bool x){
            _dropped = x;
        }

        /// <summary>
        /// returns the variable _dropped
        /// </summary>
        /// <returns></returns>
        public bool GetDropped(){
            return _dropped;
        }
    }
}