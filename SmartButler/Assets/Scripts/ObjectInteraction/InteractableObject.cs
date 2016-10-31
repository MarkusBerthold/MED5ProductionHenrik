using UnityEngine;

namespace Assets.Scripts.ObjectInteraction{
    public class InteractableObject : MonoBehaviour{
        public Material[] Mats;

        public Material MyMaterial;

        // Use this for initialization
        private void Start(){
        }

        // Update is called once per frame
        private void Update(){
            Mats = gameObject.GetComponent<MeshRenderer>().materials;
        }

        //Sets the Mesh of an object
        public void OnMouseEnter(){
            Mats[0] = gameObject.GetComponent<MeshRenderer>().materials[0];
            Mats[1] = MyMaterial;

            gameObject.GetComponent<MeshRenderer>().materials = Mats;
        }

        //Resets the Mesh of an object
        public void OnMouseExit(){
            Mats[0] = gameObject.GetComponent<MeshRenderer>().materials[0];
            Mats[1] = null;

            gameObject.GetComponent<MeshRenderer>().materials = Mats;
        }

        //Debugging
        public void OnMouseUp(){
            Debug.Log("Up");
        }
    }
}