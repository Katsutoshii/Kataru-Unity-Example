using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Example Choices view.
/// Displays dialogue choices as buttons and enters selected into into the Runner.
/// </summary>
class KataruChoicesView : Kataru.Handler
{
    [SerializeField] RectTransform optionContainer = null;
    [SerializeField] GameObject optionButtonTemplate = null;

    private List<GameObject> optionButtons = new List<GameObject>();

    protected override void OnChoices(Kataru.Choices choices)
    {
        optionButtons.Clear();
        foreach (string choice in choices.choices)
        {

            var newOption = Instantiate(optionButtonTemplate);
            newOption.SetActive(true);
            newOption.transform.SetParent(optionContainer, false);

            var button = newOption.GetComponentInChildren<Button>();
            button.onClick.AddListener(() => OnChoice(choice));

            var text = newOption.GetComponentInChildren<Text>();
            text.text = choice;

            text.color = Color.white;

            optionButtons.Add(newOption);
        }
    }

    public void OnChoice(string choice)
    {
        Debug.Log("OptionButtonSelected '" + choice + "'");
        foreach (var button in optionButtons)
        {
            Destroy(button);
        }

        optionButtons.Clear();
        Runner.Next(choice);
    }
}