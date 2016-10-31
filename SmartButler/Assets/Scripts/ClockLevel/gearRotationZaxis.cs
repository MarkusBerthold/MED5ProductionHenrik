using UnityEngine;

namespace Assets.Scripts.ClockLevel{
    public class GearRotationZaxis : MonoBehaviour{
        public float Rotspeed;

        private void Start(){
        }

        // Update is called once per frame
        private void Update(){
            transform.Rotate(new Vector3(0, 0, 1)*Time.deltaTime*Rotspeed, Space.Self);
        }
    }
}