﻿using Assets.Characters.FirstPersonCharacter.Scripts;
using Assets.Scripts.Controllers;
using Assets.Scripts.GameManager;
using UnityEngine;
using Assets.Characters.ThirdPerson;
using Assets.Scripts.MessageingSystem;

namespace Assets.Scripts.Character {
    public class Despawner : MonoBehaviour {
        //private fallingPlatform[] _allFallingPlatforms;
        //public Transform CurrentCheckpoint;
        public Vector3 Offset = Vector3.up; // is used for when a player is killed so that the player doesn't spawn inside the cog
        private FirstPersonController _player;
        private ThirdPersonCharacter _thirdPersonCharacter;
        private LightRotation _lightRotation;

        //Initialise
        private void Start(){
            _player = FindObjectOfType<FirstPersonController>();
            _thirdPersonCharacter = FindObjectOfType<ThirdPersonCharacter>();
            _lightRotation = FindObjectOfType<LightRotation>();
            //CurrentCheckpoint = GameObject.Find("RespawnPoint").transform;
            //_allFallingPlatforms = FindObjectsOfType<fallingPlatform>();
        }

        /*
        /// <summary>
        /// Reset falling platforms
        /// </summary>
        private void ResetFallingPlatforms()
        {
            foreach (var f in _allFallingPlatforms) f.resetStartPos();
        }
        */
        /*
        /// <summary>
        /// Changes the current checkpoint to a provided Transform 'trans'.
        /// </summary>
        /// <param name="trans"></param>
        public void SetCurrentCheckpoint(Transform trans)
        {
            CurrentCheckpoint = trans;
        }
        */
        //If the provided Collider 'coll' hits the player, reset the falling platforms
        //and move the player back to the last checpoint
        private void OnTriggerEnter(Collider coll){
            switch (coll.gameObject.tag){
                case "Player":
                    _player.RespawnChar();
				//EventManager.TriggerEvent ("emmacollides");
                    break;
			case "ThirdPerson":
				_thirdPersonCharacter.RespawnChar ();
				_lightRotation.ResetLevel ();
				EventManager.TriggerEvent ("emmacollides");
                    break;
            }

            //_player.transform.rotation = Quaternion.Euler(0, 90, 0);
                //_player.transform.position = CurrentCheckpoint.transform.position + Offset;
                //ResetFallingPlatforms();
        }
        }
    }