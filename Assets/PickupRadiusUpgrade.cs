using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PickupRadiusUpgrade : UpgradeChoice
{
    float modifier = 1.3f;

    public string title = "Pick up Radius";
    public string description = "+30%";

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

        player.pickupRange *= modifier;
    }
}
