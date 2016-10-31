using UnityEngine;

namespace Assets.Scripts.ClockLevel{
    public class GearRotationXAxis : MonoBehaviour{
        public float Rotspeed;
        //public float startspeed;

        //public GameObject prevplat;

        // Use this for initialization
        private void Start(){

            /*if(prevplat == null){
                        rotspeed = 0;
                    }else{
                        //rotspeed = prevplat.rotspeed;
                    }*/
        }

        // Update is called once per frame
        private void Update(){
            transform.Rotate(new Vector3(1, 0, 0)*Time.deltaTime*Rotspeed, Space.Self);
        }
    }
}