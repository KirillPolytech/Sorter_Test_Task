using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Runtime.Scripts.EventBusThings
{
    public class EventBus
    {
        private readonly Dictionary<string, List<object>> _events = new();

        public void Subscribe<T>(Action<T> callBack)
        {
            string key = typeof(T).Name;
            if (_events.ContainsKey(key))
            {
                _events[key].Add(callBack);
                return;
            }

            _events.Add(key, new List<object> {callBack});
        }

        public void UnSubscribe<T>(Action<T> callBack)
        {
            string key = typeof(T).Name;

            if (_events.ContainsKey(key))
            {
                _events[key].Remove(callBack);
                return;
            }

            Debug.LogError($"[Subscribe] Can't find key [Time: {Time.time}]");
        }

        public void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;
            if (_events.ContainsKey(key))
            {
                foreach (object obj in _events[key])
                {
                    Action<T> action = obj as Action<T>;
                    action?.Invoke(signal);
                }
                
                return;
            }

            Debug.LogError($"[Subscribe] Can't find key [Time: {Time.time}]");
        }
    }
}