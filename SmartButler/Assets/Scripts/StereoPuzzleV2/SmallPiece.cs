using UnityEngine;
using System.Collections;

public class SmallPiece : MonoBehaviour {

    public bool _isKey;

    private Renderer _renderer;
    public HookPiece CurrentHook;
    public HookPiece.Position KeyPosition;

    //Getter for _isKey
    public bool IsKey {
        get { return _isKey; }
    }

    //initisalises _renderer
    private void Start() {
        _renderer = GetComponent<Renderer>();
    }

    /// <summary>
    /// Returns if position is equsl to KeyPosition
    /// Takes a position to check
    /// </summary>
    /// <param name="position"></param>
    /// <returns></returns>
    public bool IsInKeyPosition(HookPiece.Position position) {
        return position == KeyPosition;
    }

    /// <summary>
    /// Lights up material with blue color
    /// </summary>
    public void LightUp() {
        _renderer.material.color = Color.blue;
    }

    /// <summary>
    /// Resets the colour
    /// </summary>
    public void UnLit() {
        _renderer.material.color = Color.white;
    }

    /// <summary>
    /// Checks if the position is a KeyPosition and lights up if yes
    /// </summary>
    public void KeyPositionCheck() {
        if (IsInKeyPosition(CurrentHook.position))
            LightUp();
        else
            UnLit();
    }

    /// <summary>
    /// Checks if the position is a KeyPosition and lights up if yes
    /// Takes a Position to check
    /// </summary>
    /// <param name="position"></param>
    public void KeyPositionCheck(HookPiece.Position position) {
        if (IsInKeyPosition(position))
            LightUp();
        else
            UnLit();
    }
}
