using UnityEngine;

namespace Assets.Scripts.ObjectInteraction{
    public class HoldOnRotate : MonoBehaviour{
        private Quaternion _rotation;

        //If the player collides with this object, unfreeze its rotation
        private void OnTriggerEnter(Collider c){
            if (c.gameObject.tag == "Player"){
                //c.transform.localRotation = Quaternion.identity;
                //c.transform.parent = this.gameObject.transform;
                c.GetComponent<Rigidbody>().freezeRotation = true;
                //rotation = transform.rotation;
                c.transform.SetParent(transform, true);
                c.GetComponent<Rigidbody>().freezeRotation = false;
                //c.transform.rotation = rotation;
                //c.transform.rotation = this.gameObject.transform.rotation;
            }
        }

        //If the player collides with this object, freeze its rotation
        private void OnTriggerExit(Collider c){
            if (c.gameObject.tag == "Player"){
                //c.transform.rotation = this.gameObject.transform.rotation;
                //rotation = transform.rotation;
                c.GetComponent<Rigidbody>().freezeRotation = true;
                c.gameObject.transform.SetParent(null, true);
                c.GetComponent<Rigidbody>().freezeRotation = false;
                //c.gameObject.transform.rotation = rotation;
                //c.gameObject.transform.rotation = rotation;
                //c.transform.SetParent(null);
                //c.transform.localRotation = Quaternion.identity;
                //c.transform.rotation = this.gameObject.transform.rotation;
            }
        }
    }
}