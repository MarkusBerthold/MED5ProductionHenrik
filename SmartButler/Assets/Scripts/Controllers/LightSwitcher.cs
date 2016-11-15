using Assets.Scripts.ObjectInteraction;
using UnityEngine;

namespace Assets.Scripts.Controllers{
    public class LightSwitcher : MonoBehaviour{
        private float _currentIntensity;
        private float _targetedIntensity = 1;
        public Light[] AllLights;
        public float Degrees;
        public bool IsActive = true;

        public float MaxIntensity = 0.5f;
        public ObjectRotater[] Rotaters;

        //Initialises AllLights
        private void Start(){
            AllLights = FindObjectsOfType(typeof(Light)) as Light[];
            foreach (var l in AllLights) l.enabled = false;
        }

        //Runs once per frame
        private void Update(){
            if (IsActive){
                //change colors for all lights
                foreach (var objectRotater in Rotaters)
                    if (objectRotater.IsActive && objectRotater.IsRotating)
                        for (var i = 0; i < AllLights.Length; i++)
                            AllLights[i].color = Color.HSVToRGB(Degrees/360, 1, 1);

                
            }
        }

        /// <summary>
        /// Sets intensity of lights to parameter
        /// Takes a float which is the intensity
        /// </summary>
        /// <param name="intensity"></param>
        public void SetIntensity(float intensity){
            _targetedIntensity = intensity*MaxIntensity;
            //change intensity for all lights
            foreach (var light in AllLights)
                light.intensity = _targetedIntensity;
        }

        /// <summary>
        /// Returns light color to white
        /// </summary>
        public void SetLightsClear(){
            for (var i = 0; i < AllLights.Length; i++)
                AllLights[i].color = Color.white;
        }

        /// <summary>
        /// Updates the hue of lights
        /// Takes an int which is the degrees
        /// </summary>
        /// <param name="degrees"></param>
        public void UpdateLightsHue(int degrees){
            Degrees = degrees;
        }

        /// <summary>
        /// Enables all lights
        /// </summary>
        public void SwitchEnable(){
            foreach (var l in AllLights) l.enabled = !l.enabled;
            IsActive = !IsActive;
        }
    }
}