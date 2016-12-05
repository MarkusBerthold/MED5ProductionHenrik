using System;
using UnityEngine;
using System.Collections;
using Assets.Scripts.MessageingSystem;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour {
    public GameObject UI;
    private Canvas _uiCanvas;
    private GameObject PauseMenuPrefab;

    void Awake(){
        PauseMenuPrefab = Resources.Load("PauseMenuPrefab") as GameObject;
    }

    void Start(){
        if (!(UI = GameObject.FindWithTag("PauseMenu")))
            UI = Instantiate(PauseMenuPrefab);
        _uiCanvas = UI.GetComponentInChildren<Canvas>();
        _uiCanvas.worldCamera = Camera.main;
        TogglePauseMenu();
    }

    void Update(){
        if (Input.GetKeyDown("escape"))
            TogglePauseMenu();
    }

    public void TogglePauseMenu(){
        // not the optimal way but for the sake of readability
        if (_uiCanvas.enabled){
            _uiCanvas.enabled = false;
            //Cursor.visible = false;
            //LockCursor.DisableCursor();
            EventManager.TriggerEvent("EnableControls");
            Time.timeScale = 1.0f;

        }
        else{
            _uiCanvas.enabled = true;
            //Cursor.visible = true;
            //LockCursor.EnableCursor();

            EventManager.TriggerEvent("DisableControls");
            Time.timeScale = 0f;
        }
    }


    public void OnExit(){
        TogglePauseMenu();
        AutoFade.LoadLevel(0, 1, 1, Color.black);
    }
}