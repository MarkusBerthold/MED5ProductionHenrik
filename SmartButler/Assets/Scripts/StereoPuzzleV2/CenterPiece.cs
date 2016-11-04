using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.StereoPuzzleV2 {
    [RequireComponent(typeof (BoxCollider))]
    public class CenterPiece : MonoBehaviour {
        private static bool _canSpin = true;
        private readonly List<SmallPiece> _associatedPieces = new List<SmallPiece>();


        // used for testing purposes
        private void OnMouseDown(){
            StartCoroutine("SpinObject");
        }

        /// <summary>
        ///     Adds the associated pieces
        /// </summary>
        /// <param name="piece"></param>
        public void AddAssociatedPiece(SmallPiece piece){
            if (!_associatedPieces.Contains(piece) & _canSpin)
                _associatedPieces.Add(piece);
        }

        /// <summary>
        ///     Removes the associated pieces
        /// </summary>
        /// <param name="piece"></param>
        public void RemoveAssociatedPiece(SmallPiece piece){
            if (_associatedPieces.Contains(piece))
                _associatedPieces.Remove(piece);
        }

        private void OnTriggerEnter(Collider other){
            var potentialPiece = other.gameObject.GetComponent<SmallPiece>();
            if (potentialPiece != null)
                AddAssociatedPiece(potentialPiece);
        }

        private void OnTriggerStay(Collider other){
            if (!_associatedPieces.Contains(other.GetComponent<SmallPiece>()))
                AddAssociatedPiece(other.GetComponent<SmallPiece>());
        }

        private void OnTriggerExit(Collider other){
            var potentialPiece = other.gameObject.GetComponent<SmallPiece>();
            if (potentialPiece != null) RemoveAssociatedPiece(potentialPiece);
        }


        public static bool CanSpin{
            get { return _canSpin; }
        }

        /// <summary>
        ///     Spins the object
        /// </summary>
        /// <returns></returns>
        private IEnumerator SpinObject(){
            if (_canSpin){
                _canSpin = false;
                //Debug.Log("spiinning: " + associatedPieces.Count);
                //find children to spin
                foreach (var piece in _associatedPieces){
                    var pieceTransform = piece.transform;
                    pieceTransform.parent = transform;
                }
                //find rotation target:
                var curRotation = transform.rotation.eulerAngles;
                var to = Quaternion.Euler(curRotation.x, curRotation.y, curRotation.z - 90); //new Vector3(curRotation.x, curRotation.y + 90, curRotation.z);

                //start spinning:
                while (Quaternion.Angle(transform.rotation, to) > 1f){
                    transform.rotation =  Quaternion.Lerp(transform.rotation,to,Time.deltaTime * 3);//Vector3.Lerp(transform.rotation.eulerAngles, to, Time.deltaTime*3);
                    yield return new WaitForEndOfFrame();
                }

                //the spin is at this point 1 euler degree from the target rotation
                //so we shift it into place and enable another rotation
                //   foreach (var p in _associatedPieces)
                //       if (p.CurrentHook)
                //           p.KeyPositionCheck();
                transform.rotation = to;
                //_parent.CheckAlignedPieces();
                _canSpin = true;
            }
            yield return null;
        }
    }
}