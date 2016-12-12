using System.Runtime.InteropServices;
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

        public SmallPieceIPChar _SmallPieceIPChar;
        public void Start(){
            
            _renderer = GetComponent<Renderer>();
            originalColor = _renderer.material.color;

            _SmallPieceIPChar = new SmallPieceIPChar(this.gameObject, IpAdressValue);

        }

        public void Update()
        {
            _SmallPieceIPChar._plane.transform.position = this.transform.position + new Vector3(0.06f, 0, -0.06f);
                    
        }

        /// <summary>
        /// Lights up material with blue color
        /// </summary>
        public void LightUp(){
			_renderer.material.color = Color.green;
        }

        /// <summary>
        /// Resets the colour
        /// </summary>
        public void UnLit(){
            _renderer.material.color = originalColor;
        }   
    }

    public class SmallPieceIPChar
    {
        public Texture2D _IPCharTexture;
        public GameObject _plane;
        public Vector3 _pos;
        public Quaternion _rot;
        public GameObject _smallPiece;

        public SmallPieceIPChar(GameObject smallpiece, char IP)
        {
            _IPCharTexture = Resources.Load("Textures/TEXT/" + IP) as Texture2D;
            _smallPiece = smallpiece;
            _pos = _smallPiece.transform.position;
            _plane = MakePlane(_pos);
            //_plane.transform.SetParent(_smallPiece.transform);
            ApplyTexture();

            if (_smallPiece.transform.parent.transform.rotation.y == 0)
            {
                _plane.transform.Rotate(90, 180, 0);
                _plane.transform.position += Vector3.back * 0.1f;
            } else if (_smallPiece.transform.parent.transform.rotation.y < 0)
            {
                _plane.transform.Rotate(90, 90, 0);
                _plane.transform.position += new Vector3(0.8f,0,0);
            }
            
        }

        public GameObject MakePlane(Vector3 pos)
        {
            _plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
            _plane.transform.position = pos;
            _plane.transform.localScale = Vector3.one*0.1f;          
            return _plane;
        }

        public void ApplyTexture()
        {
            _plane.GetComponent<Renderer>().material.mainTexture = _IPCharTexture;
        }
    }
}