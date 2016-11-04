using UnityEngine;

namespace Assets.Scripts.StereoPuzzleV2 {
    [RequireComponent(typeof(MeshCollider))]
    [RequireComponent(typeof(Rigidbody))]
    public class SmallPiece : MonoBehaviour {

        //public bool IsKey;
        public char IpAdressValue;

        private Renderer _renderer;
        private Color originalColor;
        //initisalises _renderer
        private void Start(){
            
            _renderer = GetComponent<Renderer>();
            originalColor = _renderer.material.color;
        }

        /// <summary>
        /// Lights up material with blue color
        /// </summary>
        public void LightUp(){
            _renderer.material.color = Color.red;
        }

        /// <summary>
        /// Resets the colour
        /// </summary>
        public void UnLit(){
            _renderer.material.color = originalColor;
        }   
    }
}