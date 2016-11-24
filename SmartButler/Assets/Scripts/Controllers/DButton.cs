using UnityEngine;
using System.Collections;
using Assets.Scripts.GameManager;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.Controllers {
    public class DButton : MonoBehaviour {
        //private GameStateManager _gameStateManager;

        public AudioSource _coffeAudioSource;

        //When the mouse is pressed, enable LightSwitcher and change the state
        private void OnMouseDown(){
            if (this.tag == "CoffeeButton"){
                if (!LightSwitcher.IsFixed)
                    EventManager.TriggerEvent("coffeebutton");
            }
            else if (this.tag == "RemoteController"){

                EventManager.TriggerEvent("remotecontrol");


                EventManager.TriggerEvent("StereoButton"); 
            }
            else if (this.tag == "Speaker"){
                GameStateManager.Instance.ChangeState(GameStateManager.State.Stereo);
            }
        }
    }
}