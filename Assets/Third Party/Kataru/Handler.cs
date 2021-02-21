
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Kataru
{
    public class Handler : MonoBehaviour
    {
        [SerializeField] protected Kataru.Runner Runner;

        protected Dictionary<string, UnityAction<Kataru.Command>> Commands =
            new Dictionary<string, UnityAction<Kataru.Command>>();
        protected Dictionary<string, UnityAction<Kataru.Dialogue>> Characters =
            new Dictionary<string, UnityAction<Kataru.Dialogue>>();

        void OnEnable()
        {
            foreach (var pair in Commands)
            {
                Runner.Commands.Add(pair.Key, pair.Value);
            }

            foreach (var pair in Characters)
            {
                Runner.Characters.Add(pair.Key, pair.Value);
            }

            Runner.OnChoices += OnChoices;
        }

        void OnDisable()
        {
            foreach (var pair in Commands)
            {
                Runner.Commands.Remove(pair.Key, pair.Value);
            }

            foreach (var pair in Characters)
            {
                Runner.Characters.Remove(pair.Key, pair.Value);
            }

            Runner.OnChoices -= OnChoices;
        }

        protected virtual void OnChoices(Choices choices) { }
    }
}
