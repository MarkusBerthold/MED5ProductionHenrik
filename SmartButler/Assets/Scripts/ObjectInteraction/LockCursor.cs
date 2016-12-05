using UnityEngine;
using System.Collections;

public class LockCursor : MonoBehaviour {
    public static void EnableCursor () {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public static void DisableCursor () {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
