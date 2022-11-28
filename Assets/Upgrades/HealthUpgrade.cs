using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HealthUpgrade : UpgradeChoice
{
    float modifier = 2f;

    public string title = "Player Max HP";
    public string description = "+2";

    public string GetTitle()
    {
        return title;
    }

    public string GetDescription()
    {
        return description;
    }

    public void Apply()
    {
        var playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();

        playerHealth.maxHealth += modifier;
    }
}
