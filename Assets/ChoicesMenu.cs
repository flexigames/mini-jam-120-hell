using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesMenu : MonoBehaviour
{
    public GameObject[] choices;

    public Color defaultColor;
    public Color selectedColor;

    int currentChoiceIndex = 0;

    UpgradeChoice[] upgradeChoices = new UpgradeChoice[3]
    {
        new ArmyRangeUpgrade(),
        new PickupRadiusUpgrade(),
        new SpeedUpgrade(),
    };

    void Start()
    {
        UpdateSelectionIndicator();

        for (int i = 0; i < choices.Length; i++)
        {
            Choice choice = choices[i].GetComponent<Choice>();
            choice.SetUpgradeChoice(upgradeChoices[i]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentChoiceIndex++;
            if (currentChoiceIndex >= choices.Length)
            {
                currentChoiceIndex = 0;
            }
            UpdateSelectionIndicator();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentChoiceIndex--;
            if (currentChoiceIndex < 0)
            {
                currentChoiceIndex = choices.Length - 1;
            }
            UpdateSelectionIndicator();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Game.isPaused = false;
            Time.timeScale = 1f;
            upgradeChoices[currentChoiceIndex].Apply();
            gameObject.SetActive(false);
        }
    }

    void UpdateSelectionIndicator()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            var image = choices[i].GetComponent<UnityEngine.UI.Image>();
            if (i == currentChoiceIndex)
            {
                image.color = selectedColor;
            }
            else
            {
                image.color = defaultColor;
            }
        }
    }
}
