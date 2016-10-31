using UnityEngine;
using System.Collections;

public class WaveformFloor : MonoBehaviour {

    public float perlinScale;
    public float waveSpeed;
    public float waveHeight;
    public float offset;
    public GameObject FloorTile;
    private float FloorTileWidth;
    private float FloorTileDepth;


    void Start() {
    /*    if (FloorTile != null){
            Bounds b = FloorTile.GetComponent<MeshRenderer>().bounds;
            FloorTileWidth = b.size.x;
            FloorTileDepth = b.size.z;
        }


        for (int j = 0, y = 0; y <= 10; y++) 
            for (int x = 0; x <= 10; x++, j++) 
                GameObject.Instantiate(FloorTile,
                    new Vector3(transform.position.x+x * FloorTileWidth, transform.position.y, transform.position.z + y * FloorTileDepth),Quaternion.identity);

            


        MeshFilter[] meshFilters = GetComponentsInChildren<MeshFilter>();
        CombineInstance[] combine = new CombineInstance[meshFilters.Length];
        int i = 0;
        while (i < meshFilters.Length) {
            combine[i].mesh = meshFilters[i].sharedMesh;
            combine[i].transform = meshFilters[i].transform.localToWorldMatrix;
            meshFilters[i].gameObject.active = false;
            i++;
        }
        transform.GetComponent<MeshFilter>().mesh = new Mesh();
        transform.GetComponent<MeshFilter>().mesh.CombineMeshes(combine);
        transform.gameObject.active = true;*/
    }

    void Update() {
        CalcNoise();
    }

    void CalcNoise() {
        MeshFilter mF = GetComponent<MeshFilter>();
        MeshCollider mC = GetComponent<MeshCollider>();

        mC.sharedMesh = mF.mesh;

        Vector3[] verts = mF.mesh.vertices;

        for (int i = 0; i < verts.Length; i++) {
            float pX = (verts[i].x * perlinScale) + (Time.timeSinceLevelLoad * waveSpeed) + offset;
            float pZ = (verts[i].z * perlinScale) + (Time.timeSinceLevelLoad * waveSpeed) + offset;
            verts[i].y = Mathf.PerlinNoise(pX, pZ) * waveHeight;
        }

        mF.mesh.vertices = verts;
        mF.mesh.RecalculateNormals();
        mF.mesh.RecalculateBounds();
    }
}
