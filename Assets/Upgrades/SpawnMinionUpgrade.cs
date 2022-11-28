using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpawnMinionUpgrade : UpgradeChoice
{
    public string title = "Spawn Minion";
    public string description = "+1 Army";

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

        player.SpawnFollower();
    }
}
