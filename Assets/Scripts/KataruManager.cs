using UnityEngine;
using System;

/// <summary>
/// Example Kataru Manager class.
/// This class is in charge of initializing the Runner.
/// Also includes examples of reacting to Runner's events.
/// </summary>
public class KataruManager : Kataru.Handler
{
    protected override void OnChoices(Kataru.Choices choices)
    {
        Debug.Log($"Choices: {{{String.Join(", ", choices.choices)}}}");
    }

    [Kataru.CharacterHandler("Bob")]
    [Kataru.CharacterHandler("Alice")]
    [Kataru.CharacterHandler("Narrator")]
    public void OnDialogue(Kataru.Dialogue dialogue)
    {
        Debug.Log($"{dialogue.name}: {dialogue.text}");
        foreach (var item in dialogue.attributes)
        {
            string attribute = item.Key;
            Kataru.Dialogue.Span[] spans = item.Value;

            string logString = $"Attr {attribute}: {{";
            foreach (var span in spans)
            {
                logString += span.ToString() + ", ";
            }
            Debug.Log(logString + "}");
        }
    }

    [Kataru.CommandHandler]
    void ClearScreen(Kataru.Command command)
    {
        Debug.Log($"Command [{command.name}: {{{String.Join(", ", command.parameters)}}}]");
        Runner.Next("");
    }

    [Kataru.CommandHandler]
    public void Save(Kataru.Command command)
    {
        Debug.Log($"Command [{command.name}: {{{String.Join(", ", command.parameters)}}}]");
        Runner.Save();
        Runner.Next("");
    }

    [Kataru.CommandHandler]
    public void Reset(Kataru.Command command)
    {
        Debug.Log($"Command [{command.name}: {{{String.Join(", ", command.parameters)}}}]");
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
            Runner.Next("");
        }
    }
}