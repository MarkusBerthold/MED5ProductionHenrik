using UnityEngine;

namespace Assets.Scripts.GameManager{
    public class DayNightController : MonoBehaviour{
        [Range(0, 1)] public float CurrentTimeOfDay;

        private readonly float _secondsInFullDay = 2000;

        public Light Sun;

        private float _sunInitialIntensity;

        [HideInInspector] public float TimeMultiplier = 1f;

        private void Start(){
            _sunInitialIntensity = Sun.intensity;
           // Sun = GetComponentInChildren<Light>();
        }

        private void Update(){
            UpdateSun();

            CurrentTimeOfDay += Time.deltaTime/_secondsInFullDay*TimeMultiplier;

            if (CurrentTimeOfDay >= 1) CurrentTimeOfDay = 0;
        }

        /// <summary>
        /// Updates variables and set new intensity for sun
        /// </summary>
        private void UpdateSun(){
            Sun.transform.localRotation = Quaternion.Euler(CurrentTimeOfDay*360f - 90, 170, 0);

            float intensityMultiplier = 1;
            if ((CurrentTimeOfDay <= 0.23f) || (CurrentTimeOfDay >= 0.75f)) intensityMultiplier = 0;
            else if (CurrentTimeOfDay <= 0.25f) intensityMultiplier = Mathf.Clamp01((CurrentTimeOfDay - 0.23f)*(1/0.02f));
            else if (CurrentTimeOfDay >= 0.73f)
                intensityMultiplier = Mathf.Clamp01(1 - (CurrentTimeOfDay - 0.73f)*(1/0.02f));

            Sun.intensity = _sunInitialIntensity*intensityMultiplier;
        }
    }
}