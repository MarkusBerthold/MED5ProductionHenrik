using UnityEngine;

namespace Assets.Scripts.StereoPuzzle{
    public class StereoPuzzleHook : MonoBehaviour{
        public enum Position{
            Top,
            Bottom,
            Left,
            Right
        }

        public StereoPuzzlePiece _currentPiece;
        public Position position;

        // Getter for current piece
        public StereoPuzzlePiece CurrentPiece{
            get { return _currentPiece; }
        }

        private void OnTriggerEnter(Collider other){
            StereoPuzzlePiece potentialPiece;
            if (potentialPiece = other.gameObject.GetComponent<StereoPuzzlePiece>()){
                _currentPiece.CurrentHook = this;
                _currentPiece = potentialPiece;
                if (_currentPiece.IsKey) _currentPiece.KeyPositionCheck(position);
            }
        }

        private void OnTriggerExit(Collider other){
            StereoPuzzlePiece potentialPiece;
            if (potentialPiece = other.gameObject.GetComponent<StereoPuzzlePiece>()){
                potentialPiece.KeyPositionCheck(position);
                potentialPiece.CurrentHook = null;
            }
        }
    }
}