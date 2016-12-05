using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.Scripts.Controllers {
    [Serializable]
    public class LightController {
        public LightController(float maxIntensity){
            _maxIntensity = maxIntensity;
            _currentIntensity = _maxIntensity/2;
            InitializeLights();
        }

        [Persistent] private static bool loaded;
        [Persistent] private static Color _savedColor = Color.white;
        [Persistent] private static float _savedIntensity;
        [Persistent] private static bool _savedIsOn;

        private readonly float _maxIntensity;

        private readonly List<Light> lights = new List<Light>();


        private Color _currentColor = Color.white;

        private float _currentIntensity;
        private bool _isOn;



        private void InitializeLights() {
            Light[] allLightsInScene = Object.FindObjectsOfType<Light>();
            foreach (Light l in allLightsInScene)
                if (l.tag != "MainDirectionalLight") {
                    l.enabled = false;
                    lights.Add(l);
                }
            if (loaded) {
                Debug.Log("extract Light Values" + _savedColor + "   " + _savedIntensity + "    " + _savedIsOn);
                ChangeHue(_savedColor);
                ChangeIntensity(_savedIntensity);
                SetActive(_savedIsOn);
            }
            else {
                _savedColor = Color.white;
                _savedIntensity = _currentIntensity;
                loaded = true;
            }
        }

        public void ChangeIntensity(float ZeroToOne){
            _currentIntensity = ZeroToOne*_maxIntensity;

            _savedIntensity = _currentIntensity;

            if (!_isOn)
                FlipLights();
            UpdateIntensity();
        }

        public void ChangeHue(Color color){
            _currentColor = color;
            _savedColor = _currentColor;
            UpdateHue();
        }


        public void FlipLights(){
            _isOn = !_isOn;
            _savedIsOn = _isOn;
            SetActive(_isOn);
            UpdateHue();
            UpdateIntensity();
        }

        public void SetLightsClear(){
            ChangeHue(Color.white);
        }



        private void UpdateIntensity(){
            foreach (Light l in lights)
                l.intensity = _currentIntensity;
        }

        private void SetActive(bool enabled){
            foreach (Light light in lights)
                light.enabled = enabled;
        }

        private void UpdateHue(){
            foreach (Light l in lights)
                l.color = _currentColor;
        }
    }
}