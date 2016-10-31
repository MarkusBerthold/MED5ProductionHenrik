using UnityEngine;

namespace Assets.Scripts.Generators{
    public class MeshGenerator : MonoBehaviour{
        private void Awake(){
            var plane = new GameObject("Plane");
            var meshFilter = (MeshFilter) plane.AddComponent(typeof(MeshFilter));
            meshFilter.mesh = CreateMesh(100, 100f);
            var renderer = plane.AddComponent(typeof(MeshRenderer)) as MeshRenderer;
            renderer.material.shader = Shader.Find("Particles/Additive");
            var tex = new Texture2D(1, 1);
            tex.SetPixel(0, 0, Color.grey);
            tex.Apply();
            renderer.material.mainTexture = tex;
            renderer.material.color = Color.green;
        }

        // Use this for initialization
        private void Start(){
        }

        // Update is called once per frame
        private void Update(){
        }

        /// <summary>
        /// Creates a mesh with a specified width and height
        /// Takes 2 floats, a width and a height
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        private Mesh CreateMesh(float width, float height){
            var m = new Mesh();
            m.name = "ScriptedMesh";
            m.vertices = new[]{
                new Vector3(-width, -height, 0.01f),
                new Vector3(width, -height, 0.01f),
                new Vector3(width, height, 0.01f),
                new Vector3(-width, height, 0.01f)
            };
            m.uv = new[]{
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0)
            };
            m.triangles = new[]{0, 1, 2, 0, 2, 3};
            m.RecalculateNormals();

            return m;
        }
    }
}