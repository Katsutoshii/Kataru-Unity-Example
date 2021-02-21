using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Kataru
{
    public class EventMap<T>
    {
        private Dictionary<string, UnityEvent<T>> events = new Dictionary<string, UnityEvent<T>>();

        public void Add(string name, UnityAction<T> listener)
        {
            UnityEvent<T> @event = null;
            if (events.TryGetValue(name, out @event))
            {
                @event.AddListener(listener);
            }
            else
            {
                @event = new UnityEvent<T>();
                @event.AddListener(listener);
                events.Add(name, @event);
            }
        }

        public void Add(UnityAction<T> listener) => Add(listener.Method.Name, listener);

        public void Remove(string name, UnityAction<T> listener)
        {
            UnityEvent<T> @event = null;
            if (events.TryGetValue(name, out @event))
            {
                @event.RemoveListener(listener);
            }
        }

        public void Remove(UnityAction<T> listener) => Remove(listener.Method.Name, listener);

        public void Invoke(string name, T args)
        {
            UnityEvent<T> @event = null;
            if (events.TryGetValue(name, out @event))
            {
                @event.Invoke(args);
            }
            else
            {
                Debug.LogError(String.Format("Command '{0}' had no listeners.", name));
            }
        }
    }
}