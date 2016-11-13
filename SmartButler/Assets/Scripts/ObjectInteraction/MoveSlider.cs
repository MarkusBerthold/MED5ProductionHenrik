using Assets.Scripts.Controllers;
using UnityEngine;

namespace Assets.Scripts.ObjectInteraction{
    public class MoveSlider : MonoBehaviour{
        private bool _dragEnabled; //is used to set whether or not you can drag the slider
        private float _dragStartDistance; //where are the max/min values set?
        private Vector3 _dragStartPosition;
        private Vector3 _objectStartPosition;
        private float _zeroToOne;
        public LightSwitcher LightSwitcher;
        public StereoController StereoController;
        public float LowerBound;

        public float OpperBound;
        public Transform Pivot;
        public char SlideAxis;
        public float SliderLength;

        //Initalises Pivot and _objectStartPosition
        private void Start(){
            if (!Pivot)
                Pivot = transform;

            _objectStartPosition = transform.position;
        }

        //Set variables values
        private void OnMouseDown(){
            _dragEnabled = true;
            _dragStartPosition = transform.position;
            _dragStartDistance = (Camera.main.transform.position - transform.position).magnitude;
        }

        //Runs once per frame
        private void Update(){
            //zerToOne is the number that we want to access later, 0 -> 1
            //SliderLength = OpperBound - LowerBound;
            
            //only drag if mouse is pressed
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) _dragEnabled = false;
        }

        //function is called when you click an object with a collider
        private void OnMouseDrag(){
            if (_dragEnabled){
                //switching axises
                if (SlideAxis.Equals('X')){
                    // using unitys screen to world function
                    var worldDragTo =
                        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                            _dragStartDistance));
                    //making sure we can only slide along the slider and not further
                    if ((worldDragTo.x > LowerBound) && (worldDragTo.x < OpperBound))

                        //translate the axis along the vector from the "screen to world" function
                        transform.position = new Vector3(worldDragTo.x, _dragStartPosition.y, _dragStartPosition.z);
                }

                // ?
                if (SlideAxis.Equals('Y')){
                    var worldDragTo =
                        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                            _dragStartDistance));

                    if ((worldDragTo.y >= LowerBound) && (worldDragTo.y <= OpperBound))
                        transform.position = new Vector3(_dragStartPosition.x, worldDragTo.y, _dragStartPosition.z);
                }

                // ?
                if (SlideAxis.Equals('Z')){
                    var worldDragTo =
                        Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y,
                            _dragStartDistance));

                    if ((worldDragTo.z >= LowerBound) && (worldDragTo.z <= OpperBound))
                        transform.position = new Vector3(_dragStartPosition.x, _dragStartPosition.y, worldDragTo.z);
                }
                if (this.tag == "RemoteController"){
                    StereoController.UpdateStereoVolume(GetZeroToOne());
                }
                LightSwitcher.SetIntensity(GetZeroToOne());
            }
        }

		public void SetLowerandOpper(){

			Vector3 RemotePos = this.transform.parent.gameObject.transform.position;
			LowerBound = LowerBound + RemotePos.y;
			OpperBound = OpperBound + RemotePos.y;
		}

		public void ResetLowerandOpper(){

			Vector3 RemotePos = this.transform.parent.gameObject.transform.position;
			LowerBound = -0.1f;
			OpperBound = -0.004f;
		}

        /// <summary>
        /// ?
        /// </summary>
        /// <returns></returns>
        public float GetZeroToOne(){
            if (this.tag == "RemoteController"){
                _zeroToOne = (transform.position.x - LowerBound) / SliderLength;
            }
            else{
                _zeroToOne = (transform.position.y - LowerBound) / SliderLength;
            }
            return _zeroToOne;
        }

        /// <summary>
        /// ?
        /// </summary>
        private void OnDrawGizmos(){
            if (SlideAxis.Equals('X'))
                Gizmos.DrawLine(Pivot.transform.position + Vector3.right*SliderLength,
                    Pivot.transform.position - Vector3.right*SliderLength);
            if (SlideAxis.Equals('Y'))
                Gizmos.DrawLine(Pivot.transform.position + Vector3.up*SliderLength,
                    Pivot.transform.position - Vector3.up*SliderLength);
            if (SlideAxis.Equals('Z'))
                Gizmos.DrawLine(Pivot.transform.position + Vector3.forward*SliderLength,
                    Pivot.transform.position - Vector3.forward*SliderLength);
        }
    }
}