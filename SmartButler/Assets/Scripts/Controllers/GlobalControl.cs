using UnityEngine;

namespace Assets.Scripts.Controllers{
    public class GlobalControl : MonoBehaviour{
        public static GlobalControl Instance;

        // Write all the variables for each thing that needs to be saved. 
        // e.g. public PlayerStatistics savedPlayerData = new PlayerStatistics();

        // In each script that has values that needs to be saved, needs to add: a save function e.g. 
        // public void SavePlayer()
        // {
        //           GlobalControl.Instance.savedPlayerData = localPlayerData;        
        // }
        //
        //              https://www.sitepoint.com/saving-data-between-scenes-in-unity/

        
        //Instantiate this and delete other instances of this object
        private void Awake(){
            if (Instance == null){
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this){
                Destroy(gameObject);
            }
        }
    }
}