using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.GameManager{
    public class EndLightLevel : MonoBehaviour{
        [SerializeField] private Text _loadingText;

        private bool _loadScene;

        [SerializeField] private int _scene;

        public GameObject Canvas;

        public bool Triggered;

        private void Start(){
            Canvas.SetActive(false);
        }

        // Updates once per frame
        private void Update(){
            // If the player has pressed the space bar and a new scene is not loading yet...
            if (Triggered && (_loadScene == false)){
                Canvas.SetActive(true);

                // ...set the loadScene boolean to true to prevent loading a new scene more than once...
                _loadScene = true;

                // ...change the instruction text to read "Loading..."
                _loadingText.text = "Loading...";


                // ...and start a coroutine that will load the desired scene.
                StartCoroutine(LoadNewScene());
            }

            // If the new scene has started loading...
            if (_loadScene)
                _loadingText.color = new Color(_loadingText.color.r, _loadingText.color.g, _loadingText.color.b,
                    Mathf.PingPong(Time.time, 1));
        }

        //Triggers if the the player objects collides with this object
        private void OnTriggerEnter(Collider c){
            if (c.tag == "Player")
                Triggered = true;
        }


        // The coroutine runs on its own at the same time as Update() and takes an integer indicating which scene to load.
        private IEnumerator LoadNewScene(){
            // This line waits for 3 seconds before executing the next line in the coroutine.
            // This line is only necessary for this demo. The scenes are so simple that they load too fast to read the "Loading..." text.
            //yield return new WaitForSeconds(3);

            // Start an asynchronous operation to load the scene that was passed to the LoadNewScene coroutine.
            //AsyncOperation async = Application.LoadLevelAsync(scene);
            var async = SceneManager.LoadSceneAsync(_scene);

            // While the asynchronous operation to load the new scene is not yet complete, continue waiting until it's done.
            while (!async.isDone) yield return null;
        }
    }
}