using UnityEngine;

/// <summary>
/// Example Kataru Text View.
/// Shows dialogue text.
/// </summary>
class KataruCharacterView : Kataru.Handler
{
    void OnValidate()
    {
        Name = gameObject.name;
    }

    [Kataru.CommandHandler(local: true)]
    void Wave(Kataru.Command command)
    {
        double amount = (double)command.parameters["amount"];
        Debug.Log($"{Name}.Wave! Amount = {amount}");
    }
}
