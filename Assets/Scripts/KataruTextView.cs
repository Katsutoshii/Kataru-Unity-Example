using UnityEngine;
using UnityEngine.Events;
using System;
using UnityEngine.UI;
using System.Collections.Generic;

class KataruTextView : Kataru.Handler
{
    [SerializeField] Text text = null;


    void Awake()
    {
        Commands = new Dictionary<string, UnityAction<Kataru.Command>>()
        {
            { "ClearScreen", ClearScreen },
        };

        Characters = new Dictionary<string, UnityAction<Kataru.Dialogue>>()
        {
            { "Narrator", OnNarrator },
            { "Alice", OnDialogue },
            { "Bob", OnDialogue }
        };
    }

    void OnValidate()
    {
        text = GetComponent<Text>();
    }

    void OnNarrator(Kataru.Dialogue dialogue)
    {
        text.fontStyle = FontStyle.Italic;
        text.text = dialogue.text;
    }

    void OnDialogue(Kataru.Dialogue dialogue)
    {
        text.fontStyle = FontStyle.Normal;
        text.text = String.Format("{0}: {1}", dialogue.name, dialogue.text);
    }

    void ClearScreen(Kataru.Command command)
    {
        Debug.Log("Clear text view screen!");
        text.text = "";
    }
}