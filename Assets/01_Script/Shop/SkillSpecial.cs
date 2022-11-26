using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSpecial : ShopUpgrade
{
    public override void Upgrade()
    {
        base.Upgrade();
        if (!isPossible) return;
        ShopState.Instance.UltBool = (int)shopOption.upValue;
        ShopState.Instance.SpecialCount = shopOption.count;
        UpdateText();
    }

    public override void UpdateText()
    {
        base.UpdateText();
        if (shopOption.count == shopOption.maxCount)
        {
            CoastText.text = "MAX";
            return;
        }
        CoastText.text = $"{shopOption.coast + (shopOption.count * shopOption.upCoast)} ({shopOption.count}/{shopOption.maxCount})";
        goldText.UpdateText();
    }
}
