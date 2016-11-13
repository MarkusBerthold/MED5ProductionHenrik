using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Platforms;
using UnityEditor;

public class TunnelLight : MonoBehaviour {

    public Vector3 TunnelEnter, TunnelExit;
    private double _threshold = 1;
    public float  Speed = 1 ;

    void Update(){
        transform.Translate(Vector3.right * Speed);
        if ((Vector3.Distance(transform.position, TunnelEnter) < _threshold) || transform.position.x >= TunnelEnter.x){
            transform.position = TunnelExit;
        }
        else if ((Vector3.Distance(transform.position, TunnelExit) < _threshold) || transform.position.x <= TunnelExit.x ) {
            transform.position = TunnelEnter;
        }
    }



}


/*
     private Color firstColor, secondColor;

         /*Light light;
        private Color firstColor, secondColor;
        private bool _spawnRight, _spawnBoth = true;
        public int lightDistance = 250;
        private static int numberOfLights = 0;

        public GameObject GameObjectToSpawn;


        void OnEnable(){
            if (transform.position.x <= -1243 || transform.position.x >= 1512 || numberOfLights >= 25){
                Destroy(gameObject);
            }
            light = GetComponent<Light>();

            if (SpawnBoth){
                SpawnBothWays();
                return;
            }
            GameObject gm = Instantiate(gameObject, transform.position, Quaternion.identity, transform) as GameObject;
            TunnelLight gmTunnelLight = gm.GetComponent<TunnelLight>();

            int spawnDirection = (SpawnRight) ? 1 : -1;
            gm.transform.position = transform.position + Vector3.right*lightDistance*spawnDirection;
            SetupLightObject(gmTunnelLight, SpawnRight);


        }
        

    void Start() {
        firstColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
        secondColor = new Color(Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f), Random.Range(0.0f, 1.0f));
    }
    void Update() {
        GetComponent<Light>().color = Color.Lerp(firstColor, secondColor, Mathf.PingPong(Time.time, 1));
    }


    private void SpawnBothWays(){
        GameObject gm;
        TunnelLight gmTunnelLight;
        for (int i = -1; i < 2; i += 2){
            gm =
                Instantiate(gameObject, transform.position + Vector3.right*lightDistance*i, Quaternion.identity,
                    transform) as
                    GameObject;
            gmTunnelLight = gm.GetComponent<TunnelLight>();
            SetupLightObject(gmTunnelLight, i > 0);
        }
    }

    void SetupLightObject(TunnelLight tl, bool direction){
        tl.SpawnBoth = false;
        tl.SpawnRight = direction;
        numberOfLights++;
        tl.gameObject.SetActive(true);
    }

    public bool SpawnRight{
        get { return _spawnRight; }
        set { _spawnRight = value; }
    }

    public bool SpawnBoth{
        get { return _spawnBoth; }
        set { _spawnBoth = value; }
    }

   private List<GameObject> _allLights = new List<GameObject>();
    public float LightRange = 30f, LightIntensity = 8;
    private MeshFilter mesh;


    // Use this for initialization
    void Start(){
        //  GenerateLightGameObjects();

        mesh = transform.parent.GetComponent<MeshFilter>();
        float meshMaxX = mesh.mesh.bounds.size.x;
        int numberOfLights = (int) meshMaxX/50;
        Vector3 currentSpawnPosition = transform.position + Vector3.right*LightRange/2;
        int currentLightNumber = 1;
        Debug.Log("meshMaxX: " + (meshMaxX / 2) + "meshMaxX + transform: " + (meshMaxX - transform.position.x));
      // while (currentSpawnPosition.x > transform.position.x - meshMaxX*2) {
      //     GameObject temp =
      //         Instantiate(new GameObject("Light"), currentSpawnPosition, Quaternion.identity) as GameObject;
      //     SetupLightGameObject(temp);
      // 
      // 
      // 
      //     currentSpawnPosition = transform.position + (Vector3.left *LightRange/2)*currentLightNumber;
      // }
    }

    private void SetupLightGameObject(GameObject gameObject){
        Light tempLight = gameObject.AddComponent<Light>();
        tempLight.range = LightRange;
        tempLight.intensity = LightIntensity;
        gameObject.transform.parent = transform;
        _allLights.Add(gameObject);
    }

    // Update is called once per frame
    void Update(){
     //   foreach (Light light in _allLights){
     //       if (light.transform.position.x > transform.position.x + mesh.mesh.bounds.size.x){
     //           light.transform.position = transform.position;
     //       }
     //       light.transform.position += Vector3.left*0.5f;
     //   }
    }
}
     */