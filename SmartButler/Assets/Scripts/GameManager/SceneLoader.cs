using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GameManager {


	/// <summary>
	/// class for handling loading the four different scenes
	/// </summary>
	public class SceneLoader : MonoBehaviour {
		public enum Scene {
			LivingRoom,
			Clock,
			Stereo,
			Light
		}

		private readonly float _distanceThreshold = 4.0f;
		private bool _loading;
		public GameObject Canvas;
		public bool IsEnterable;
		public Text LoadingText;
		public Scene TargetScene;

		void Start(){
			_loading = false;
		}

		private void OnMouseDown() {
			var distanceToCam = Vector3.Distance(transform.position, Camera.main.gameObject.transform.position);
			if (IsEnterable && (distanceToCam <= _distanceThreshold))
				LoadScene();

		}

		//If player collides with this object, load a scene
		private void OnTriggerEnter(Collider coll) {
			if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "ThirdPerson")
				LoadScene();

		}

		/// <summary>
		///     Loads a scene and shows "loading" text
		/// </summary>
		public void LoadScene() {
			if (!_loading) {
				// ...set the loadScene boolean to true to prevent loading a new scene more than once...
				_loading = true;
				Canvas.SetActive(true);

				// ...change the instruction text to read "Loading..."
				LoadingText.text = "Level complete \n Loading...";
				// ...and start a coroutine that will load the desired scene.
				StartCoroutine(LoadNewScene());
			}
		}


		private IEnumerator LoadNewScene() {
			// Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
			//AsyncOperation async = Application.LoadLevelAsync(scene);
			var async = SceneManager.LoadSceneAsync(TargetScene.ToString());
			// While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
			while (!async.isDone)
				yield return null;
			_loading = false;
			Debug.Log("corutine ends");
		}


		/* private void OnLevelFinishedLoading(UnityEngine.SceneManagement.Scene arg0, LoadSceneMode arg1) {
             if (_loading) {
                 switch (TargetScene) {
                     case Scene.LivingRoom:
                         GameStateManager.Init();
                         Debug.Log("we are loading to living room");
                         break;
                     case Scene.Clock:
                         //_gameStateManager.ChangeCurrentState(GameStateManager.State.Clock);
                         GameStateManager.ChangeCurrentState(GameStateManager.State.Clock);
                         break;
                     case Scene.Stereo:
                         //_gameStateManager.ChangeCurrentState(GameStateManager.State.Stereo);
                         GameStateManager.ChangeCurrentState(GameStateManager.State.Stereo);
                         break;
                     case Scene.Light:
                         //_gameStateManager.ChangeCurrentState(GameStateManager.State.Light);
                         GameStateManager.ChangeCurrentState(GameStateManager.State.Light);
                         break;
                 }
             }
         }*/

	}
}