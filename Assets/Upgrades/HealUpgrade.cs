using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class HealUpgrade : UpgradeChoice
{
    public string title = "Full Heal";
    public string description = "Heal to full HP";

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

        playerHealth.Reset();
    }
}
