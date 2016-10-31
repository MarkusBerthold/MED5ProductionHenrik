using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Character
{
    public class HoldCharacter : MonoBehaviour
    {
        // store all playercontrollers currently on platform
        private readonly Hashtable _onPlatform = new Hashtable();
        // used to calculate horizontal movement
        private Vector3 _lastPos;
        // height above the center of object the char must be kept
        public float VerticalOffset = 0.25f;

        //When the player collides with this, make sure they move with the platform
        private void OnTriggerEnter(Collider other)
        {
            var ctrl = other.GetComponent(typeof(CharacterController)) as CharacterController;
            // make sure we only move objects that are rigidbodies or charactercontrollers.
            // this to prevent we move elements of the level itself
            if (ctrl == null) return;
            var t = other.transform; // transform of character
            // we calculate the yOffset from the character height and center
            var yOffset = ctrl.height/2f - ctrl.center.y + VerticalOffset;
            var data = new Data(ctrl, t, yOffset);
            // add it to table of characters on this platform
            // we use the transform as key
            _onPlatform.Add(other.transform, data);
        }

        //stop when the player moves away
        private void OnTriggerExit(Collider other)
        {
            // remove (if in table) the uncollided transform
            _onPlatform.Remove(other.transform);
        }

        //
        private void Start()
        {
            _lastPos = transform.position;
        }

        /// <summary>
        /// </summary>
        private void LateUpdate()
        {
            var curPos = transform.position;
            var y = curPos.y; // current y pos of platform
            // we calculate the delta
            var delta = curPos - _lastPos;
            var yVelocity = delta.y;
            // remove y component of delta (as we use delta only for correcting
            // horizontal movement now...
            delta.y = 0f;
            _lastPos = curPos;
            // let's loop over all characters in the table
            foreach (DictionaryEntry d in _onPlatform)
            {
                var data = (Data) d.Value; // get the data
                var charYVelocity = data.ctrl.velocity.y;
                // check if char seems to be jumping
                if (charYVelocity == 0f)
                {
                    // no, lets do our trick!
                    var pos = data.t.position; // current charactercontroller position
                    pos.y = y + data.yOffset; // adjust to new platform height
                    pos += delta; // adjust to horizontal movement
                    data.t.position = pos; // and write it back!
                }
            }
        }

        // helper struct to contain the transform of the player and the
        // vertical Offset of the player (how high the center of the
        // charcontroller must be above the center of the platform)
        public struct Data
        {
            public Data(CharacterController ctrl, Transform t, float yOffset)
            {
                this.ctrl = ctrl;
                this.t = t;
                this.yOffset = yOffset;
            }

            public CharacterController ctrl; // the char controller
            public Transform t; // transform of char
            public float yOffset; // y Offset of char above platform center
        }
    }
}