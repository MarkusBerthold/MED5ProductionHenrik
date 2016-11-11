using UnityEngine;
using System.Collections;

public class AnimatedCamera : MonoBehaviour {
    private Animator _animator;
    public GameObject FpsGameObject;

    void Start(){
        _animator = GetComponent<Animator>();
    }

	// Update is called once per frame
	void Update () {
	    if (_animator.GetCurrentAnimatorStateInfo(0).IsName("wakingCam")){
	        
	    }

	}

    public IEnumerator Destroy(){
        FpsGameObject.SetActive(true);
        yield return  new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
