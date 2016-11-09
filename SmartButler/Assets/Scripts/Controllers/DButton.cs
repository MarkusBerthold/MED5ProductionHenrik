using UnityEngine;
using System.Collections;
using Assets.Scripts.GameManager;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.Controllers{
    public class DButton : MonoBehaviour{
        //private GameStateManager _gameStateManager;

        public StereoController StereoController;
        public LightSwitcher LightSwitcher;

        //Initalise the _gameStateManager
        private void Awake(){
            //_gameStateManager = FindObjectOfType<GameStateManager>();
        }

        //When the mouse is pressed, enable LightSwitcher and change the state
        private void OnMouseDown(){
            if (this.tag == "CoffeeButton"){
                EventManager.TriggerEvent("coffeebutton");
                LightSwitcher.SwitchEnable();
                GameStateManager.Instance.ChangeState(GameStateManager.State.Coffee);
            }else if (this.tag == "RemoteController" && !StereoController.Source.isPlaying){
                StereoController.StartStopPlayback(true);
            }
            else if (this.tag == "RemoteController" && StereoController.Source.isPlaying){
                StereoController.StartStopPlayback(false);
            }
        }
    }
}