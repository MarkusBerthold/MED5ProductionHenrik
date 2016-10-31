using UnityEngine;

namespace Assets.Scripts.LightLevel{
    public class KillCube : MonoBehaviour{
        // Use this for initialization
        private void Start(){
        }

        // Update is called once per frame
        private void Update(){
            //transform.Rotate(Vector3.up * Time.deltaTime*-12, Space.World);
        }

        //If player collides with this object, reset position
        private void OnTriggerEnter(Collider c){
            if (c.tag == "Player"){
                c.transform.position = new Vector3(189.9f, -1.4f, -0.45f);
                c.transform.rotation = new Quaternion(0, 0, 0, 0);
                // Rotation doesnt work
            }
        }
    }
}