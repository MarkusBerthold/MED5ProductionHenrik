using UnityEngine;

namespace Assets.Scripts.Controllers{
    public class LightController : MonoBehaviour{
        //public Color color;
        public float Color; //changes the color of the lights: mesured in degrees 0-360
        public float Intensity; //sets the intensity of the lights
        public Light[] KitchenGlows;

        public Light[] KitchenLights;
        public int OnOff = 1; //turns the lights: on=1, off=0 or whatever
        public Light[] SofaGlows;
        //public Color kitchenColor;
        //public float kitchenIntensity;
        //////////////////////////////////////////////////
        public Light[] SofaLights;
        //public Color sofaColor;
        //public float sofaIntensity;
        //////////////////////////////////////////////////
        public int SofaOrKitchen = 1; //select light group: kitchen=1, sofa=whatever

        //Runs each frame and sets states to values
        private void Update(){
            TurnOnOff(OnOff);
            SetColorOfLight(Color, SofaOrKitchen);
            SetIntensityOfLight(Intensity, SofaOrKitchen);
            //changingValues();
        }

        /// <summary>
        /// Turns lights on or off
        /// Takes an int as a binary value, either 0 = off or 1 = on
        /// </summary>
        /// <param name="onOff"></param>
        public void TurnOnOff(int onOff){
            foreach (var light in KitchenLights)
                if ((onOff == 0) && light.enabled) light.enabled = false;
                else if ((onOff == 1) && (light.enabled == false)) light.enabled = true;
            foreach (var light in KitchenGlows)
                if ((onOff == 0) && light.enabled) light.enabled = false;
                else if ((onOff == 1) && (light.enabled == false)) light.enabled = true;
            //missing a way to turn on/off the sofa lights without also turn on/off the kitchen lights
        }

        /// <summary>
        /// Sets the color of the lights
        /// Takes a float which is the value of the color and an int which denotes 
        /// which lights are being manipulated
        /// </summary>
        /// <param name="_color"></param>
        /// <param name="sofaOrKitchen"></param>
        public void SetColorOfLight(float _color, int sofaOrKitchen){
            if (sofaOrKitchen == 1){
                for (var i = 0; i < KitchenLights.Length; i++)
                    KitchenLights[i].color = UnityEngine.Color.HSVToRGB(_color/360.0f, 1.0f, 1.0f);
                for (var i = 0; i < KitchenGlows.Length; i++)
                    KitchenGlows[i].color = UnityEngine.Color.HSVToRGB(_color/360.0f, 1.0f, 1.0f);
            }
            else{
                for (var i = 0; i < SofaLights.Length; i++)
                    SofaLights[i].color = UnityEngine.Color.HSVToRGB(_color/360.0f, 1.0f, 1.0f);
                for (var i = 0; i < SofaGlows.Length; i++)
                    SofaGlows[i].color = UnityEngine.Color.HSVToRGB(_color/360.0f, 1.0f, 1.0f);
            }
        }

        /// <summary>
        /// Sets intesity of lights
        /// Takes a float which is intensity and an int which denotes
        /// which lights are being manipulated
        /// </summary>
        /// <param name="_intensity"></param>
        /// <param name="sofaOrKitchen"></param>
        public void SetIntensityOfLight(float _intensity, int sofaOrKitchen){
            if (sofaOrKitchen == 1){
                for (var i = 0; i < KitchenLights.Length; i++) KitchenLights[i].intensity = _intensity;
                for (var i = 0; i < KitchenGlows.Length; i++) KitchenGlows[i].intensity = _intensity;
            }
            else{
                for (var i = 0; i < SofaLights.Length; i++) SofaLights[i].intensity = _intensity;
                for (var i = 0; i < SofaGlows.Length; i++) SofaGlows[i].intensity = _intensity;
            }
        }

        /*
    void changingValues () {
        //kitchen area
        for (int i = 0; i < kitchenLights.Length; i++) {
            kitchenLights[i].color = kitchenColor;
            kitchenLights[i].intensity = kitchenIntensity;
            //kitchenLights[i].intensity = Mathf.Lerp(kitchenLights[i].intensity, kitchenIntensity, Time.deltaTime);
        }
        for (int i = 0; i < kitchenGlows.Length; i++) {
            kitchenGlows[i].color = kitchenColor;
            kitchenGlows[i].intensity = kitchenIntensity;
        }
        //////////////////////////////////////////////////
        //sofa area
        for (int i = 0; i < sofaLights.Length; i++) {
            sofaLights[i].color = sofaColor;
            sofaLights[i].intensity = sofaIntensity;
        }
        for (int i = 0; i < sofaGlows.Length; i++) {
            sofaGlows[i].color = sofaColor;
            sofaGlows[i].intensity = sofaIntensity;
        }
    }
    */
    }
}