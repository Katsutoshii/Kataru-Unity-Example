using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Example Kataru Text View.
/// Shows dialogue text.
/// </summary>
class KataruTextView : Kataru.Handler
{
    [SerializeField] Text text = null;

    void OnValidate()
    {
        text = GetComponent<Text>();
    }

    [Kataru.CharacterHandler("Narrator")]
    void OnNarrator(Kataru.Dialogue dialogue)
    {
        text.fontStyle = FontStyle.Italic;
        text.text = dialogue.text;
    }

    [Kataru.CharacterHandler("Alice")]
    [Kataru.CharacterHandler("Bob")]
    void OnDialogue(Kataru.Dialogue dialogue)
    {
        text.fontStyle = FontStyle.Normal;
        text.text = $"{dialogue.name}: {dialogue.text}";
    }

    [Kataru.CommandHandler]
    void ClearScreen(Kataru.Command command)
    {
        text.text = "";
    }
}
