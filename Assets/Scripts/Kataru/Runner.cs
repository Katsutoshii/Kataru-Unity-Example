using UnityEngine;
using System;

namespace Kataru
{

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
            FFI.Validate();
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

        public void SetLine(int line) => FFI.SetLine(line);
        public void GotoPassage(string passage) => FFI.GotoPassage(passage);
        public void SetState(string key, string value) => FFI.SetState(key, value);
        public void SetState(string key, double value) => FFI.SetState(key, value);
        public void SetState(string key, bool value) => FFI.SetState(key, value);

        public void Next(string input)
        {
            Debug.Log("Calling next with input '" + input + "'");
            FFI.Next(input);
            switch (FFI.Tag())
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