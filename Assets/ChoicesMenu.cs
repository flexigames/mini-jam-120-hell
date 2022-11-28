using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ChoicesMenu : MonoBehaviour
{
    public GameObject[] choices;

    public Color defaultColor;
    public Color selectedColor;

    int currentChoiceIndex = 0;

    UpgradeChoice[] upgradeChoices = new UpgradeChoice[]
    {
        new ArmyRangeUpgrade(),
        new PickupRadiusUpgrade(),
        new SpeedUpgrade(),
        new HealthUpgrade(),
        new DamageUpgrade(),
        new HealUpgrade(),
        new SpawnMinionUpgrade(),
    };

    UpgradeChoice[] currentChoices = new UpgradeChoice[3];

    void Start()
    {
        UpdateSelectionIndicator();
        SetRandomUpgradeChoices();
    }

    void SetRandomUpgradeChoices()
    {
        var random = new System.Random();

        currentChoices = new List<UpgradeChoice>(upgradeChoices)
            .OrderBy(x => random.Next())
            .Take(3)
            .ToArray();

        for (int i = 0; i < choices.Length; i++)
        {
            Choice choice = choices[i].GetComponent<Choice>();
            choice.SetUpgradeChoice(currentChoices[i]);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Sounds.Play("Click");
            currentChoiceIndex++;
            if (currentChoiceIndex >= choices.Length)
            {
                currentChoiceIndex = 0;
            }
            UpdateSelectionIndicator();
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Sounds.Play("Click");
            currentChoiceIndex--;
            if (currentChoiceIndex < 0)
            {
                currentChoiceIndex = choices.Length - 1;
            }
            UpdateSelectionIndicator();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnResume();
        }
    }

    void OnResume()
    {
        Game.isPaused = false;
        Time.timeScale = 1f;
        currentChoices[currentChoiceIndex].Apply();
        SetRandomUpgradeChoices();
        Sounds.Play("Powerup");

        gameObject.SetActive(false);
    }

    void UpdateSelectionIndicator()
    {
        for (int i = 0; i < choices.Length; i++)
        {
            var image = choices[i];
            if (i == currentChoiceIndex)
            {
                image.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            }
            else
            {
                image.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }
}
