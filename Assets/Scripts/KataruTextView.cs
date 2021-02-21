using UnityEngine;
using System;
using UnityEngine.UI;
using Kataru;

class KataruTextView : Handler
{
    [SerializeField] Text text = null;

    protected override ActionMap<Command> Commands
    {
        get => new ActionMap<Command> { ClearScreen };
    }

    protected override ActionMap<Dialogue> Characters
    {
        get => new ActionMap<Dialogue>()
        {
            ["Narrator"] = OnNarrator,
            ["Alice"] = OnDialogue,
            ["Bob"] = OnDialogue
        };
    }

    void OnValidate()
    {
        text = GetComponent<Text>();
    }

    void OnNarrator(Dialogue dialogue)
    {
        text.fontStyle = FontStyle.Italic;
        text.text = dialogue.text;
    }

    void OnDialogue(Dialogue dialogue)
    {
        text.fontStyle = FontStyle.Normal;
        text.text = String.Format("{0}: {1}", dialogue.name, dialogue.text);
    }

    void ClearScreen(Command command)
    {
        Debug.Log("Clear text view screen!");
        text.text = "";
    }
}