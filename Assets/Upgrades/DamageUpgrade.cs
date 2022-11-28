using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DamageUpgrade : UpgradeChoice
{
    float modifier = 2f;

    public string title = "Minion Damage";
    public string description = "+20%";

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
        Weapon.damage += modifier;
    }
}
