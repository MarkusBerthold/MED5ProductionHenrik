using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GameManager{
    public class SceneLoader : MonoBehaviour{
        public enum Scene{ //not sure how this works but it's used for switching between the scenes
            LivingRoom,
            Clock,
            Stereo,
            Light
        }

        private readonly float _distanceThreshold = 9.0f;
        private GameStateManager _gameStateManager;
        private bool _loading;
        public GameObject Canvas;
        public bool IsEnterable = false;
        public Text LoadingText;
        public Scene TargetScene, CurrentScene;

        private void Awake(){
            _gameStateManager = FindObjectOfType<GameStateManager>();
        }

        /// <summary>
        /// Loads a scene and shows "loading" text
        /// </summary>
        public void LoadScene(){
            Canvas.SetActive(true);

            // ...set the loadScene boolean to true to prevent loading a new scene more than once...
            _loading = true;

            // ...change the instruction text to read "Loading..."
            LoadingText.text = "Level complete \n Loading...";

            // ...and start a coroutine that will load the desired scene.
            StartCoroutine(LoadNewScene());
        }

        // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
        private IEnumerator LoadNewScene(){
            // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
            //AsyncOperation async = Application.LoadLevelAsync(scene);
            var async = SceneManager.LoadSceneAsync(TargetScene.ToString());

            // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
            while (!async.isDone)
                yield return null;

            switch (CurrentScene){
                case Scene.LivingRoom:
                    break;
                case Scene.Clock:
                    _gameStateManager.ChangeCurrentState(GameStateManager.State.Clock);
                    break;
                case Scene.Stereo:
                    _gameStateManager.ChangeCurrentState(GameStateManager.State.Stereo);
                    break;
                case Scene.Light:
                    _gameStateManager.ChangeCurrentState(GameStateManager.State.Light);
                    break;
            }
        }

        //Checks the camera location and sets a scene to be enterable
        private void OnMouseDown(){
            var distanceToCam = Vector3.Distance(transform.position, Camera.main.gameObject.transform.position);
            if (IsEnterable && (distanceToCam <= _distanceThreshold))
                LoadScene();
        }

        //If player collides with this object, load a scene
        private void OnTriggerEnter(Collider coll){
            if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "ThirdPerson")
                LoadScene();
        }
    }
}