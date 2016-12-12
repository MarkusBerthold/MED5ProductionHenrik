using UnityEngine;
using System.Collections;
using Assets.Scripts.ClockLevel;

namespace Assets.Scripts.ClockItem
{
    public class fuckItUp : MonoBehaviour
    {
        private Component[] wallClockGearsZ;
        private Component[] wallClockGearsY;

        public GameObject wallClock;

        private bool DoOnce;

        // Use this for initialization
        void Start()
        {
            DoOnce = false;


        }

        // Update is called once per frame
        void OnMouseDown()
        {

            print("fucked it up1");

            if (this.tag == "Speaker" && !DoOnce)
            {
                DoOnce = true;

                wallClockGearsZ = wallClock.GetComponentsInChildren<gearRotationZaxis>();
                wallClockGearsY = wallClock.GetComponentsInChildren<GearRotationYAxis>();

                print("fucked it up2");
                for (int i = 0; i < wallClockGearsZ.Length; i++)
                {
                    wallClockGearsZ[i].GetComponent<gearRotationZaxis>().Rotspeed =
                        wallClockGearsZ[i].GetComponent<gearRotationZaxis>().Rotspeed*10;
                    
                }

                for (int i = 0; i < wallClockGearsY.Length; i++)
                {
                    wallClockGearsY[i].GetComponent<GearRotationYAxis>().Rotspeed =
                        wallClockGearsY[i].GetComponent<GearRotationYAxis>().Rotspeed*10;
                }
            }
        }
    }
}
