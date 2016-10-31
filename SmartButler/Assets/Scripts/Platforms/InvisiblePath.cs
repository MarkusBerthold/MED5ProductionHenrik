using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Platforms{
    public class InvisiblePath : MonoBehaviour{
        private bool _pathVisible;
        private readonly List<Invisible> path = new List<Invisible>();

        private void Awake(){
            var i = GetComponentsInChildren<Invisible>();
            foreach (var invis in i) path.Add(invis);
        }

        /// <summary>
        ///     Makes the path visible
        /// </summary>
        public void MakePathVisible(){
            foreach (var invis in path) invis.EnablePlatform();
        }
    }
}