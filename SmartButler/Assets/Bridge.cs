using UnityEngine;
using System.Collections;
using Assets.Scripts.Platforms;
using UnityEngine.Events;
using Assets.Scripts.MessageingSystem;

public class Bridge : MonoBehaviour {
    private MoveToPoints[] movingPlatforms;
    private UnityAction _someListener;
    public int PuzzleNumber;


    void Start() {
        movingPlatforms = GetComponentsInChildren<MoveToPoints>();
        foreach (MoveToPoints platform in movingPlatforms) {
            platform.enabled = false;
        }
        _someListener = OnPuzzleSolved;
        EventManager.StartListening("PuzzleIsSolved" + PuzzleNumber, _someListener);

    }


    void OnEnable() {
    }

    void OnDisable() {
        EventManager.StopListening("PuzzleIsSolved" + PuzzleNumber, _someListener);
    }


    private void OnPuzzleSolved() {
        StartCoroutine(SpawnBridge());
    }

    private IEnumerator SpawnBridge() {
        foreach (MoveToPoints platforms in movingPlatforms) {
            platforms.enabled = true;
            yield return new WaitForSecondsRealtime(.5f);
        }
    }
}