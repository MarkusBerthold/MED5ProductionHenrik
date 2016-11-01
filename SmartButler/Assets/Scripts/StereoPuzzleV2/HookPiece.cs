using UnityEngine;
using System.Collections;

public class HookPiece : MonoBehaviour {

    public enum Position {
        Top,
        Bottom,
        Left,
        Right
    }

    public SmallPiece _currentPiece;
    public Position position;

    // Getter for current piece
    public SmallPiece CurrentPiece {
        get { return _currentPiece; }
    }

    private void OnTriggerEnter(Collider other) {
        SmallPiece potentialPiece;
        if (potentialPiece = other.gameObject.GetComponent<SmallPiece>()) {
            _currentPiece.CurrentHook = this;
            _currentPiece = potentialPiece;
            if (_currentPiece.IsKey) _currentPiece.KeyPositionCheck(position);
        }
    }

    private void OnTriggerExit(Collider other) {
        SmallPiece potentialPiece;
        if (potentialPiece = other.gameObject.GetComponent<SmallPiece>()) {
            potentialPiece.KeyPositionCheck(position);
            potentialPiece.CurrentHook = null;
        }
    }
}
