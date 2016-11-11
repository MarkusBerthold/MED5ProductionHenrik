using UnityEngine;

namespace Assets.Scripts{
    public class BloomScript : MonoBehaviour{
        private Renderer _renderer;
        private Material _mat;
        private Color _red;
        private GameObject _player;
        public float Intensity = 7;


        // Use this for initialization
        void Start (){
            _renderer = GetComponent<Renderer>();
            _player = GameObject.Find("FPSController");
            _mat = _renderer.material;
            ColorUtility.TryParseHtmlString("#FF1E1E", out _red);

        }

        // Update is called once per frame
        void Update () {
            this.SetEmissionIntensity();
        }

        /// <summary>
        /// Finds the distance between the player and the neon sign
        /// and uses the distance to diminish the glow the further away the player is.
        /// </summary>
        public void SetEmissionIntensity(){
            var distance = Vector3.Distance(this.transform.position, _player.transform.position);
            _mat.SetColor("_EmissionColor", _red * (Intensity - (distance / 2)));
        }
    }
}
