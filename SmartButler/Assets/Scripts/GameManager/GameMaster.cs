using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.GameManager{
    public class GameMaster : MonoBehaviour
    {

        /// <summary>
        /// Resets the current scene
        /// </summary>
        public void ResetLevel(){
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
            /*eeewwwww this is a really bad way of resetting the level, 
         * should be done by resetting the elements of the level 
           it works for this level because it is so light fix for beta!*/
        }
    }
}
