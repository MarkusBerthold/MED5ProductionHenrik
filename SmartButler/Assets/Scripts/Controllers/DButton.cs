using UnityEngine;
using System.Collections;
using Assets.Scripts.GameManager;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.Controllers{
    public class DButton : MonoBehaviour{
        //private GameStateManager _gameStateManager;

        public LightSwitcher LightSwitcher;

        //Initalise the _gameStateManager
        private void Awake(){
            //_gameStateManager = FindObjectOfType<GameStateManager>();
        }

        //When the mouse is pressed, enable LightSwitcher and change the state
        private void OnMouseDown(){
			EventManager.TriggerEvent ("coffeebutton");
            LightSwitcher.SwitchEnable();
			GameStateManager.Instance.ChangeState(GameStateManager.State.Coffee);
        }
    }
}