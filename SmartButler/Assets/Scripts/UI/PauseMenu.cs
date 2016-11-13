using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public GameObject UI;
    private GameObject PauseMenuPrefab;

    void Awake(){
        PauseMenuPrefab = Resources.Load("PauseMenuPrefab") as GameObject;
    }

    void Start(){
        if (!(UI = GameObject.FindWithTag("PauseMenu")))
            UI = Instantiate(PauseMenuPrefab);
        TogglePauseMenu();
    }

    void Update(){
        if (Input.GetKeyDown("escape"))
            TogglePauseMenu();
    }

    public void TogglePauseMenu(){
        // not the optimal way but for the sake of readability
        if (UI.GetComponentInChildren<Canvas>().enabled){
            UI.GetComponentInChildren<Canvas>().enabled = false;
            Time.timeScale = 1.0f;
        }
        else{
            UI.GetComponentInChildren<Canvas>().enabled = true;
            Time.timeScale = 0f;
        }
    }


    public void OnExit(){
        AutoFade.LoadLevel(0, 3, 1, Color.black);
    }
}