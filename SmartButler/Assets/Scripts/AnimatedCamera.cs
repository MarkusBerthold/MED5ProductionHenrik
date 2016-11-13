using UnityEngine;
using System.Collections;

public class AnimatedCamera : MonoBehaviour {

    public GameObject FpsGameObject;

    void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    public IEnumerator Destroy(){
        foreach (Behaviour child in FpsGameObject.GetComponentsInChildren<Behaviour>()){
            if (!child.enabled)
                child.enabled = true;
        }
        FpsGameObject.transform.position = transform.position + Vector3.down*0.8f;
        yield return  new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
