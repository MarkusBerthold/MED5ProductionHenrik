using Assets.Scripts.PickUp;
using UnityEngine;

namespace Assets.Scripts.PlacementGear {
    public class placeLastGear : MonoBehaviour {
        private bool _connected;
        private float _dist;

        private bool _doOnce = true;

        public GameObject Placeable;

        // Use this for initialization
        private void Start() {
            _connected = false;
        }

        // Update is called once per frame
        private void Update() {
            transform.Rotate(Vector3.up * Time.deltaTime * -40, Space.World);
            _dist = Vector3.Distance(Placeable.transform.position, gameObject.transform.position);


            if (Placeable.GetComponent<Pickupable>() != null)
                if ((_dist < 4) && Placeable.GetComponent<Pickupable>().GetDropped()) {
                    Placeable.gameObject.transform.SetParent(gameObject.transform);

                    _connected = true;
                    Placeable.gameObject.transform.position = new Vector3(gameObject.transform.position.x,
                        gameObject.transform.position.y,
                        gameObject.transform.position.z);
                    print("LastGear - trying to parent");


                    //placeable.gameObject.transform.parent = this.gameObject.transform;
                    print("LastGear - should have parented now");
                    Destroy(Placeable.GetComponent<Pickupable>());
                }
        }

        /// <summary>
        /// Returns _connected variable
        /// </summary>
        /// <returns></returns>
        public bool GetConnected() {
            return _connected;
        }
    }
}