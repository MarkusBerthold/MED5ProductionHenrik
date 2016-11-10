using Assets.Scripts.PickUp;
using UnityEngine;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.PlacementGear{
    public class PlaceSecondGear : MonoBehaviour{
        private bool _connected;
        private float _dist;

        private bool _doOnce = true;
        private bool _shouldRotate = true;

        private float _timer;

        public GameObject Placeable;

        // Use this for initialization
        private void Start(){
            _connected = false;
        }

        // Update is called once per frame
        private void Update(){
            if (_shouldRotate)
                transform.Rotate(Vector3.forward*Time.deltaTime*40, Space.World);

            _dist = Vector3.Distance(Placeable.transform.position, gameObject.transform.position);

            if (Placeable.GetComponent<Pickupable>() != null)
                if ((_dist < 4) && Placeable.GetComponent<Pickupable>().GetDropped()){
                    Placeable.gameObject.transform.SetParent(gameObject.transform);

                    _connected = true;
					EventManager.TriggerEvent ("rotategear");
                    Placeable.gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                        gameObject.transform.position.y,
                        gameObject.transform.position.z);
                    //print("SecondGear - trying to parent");


                    //placeable.gameObject.transform.parent = this.gameObject.transform;
                    //print("SecondGear - should have parented now");
                    Destroy(Placeable.GetComponent<Pickupable>());

                    if (_doOnce){
                        Placeable.transform.Rotate(90, 0, 0);
                        Placeable.gameObject.transform.parent = gameObject.transform;
                        _doOnce = false;
                    }
                }
        }

        /// <summary>
        /// Returns _connected variable
        /// </summary>
        /// <returns></returns>
        public bool GetConnected(){
            return _connected;
        }

        /// <summary>
        /// Sets boolean _shouldRotate to a given value
        /// Takes a boolean x to set
        /// </summary>
        /// <param name="x"></param>
        public void SetShouldRotate(bool x){
            _shouldRotate = x;
        }

        /// <summary>
        /// returns _shouldRotate
        /// </summary>
        /// <returns></returns>
        public bool GetShouldRotate(){
            return _shouldRotate;
        }
    }
}