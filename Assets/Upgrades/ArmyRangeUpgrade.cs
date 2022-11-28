using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ArmyRangeUpgrade : UpgradeChoice
{
    float modifier = 1.5f;

    public string title = "Army Range";
    public string description = "+1.5";

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
        var player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        player.followerRange += modifier;
    }
}
