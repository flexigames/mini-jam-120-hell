using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpeedUpgrade : UpgradeChoice
{
    float modifier = 2f;

    public string title = "Player Speed";
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
        var playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();

        playerMovement.speed += modifier;
    }
}
