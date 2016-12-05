using UnityEngine;
using System.Collections;

class AnimateTiledTexture : MonoBehaviour {
    public int Columns = 2;
    public int Rows = 2;
    public float FramesPerSecond = 10f;

    //the current frame to display
    private int _index = 0;

    void Start() {
        StartCoroutine(UpdateTiling());

        //set the tile size of the texture (in UV units), based on the rows and columns
        Vector2 size = new Vector2(1f / Columns, 1f / Rows);
        GetComponent<Renderer>().sharedMaterial.SetTextureScale("_MainTex", size);
    }

    /// <summary>
    /// ?
    /// </summary>
    /// <returns></returns>
    private IEnumerator UpdateTiling() {
        while (true) {
            //move to the next index
            _index++;
            if (_index >= Rows * Columns)
                _index = 0;

            //split into x and y indexes
            Vector2 offset = new Vector2((float)_index / Columns - (_index / Columns), //x index
                                           (_index / Columns) / (float)Rows);          //y index

            GetComponent<Renderer>().sharedMaterial.SetTextureOffset("_MainTex", offset);

            yield return new WaitForSeconds(1f / FramesPerSecond);
        }

    }
}