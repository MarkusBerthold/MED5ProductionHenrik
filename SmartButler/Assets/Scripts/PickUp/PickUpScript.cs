using System.Runtime.CompilerServices;
using UnityEngine;

namespace Assets.Scripts.PickUp{
	public class PickUpScript : MonoBehaviour {
		//private GameObject Camera;
		private GameObject _carriedObject;
		private GameObject _carriedObjectParent;
		private bool _carrying;
		public float Distance;

		private Pickupable _pick;

		public bool GetCarrying() {
			return _carrying;
		}

		// Update is called once per frame
		private void Update() {
			if (_carrying) {
				Carry(_carriedObject);
				CheckDrop();
			} else {
				Pickup();
			}
		}

		/// <summary>
		/// Moves an object with the player
		/// Takes a GameObject o, which is what object the player is carrying
		/// </summary>
		/// <param name="o"></param>
		private void Carry(GameObject o) {
			// if (!_carriedObject.GetComponent<Pickupable>().getIsColliding() && )

			o.transform.position = GetComponent<Camera>().transform.position +
				GetComponent<Camera>().transform.forward * Distance;
			o.transform.SetParent(null);
		}

		/// <summary>
		/// Picks up an object
		/// </summary>
		private void Pickup() {
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) {
				var x = Screen.width / 2;
				var y = Screen.height / 2;
				var ray = GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
				RaycastHit hit;
				if (Physics.Raycast(ray, out hit)) {
					var p = hit.collider.GetComponent<Pickupable>();
					if (p != null) {
						_carriedObjectParent = p.gameObject.transform.root.gameObject;

						_carrying = true;
						_carriedObject = p.gameObject;
						p.GetComponent<Rigidbody>().isKinematic = true;
						_carriedObject.GetComponent<Pickupable>().SetDropped(false);

						_carriedObject.transform.localScale = _carriedObject.transform.localScale/2;
					}
				}
			}
		}

		/// <summary>
		/// Checks if an object is being dropped
		/// </summary>
		private void CheckDrop() {
			if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) {
				
				DropObject();
			}
		}

		/// <summary>
		/// Drops an object
		/// </summary>
		private void DropObject() {
			_carrying = false;
			// _carriedObject.GetComponent<Rigidbody>().isKinematic = false;
			_carriedObject.GetComponent<Pickupable>().SetDropped(true);
            _carriedObject.transform.localScale = _carriedObject.transform.localScale*2;
            _carriedObject = null;
			//hasDropped = true;
		}

	}
}