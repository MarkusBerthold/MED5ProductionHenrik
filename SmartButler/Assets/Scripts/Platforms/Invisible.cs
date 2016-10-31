using UnityEngine;

namespace Assets.Scripts.Platforms{
    public class Invisible : MonoBehaviour
    {
        private bool _isIvisible = false;

        public bool IsIvisible
        {
            set { _isIvisible = value; }
        }

        // Use this for initialization
        void Start()
        {
            gameObject.SetActive(_isIvisible);
        }

        public void EnablePlatform()
        {
            if (!_isIvisible)
            {
                _isIvisible = !_isIvisible;
                gameObject.SetActive(_isIvisible);
            }
        }
    }
}