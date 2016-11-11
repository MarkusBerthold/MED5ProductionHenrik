using UnityEngine;

namespace Assets.Scripts.StereoPuzzleV2 {
    [RequireComponent(typeof (BoxCollider))]
    public class HookPiece : MonoBehaviour {
        public int IpPosition;
        private char IpPositionValue;
        private bool _aligned = false;
        private Puzzle puzzle;

        public SmallPiece _currentPiece;

        // private GameObject _ipChar;
        private IpCharacter _ipChar;


        private Renderer _ipCharRenderer;

        void Start(){
            puzzle = transform.parent.GetComponent<Puzzle>();
            IpPositionValue = puzzle.GetIpPositionValue(IpPosition);
            _ipChar = new IpCharacter(IpPositionValue, transform.position + Vector3.up*3.5f,
                Quaternion.LookRotation(this.transform.up));

            print(IpPositionValue);

            //Instantiate(_ipChar, transform.position + Vector3.up*3.5f, Quaternion.LookRotation(Vector3.back) );
        }


        private void OnTriggerEnter(Collider other){
            SmallPiece potentialPiece = other.gameObject.GetComponent<SmallPiece>();
            if (potentialPiece && !potentialPiece.Equals(_currentPiece)){
                _currentPiece = potentialPiece;
                CheckPieceAligned();
                Debug.Log("number of pieces aligned: " + puzzle.CheckHooks());
            }
            else{
                _currentPiece.UnLit();
                _currentPiece = null;
                Debug.Log("number of pieces aligned: " + puzzle.CheckHooks());
            }
        }

        private void OnTriggerExit(Collider other){
            SmallPiece potentialPiece;
            if ((potentialPiece = other.gameObject.GetComponent<SmallPiece>())){
                potentialPiece.UnLit();
            }
        }


        private void CheckPieceAligned(){
            if (_currentPiece != null)
                if (_currentPiece.IpAdressValue.Equals(IpPositionValue)){
                    //then piece is aligned yo!
                    _currentPiece.LightUp();
                    _ipChar.TurnOnLight();
                    _aligned = true;
                }
                else{
                    _currentPiece.UnLit();
                    _ipChar.TurnOffLight();
                    _aligned = false;
                }
        }

        // Getter for current piece
        public SmallPiece CurrentPiece{
            get { return _currentPiece; }
        }

        public bool Aligned{
            get { return _aligned; }
        }
    }


    /// <summary>
    /// helper class that stores the IpCharacters Functionality
    /// </summary>
    class IpCharacter {
        private GameObject _ipCharacterGameObject;
        private Renderer renderer;
        private Color originalColor;

        public IpCharacter(char ipChar, Vector3 pos, Quaternion rotation){
            _ipCharacterGameObject = GetIpCharObject(ipChar);
            _ipCharacterGameObject = Instantiate(pos, rotation);


            renderer = _ipCharacterGameObject.GetComponent<Renderer>();
            originalColor = renderer.material.color;
        }


        public GameObject GetIpCharObject(char ipChar){
            return Resources.Load(ipChar.ToString()) as GameObject;
        }

        public void TurnOnLight(){
            renderer.material.color = Color.red;
        }

        public void TurnOffLight(){
            renderer.material.color = originalColor;
        }

        GameObject Instantiate(Vector3 pos, Quaternion rotation){
            return GameObject.Instantiate(_ipCharacterGameObject, pos, rotation) as GameObject;
        }
    }
}