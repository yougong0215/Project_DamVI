using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAttack : ShopUpgrade
{
    public override void UpgradeStat()
    {
        base.UpgradeStat();
        if (!isPossible) return;
        ShopState.Instance.AttackAdd = shopOption.upValue;
        UpdateText();
    }

    public override void UpdateText()
    {
        if (shopOption.count == shopOption.maxCount)
        {
            ValueText.text = $"+{Mathf.Round((ShopState.Instance.AttackAdd - 1f) * 10) * 0.1f}";
            CoastText.text = "MAX";
            return;
        }
        ValueText.text = $"+{Mathf.Round((ShopState.Instance.AttackAdd -1f) * 10)*0.1f} -> +{Mathf.Round((ShopState.Instance.AttackAdd + shopOption.upValue - 1)*10)*0.1f}";
        CoastText.text = $"{shopOption.coast + (shopOption.upCoast * (shopOption.count - 1))}";
        goldText.UpdateText();
    }
}
