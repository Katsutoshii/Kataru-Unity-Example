
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class KataruManager : Kataru.Handler
{
    void Awake()
    {
        Commands = new Dictionary<string, UnityAction<Kataru.Command>>()
        {
            ["ClearScreen"] = ClearScreen,
            ["Reset"] = Reset,
            ["Save"] = Save
        };

        Characters = new Dictionary<string, UnityAction<Kataru.Dialogue>>()
        {
            { "Narrator", OnDialogue },
            { "Alice", OnDialogue },
            { "Bob", OnDialogue }
        };
    }

    protected override void OnChoices(Kataru.Choices choices)
    {
        foreach (string choice in choices.choices)
        {
            Debug.Log(choice);
        }
    }

    void OnDialogue(Kataru.Dialogue dialogue)
    {
        Debug.Log(String.Format("{0}: {1}", dialogue.name, dialogue.text));
        foreach (var item in dialogue.attributes)
        {
            string attribute = item.Key;
            Kataru.Dialogue.Span[] spans = item.Value;

            string logString = String.Format("Attr {0}:", attribute);
            foreach (var span in spans)
            {
                logString += span.ToString() + ", ";
            }
            Debug.Log(logString);
        }
    }

    void ClearScreen(Kataru.Command command)
    {
        Debug.Log(String.Format("Command [{0}: {1}]",
            command.name,
            String.Join(", ", command.parameters)));
        Runner.Next("");
    }

    void Save(Kataru.Command command)
    {
        Debug.Log(String.Format("Command [{0}: {1}]",
            command.name,
            String.Join(", ", command.parameters)));
        Runner.Save();
        Runner.Next("");
    }

    void Reset(Kataru.Command command)
    {
        Debug.Log(String.Format("Command [{0}: {1}]",
            command.name,
            String.Join(", ", command.parameters)));
        Runner.GotoPassage("End");
        Runner.Next("");
    }

    void Start()
    {
        Runner.Init();

        Runner.Next("");
    }

    void Update()
    {
        //Detect when the Return key is pressed down
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Next!");
            Runner.Next("");
        }
    }
}