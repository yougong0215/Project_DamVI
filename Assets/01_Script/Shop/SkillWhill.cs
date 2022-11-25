using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillWhill : ShopUpgrade
{
    public override void Upgrade()
    {
        base.Upgrade();
        if (!isPossible) return;
        ShopState.Instance.Willadd = (int)shopOption.upValue;
        UpdateText();
    }

    public override void UpdateText()
    {
        if(shopOption.count == shopOption.maxCount)
        {
            CoastText.text = "MAX";
            return;
        }
        CoastText.text = $"{shopOption.coast + (shopOption.count * shopOption.upCoast)} ({shopOption.count}/{shopOption.maxCount})";
        goldText.UpdateText();
    }
}