
using UnityEngine;
using System;
using Kataru;

public class KataruManager : Handler
{
    protected override ActionMap<Command> Commands
    {
        get => new ActionMap<Command> { ClearScreen, Reset, Save };
    }

    protected override ActionMap<Dialogue> Characters
    {
        get => new ActionMap<Dialogue>
        {
            ["Narrator"] = OnDialogue,
            ["Alice"] = OnDialogue,
            ["Bob"] = OnDialogue
        };
    }

    protected override void OnChoices(Choices choices)
    {
        foreach (string choice in choices.choices)
        {
            Debug.Log(choice);
        }
    }

    void OnDialogue(Dialogue dialogue)
    {
        Debug.Log(String.Format("{0}: {1}", dialogue.name, dialogue.text));
        foreach (var item in dialogue.attributes)
        {
            string attribute = item.Key;
            Dialogue.Span[] spans = item.Value;

            string logString = String.Format("Attr {0}:", attribute);
            foreach (var span in spans)
            {
                logString += span.ToString() + ", ";
            }
            Debug.Log(logString);
        }
    }

    void ClearScreen(Command command)
    {
        Debug.Log(String.Format("Command [{0}: {1}]",
            command.name,
            String.Join(", ", command.parameters)));
        Runner.Next("");
    }

    void Save(Command command)
    {
        Debug.Log(String.Format("Command [{0}: {1}]",
            command.name,
            String.Join(", ", command.parameters)));
        Runner.Save();
        Runner.Next("");
    }

    void Reset(Command command)
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