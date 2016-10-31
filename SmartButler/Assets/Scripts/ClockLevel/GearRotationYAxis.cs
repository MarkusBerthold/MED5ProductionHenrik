using UnityEngine;

namespace Assets.Scripts.ClockLevel{
    public class GearRotationYAxis : MonoBehaviour{
        public float Rotspeed;

        // Use this for initialization
        private void Start(){
        }

        // Update is called once per frame
        private void Update(){
            transform.Rotate(Vector3.up*Time.deltaTime*Rotspeed, Space.World);
        }
    }
}