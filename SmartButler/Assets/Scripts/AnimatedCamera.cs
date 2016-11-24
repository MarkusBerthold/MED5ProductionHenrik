using System.Collections;
using Assets.Characters.FirstPersonCharacter.Scripts;
using UnityEngine;

public class AnimatedCamera : MonoBehaviour {
    [Persistent] private static bool _loaded;

    public GameObject FpsGameObject;

    private void Awake(){
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void StartBehaviour(){
        //Debug.Log("AnimatedCamera ::: " + _loaded);
        if (_loaded){
            Destroy(gameObject);
        }
        else{
            enableDisableChildren(false);
        }
    }

    public IEnumerator Destroy(int waitTime){
        enableDisableChildren(true);
        FpsGameObject.transform.position = transform.position + Vector3.down*0.85f;
        yield return new WaitForSeconds(waitTime);

        _loaded = true;
        Destroy(gameObject);
    }


    public void enableDisableChildren(bool enable){
        foreach (Behaviour child in FpsGameObject.GetComponentsInChildren<Behaviour>()){
            Camera potentialCamera = child.GetComponentInChildren<Camera>();
            FirstPersonController potentialController = child.GetComponent<FirstPersonController>();
            if (potentialCamera)
                potentialCamera.enabled = enable;
            if (potentialController)
                potentialController.enabled = enable;
        }
    }
}