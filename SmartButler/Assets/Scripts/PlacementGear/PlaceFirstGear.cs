using Assets.Scripts.PickUp;
using UnityEngine;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.PlacementGear{
    public class PlaceFirstGear : MonoBehaviour{
        private bool _connected;
        private float _dist;

        private bool _doOnce = true;

        public GameObject Placeable;
        public GameObject TransparentCog;

        // Use this for initialization
        private void Start(){
            _connected = false;
        }

        // Update is called once per frame
        private void Update(){
            if (Placeable.tag == "FirstGear"){
                transform.Rotate(Vector3.up*Time.deltaTime*-40, Space.World);
                _dist = Vector3.Distance(Placeable.transform.position, gameObject.transform.position);
            }

            if (Placeable.GetComponent<Pickupable>() != null)
                if ((_dist < 4) && Placeable.GetComponent<Pickupable>().GetDropped()){
                    Placeable.gameObject.transform.SetParent(gameObject.transform);

                    _connected = true;
				EventManager.TriggerEvent ("rotategear");
                    Placeable.gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                        gameObject.transform.position.y,
                        gameObject.transform.position.z);
                    print("firstGear - trying to parent");


                    //placeable.gameObject.transform.parent = this.gameObject.transform;
                    print("firstGear - should have parented now");
                    Destroy(Placeable.GetComponent<Pickupable>());
                    TransparentCog.GetComponent<Renderer>().enabled = false;
                }
        }

        /// <summary>
        /// Returns _connected variable
        /// </summary>
        /// <returns></returns>
        public bool GetConnected(){
            return _connected;
        }
    }
}