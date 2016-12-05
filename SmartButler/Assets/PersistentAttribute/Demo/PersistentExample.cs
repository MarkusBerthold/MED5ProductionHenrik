using UnityEngine;

public class PersistentExample : MonoBehaviour {

    private static Transform player;

    //Just add [Persistent] before the declaration
    //It'll be automaticaly saved
    [Persistent]
    public static bool myBool;
    [Persistent]
    public static float myFloat;
    [Persistent]
    public static int myInt;

    //Useful for saving level and points
    [Persistent(defaultValue = 1)]
    public static int level;
    [Persistent]
    public static float points;

    //You can change the key that will be used on playerprefs
    //You can also set the default value in the initializer
    [Persistent("MyPlayerColor")]
    public static Color playerColor = Color.red;

    //You can save the player position and rotation
    //so when you load the game they it will be where you left it
    [Persistent]
    private static Vector3 position {
        get { return player ? player.position : Vector3.zero; }
        set { if(player) player.position = value; }
    }
    [Persistent]
    private static Quaternion rotation {
        get { return player ? player.rotation : Quaternion.identity; }
        set { if(player) player.rotation = value; }
    }

    //Check and change this values on the inspector while in playmode, stop playmode and start again, they will be the same as before
    public int instanceLevel;
    public float instancePoints;
    public Color instancePlayerColor;

    private void Awake() {
        player = transform;
    }

    private void Start() {
        instanceLevel = level;
        instancePoints = points;
        instancePlayerColor = playerColor;
    }

    private void Update() {
        position += Random.onUnitSphere * 3f * Time.deltaTime;
        rotation = Quaternion.RotateTowards(rotation, Random.rotation, 90f * Time.deltaTime);

        level = instanceLevel;
        points = instancePoints;
        GetComponent<MeshRenderer>().material.color = playerColor = instancePlayerColor;

        if(Input.GetKeyDown(KeyCode.Space)) {
            instanceLevel++;
        }
        if(Input.GetKey(KeyCode.Return)) {
            instancePoints += Time.deltaTime;
        }
    }
}