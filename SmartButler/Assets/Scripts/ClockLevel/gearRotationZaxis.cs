using System.Collections;
using Assets.Scripts.ClockItem;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.ClockLevel{
    public class gearRotationZaxis : MonoBehaviour{
        public float Rotspeed;
        private string _sceneName;

        //Check which scene we are in and store it
        private void Start(){
            Scene currentScene = SceneManager.GetActiveScene();
            _sceneName = currentScene.name;
        }

        // Update is called once per frame
        private void Update(){
                    transform.Rotate(new Vector3(0,0,1) * Time.deltaTime * Rotspeed, Space.Self);
        }

        public void UpdateRotspeed(int rotspeed){
            Rotspeed = rotspeed;
        }

        /*
        /// <summary>
        /// Rotates in 1 second intervals. 
        /// Takes a float which is the rotation value
        /// </summary>
        /// <param name="rot"></param>
        /// <returns></returns>
        public IEnumerator WaitForSecond(float rot){
            yield return new WaitForSeconds(1);
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rot, Space.Self);
            RotateWaitSecond(rot);
        }

        /// <summary>
        /// Rotates in 1 minute intervals. 
        /// Takes a float which is the rotation value
        /// </summary>
        /// <param name="rot"></param>
        /// <returns></returns>
        public IEnumerator WaitForMinute(float rot){
            yield return new WaitForSeconds(60);
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rot, Space.Self);
            RotateWaitMinute(rot);
        }

        /// <summary>
        /// Rotates in 1 hour intervals. 
        /// Takes a float which is the rotation value
        /// </summary>
        /// <param name="rot"></param>
        /// <returns></returns>
        public IEnumerator WaitForHour(float rot)
        {
            yield return new WaitForSeconds(3600);
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * rot, Space.Self);
            RotateWaitMinute(rot);
        }

        /// <summary>
        /// Starts the rotation coroutine.
        /// Takes a float which is the rotation value
        /// </summary>
        /// <param name="rotspeed"></param>
        public void RotateWaitSecond(float rotspeed){
            StartCoroutine(WaitForSecond(rotspeed));
        }

        /// <summary>
        /// Starts the rotation coroutine.
        /// Takes a float which is the rotation value
        /// </summary>
        /// <param name="rotspeed"></param>
        public void RotateWaitMinute(float rotspeed){
            StartCoroutine(WaitForMinute(rotspeed));
        }

        /// <summary>
        /// Starts the rotation coroutine.
        /// Takes a float which is the rotation value
        /// </summary>
        /// <param name="rotspeed"></param>
        public void RotateWaitHour(float rotspeed)
        {
            StartCoroutine(WaitForHour(rotspeed));
        }

        public void RotateBroken(){
            transform.Rotate(new Vector3(0, 0, 1) * Time.deltaTime * Rotspeed, Space.Self);
        }*/
    }
}