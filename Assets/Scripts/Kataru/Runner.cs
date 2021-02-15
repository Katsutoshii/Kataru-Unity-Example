﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Text;

namespace Kataru
{
    public struct Dialogue
    {
        public string name;
        public string text;
    }

    public struct Command
    {
        public string name;
        public IDictionary<string, object> parameters;
    }

    public struct Choices
    {
        public IList<string> choices;
        public double timeout;
    }

    public struct InputCommand
    {
        public string prompt;
    }

    [CreateAssetMenu(fileName = "KataruRunner", menuName = "ScriptableObjects/KataruRunner", order = 1)]
    public class Runner : ScriptableObject
    {
        [SerializeField] public string bookmarkPath = "Kataru/Bookmark.yml";
        [SerializeField] public string savePath = "Bookmark.bin";
        [SerializeField] public string storyPath = "Kataru/Story";

        public string BookmarkPath { get => Application.dataPath + "/" + bookmarkPath; }
        public string SavePath { get => Application.persistentDataPath + "/" + savePath; }
        public string StoryPath { get => Application.dataPath + "/" + storyPath; }

        // Events to listen to.
        public event Action<Dialogue> OnDialogue;
        public event Action<Command> OnCommand;
        public event Action<Choices> OnChoices;
        public event Action OnInvalidChoice;
        public event Action<InputCommand> OnInputCommand;

        public void Init()
        {
            Debug.Log("Initializing Kataru, " + BookmarkPath);
            FFI.LoadStory(StoryPath);
            FFI.LoadBookmark(BookmarkPath);
            FFI.InitRunner();
        }

        public void Save()
        {
            Debug.Log(SavePath);
            FFI.SaveBookmark(SavePath);
        }

        public void Load()
        {
            FFI.LoadBookmark(SavePath);
            FFI.InitRunner();
        }

        public void Next(string input)
        {
            Debug.Log("Calling next with input '" + input + "'");
            LineTag tag = FFI.Next(input);
            Debug.Log(String.Format("Tag: {0}", tag));
            switch (tag)
            {
                case LineTag.Choices:
                    OnChoices.Invoke(FFI.LoadChoices());
                    break;

                case LineTag.InvalidChoice:
                    OnInvalidChoice.Invoke();
                    break;

                case LineTag.Dialogue:
                    OnDialogue.Invoke(FFI.LoadDialogue());
                    break;

                case LineTag.Commands:
                    foreach (Command command in FFI.LoadCommands())
                    {
                        OnCommand.Invoke(command);
                    }
                    break;

                case LineTag.InputCommand:
                    OnInputCommand.Invoke(FFI.LoadInputCommand());
                    break;

                case LineTag.None:
                    break;
            }
        }
    }
}