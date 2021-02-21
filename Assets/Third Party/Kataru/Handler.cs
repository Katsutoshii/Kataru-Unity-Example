
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

namespace Kataru
{
    public class Handler : MonoBehaviour
    {
        [SerializeField] protected Runner Runner;

        protected virtual ActionMap<Command> Commands { get => new ActionMap<Command>(); }
        protected virtual ActionMap<Dialogue> Characters { get => new ActionMap<Dialogue>(); }

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
