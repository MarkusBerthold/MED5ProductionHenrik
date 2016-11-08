using UnityEngine;

namespace Assets.Scripts.Highlighting{
    public class Highlighter : MonoBehaviour {

        public float DistanceThreshold = 1.0f; //???
        public Shader OutlineShader;
        private Material[] _defaultMaterials;
        private Material[] _currentMaterials;
        public int[] Indexes;
        public Transform Pivot;

        void Start() {
            if (!Pivot)
                Pivot = transform;
        
            _defaultMaterials = GetComponent<Renderer>().materials;
            _currentMaterials = _defaultMaterials;
        }

        //Checks the placement of the camera and sets outlineShader
        void OnMouseOver() {
            float distanceToCam = Vector3.Distance(Pivot.position, Camera.main.gameObject.transform.position);

            if (distanceToCam < DistanceThreshold) { //distance check
                //set all requested shaders to outlineShader
                foreach (int i in Indexes) {
                    _currentMaterials[i].shader = OutlineShader;
                }
            }
            Debug.Log(OutlineShader.name);
        }

        //Resets shaders to normal
        void OnMouseExit() {
            //set all requested shaders to normal
            foreach (int i in Indexes) {
                _currentMaterials[i].shader = Shader.Find("Standard");
            }
        }

    }
}