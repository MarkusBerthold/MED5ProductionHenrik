using System.Collections;
using Assets.Scripts.MessageingSystem;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.GameManager {
    /// <summary>
    ///     class for handling loading the four different scenes
    /// </summary>
    public class SceneLoader : MonoBehaviour {
        public enum Scene {
            LivingRoom,
            Clock,
            Stereo,
            Light,
            JohnTesting
        }

        private readonly float _distanceThreshold = 4.0f;
        private bool _loading;
        private UnityAction _someAction;
        public bool IsEnterable;

        public Scene TargetScene;

        private void Start(){
            _loading = false;
            _someAction = LoadScene;
            //    if (SceneManager.GetActiveScene().name.Equals("Stereo")) ;
            EventManager.StartListening("PuzzleIsSolved2", _someAction);
        }

        private void OnMouseDown(){
            float distanceToCam = Vector3.Distance(transform.position, Camera.main.gameObject.transform.position);
            if (IsEnterable && (distanceToCam <= _distanceThreshold))
                LoadScene();
        }

        //If player collides with this object, load a scene
        private void OnTriggerEnter(Collider coll){
            if (coll.gameObject.tag == "Player" || coll.gameObject.tag == "ThirdPerson")
                LoadScene();
        }

        /// <summary>
        ///     Loads a scene and shows "loading" text
        /// </summary>
        public void LoadScene(){
            if (!_loading){
                // ...set the loadScene boolean to true to prevent loading a new scene more than once...
                _loading = true;
                EventManager.TriggerEvent("SceneChange");
                StartCoroutine(LoadNewScene());

            }
        }

        private IEnumerator LoadNewScene(){
            //start fading
            AutoFade.LoadLevel(TargetScene.ToString(), 2, 1, Color.black);
            while (AutoFade.Fading)
                yield return null;
            _loading = false;
            Debug.Log("corutine ends");
        }        
    }
}