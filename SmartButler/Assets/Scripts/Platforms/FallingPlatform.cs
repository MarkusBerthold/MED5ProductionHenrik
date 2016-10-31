using UnityEngine;

namespace Assets.Scripts.Platforms{
	public class FallingPlatform : MonoBehaviour{
		private Animator _animator;
		private bool _isFalling;
		private GameObject _parentGameObject;
		private Rigidbody _rigidbody;
		private Vector3 _startPos;

		//Initialises variables
		private void Start(){
			_rigidbody = gameObject.GetComponent<Rigidbody>();
			_parentGameObject = transform.parent.gameObject;
			_animator = GetComponent<Animator>();
			_startPos = transform.position;
		}

		/// <summary>
		/// Resets position
		/// </summary>
		public void ResetStartPos(){
			transform.position = _startPos;
			_animator.StartPlayback();
			_isFalling = false;
			_rigidbody.useGravity = false;
			_rigidbody.isKinematic = true;
		}

		//If player collides with this object, start falling
		private void OnTriggerEnter(Collider other){
			if ((other.gameObject.tag == "Player") & !_isFalling){
				_animator.Stop();
				_isFalling = true;
				_rigidbody.useGravity = true;
				_rigidbody.isKinematic = false;
			}
		}
	}
}