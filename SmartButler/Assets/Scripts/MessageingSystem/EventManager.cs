using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.MessageingSystem{
    public class EventManager : MonoBehaviour{
        private static EventManager _eventManager;

        private Dictionary<string, UnityEvent> _eventDictionary;

        public static EventManager Instance{
            get{
                if (!_eventManager){
                    _eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                    if (!_eventManager)
                        Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
                    else
                        _eventManager.Init();
                }

                return _eventManager;
            }
        }

        /// <summary>
        /// Initialises variable
        /// </summary>
        private void Init(){
            if (_eventDictionary == null)
                _eventDictionary = new Dictionary<string, UnityEvent>();
        }

        /// <summary>
        /// Starts the event listener
        /// Takes a string, which is the name of the event and an UnityAction which is the listener
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public static void StartListening(string eventName, UnityAction listener){
            UnityEvent thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent)){
                thisEvent.AddListener(listener);
            }
            else{
                thisEvent = new UnityEvent();
                thisEvent.AddListener(listener);
                Instance._eventDictionary.Add(eventName, thisEvent);
            }
        }

        /// <summary>
        /// Stops the listener
        /// Takes 2 variables, a string which is the eventname and a UnityAction which is the listener
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="listener"></param>
        public static void StopListening(string eventName, UnityAction listener){
            if (_eventManager == null) return;
            UnityEvent thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
                thisEvent.RemoveListener(listener);
        }

        /// <summary>
        /// Triggers an event
        /// Takes a string which is the event name
        /// </summary>
        /// <param name="eventName"></param>
        public static void TriggerEvent(string eventName){
            UnityEvent thisEvent = null;
            if (Instance._eventDictionary.TryGetValue(eventName, out thisEvent))
                thisEvent.Invoke();
        }
    }
}