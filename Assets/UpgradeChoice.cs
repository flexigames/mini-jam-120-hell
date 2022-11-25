using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface UpgradeChoice
{
    public void Apply();

    public string GetTitle();
    public string GetDescription();
}
