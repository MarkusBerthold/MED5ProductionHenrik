using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {
    [SerializeField]
    private GameObject MenuPanel;
    [SerializeField]
    private GameObject CreditsPanel;
    [SerializeField]
    private GameObject InstructionsPanel;

    private bool _isViewingCredits = false;

    void Start() {
        LockCursor.EnableCursor();
    }

    public void OnCredits(){
        _isViewingCredits = !_isViewingCredits;
        MenuPanel.SetActive(!_isViewingCredits);
        CreditsPanel.SetActive(_isViewingCredits);
    }

    public void OnExit(){
        Application.Quit();
    }

    public void OnStartGame(){
        AutoFade.LoadLevel("LivingRoom", 2, 1, Color.black);
    }

    public void OnToInstruction(){
        MenuPanel.SetActive(false);
        InstructionsPanel.SetActive(true);
    }
}