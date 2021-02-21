
using UnityEngine;
using System;

public class KataruManager : MonoBehaviour
{
    [SerializeField] Kataru.Runner runner;

    void Start()
    {
        runner.Init();

        runner.OnChoices -= OnChoices;
        runner.OnDialogue += OnDialogue;
        runner.OnCommand += OnCommand;
        runner.OnInputCommand += OnInputCommand;

        runner.Next("");
    }

    void OnDisable()
    {
        runner.OnChoices -= OnChoices;
        runner.OnDialogue -= OnDialogue;
        runner.OnCommand -= OnCommand;
        runner.OnInputCommand -= OnInputCommand;
    }

    void Update()
    {
        //Detect when the Return key is pressed down
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log("Next!");
            runner.Next("");
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

    void OnCommand(Kataru.Command command)
    {
        Debug.Log(String.Format("Command [{0}: {{1}}]",
            command.name,
            String.Join(", ", command.parameters)));
        if (command.name == "save")
        {
            runner.Save();
        }
        else if (command.name == "reset")
        {
            runner.GotoPassage("End");
            runner.SetLine(0);
        }

        runner.Next("");
    }

    void OnChoices(Kataru.Choices choices)
    {
        foreach (string choice in choices.choices)
        {
            Debug.Log(choice);
        }
    }

    void OnInputCommand(Kataru.InputCommand inputCommand)
    {
        Debug.Log(inputCommand.prompt);
    }
}