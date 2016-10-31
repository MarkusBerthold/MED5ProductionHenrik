using UnityEngine;

namespace Assets.Scripts.ClockLevel{
    public class GearRotationFollower : MonoBehaviour{
        public int CurrentTurn;
        public int MyTurn;
        public GameObject PrevPlatform;

        public float Rotspeed;

        public float Size;

        // Use this for initialization
        private void Start(){
        }

        // Update is called once per frame
        private void Update(){
            //transform.Rotate(Vector3.up * Time.deltaTime * rotspeed, Space.World);

            //currentTurn = prevPlatform.GetComponent<GearRotationStarter>().
        }
    }
}