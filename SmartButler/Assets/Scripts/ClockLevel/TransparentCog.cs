using Assets.Scripts.PickUp;
using UnityEngine;

namespace Assets.Scripts.ClockLevel{
    public class TransparentCog : MonoBehaviour{
        private PickUpScript _pickUp;
        private Renderer _rend;

        // Use this for initialization
        private void Start(){
            _pickUp = FindObjectOfType<PickUpScript>();
            _pickUp.GetCarrying();
            _rend = GetComponent<Renderer>();
        }

        // Update is called once per frame
        private void Update(){
            if (_pickUp.GetCarrying()) _rend.enabled = true;
            else _rend.enabled = false;
        }
    }
}