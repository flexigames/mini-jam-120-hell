using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Choice : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TextMeshProUGUI description;

    public void SetUpgradeChoice(UpgradeChoice upgradeChoice)
    {
        title.text = upgradeChoice.GetTitle();
        description.text = upgradeChoice.GetDescription();
    }
}
