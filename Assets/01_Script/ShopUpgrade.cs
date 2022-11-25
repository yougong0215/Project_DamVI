using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ShopUpgrade : MonoBehaviour
{
    [SerializeField]
    protected Shop shop;
    [SerializeField]
    protected GoldText goldText;
    [SerializeField]
    protected ShopEnum stats;
    [SerializeField]
    protected TextMeshProUGUI CountText;
    [SerializeField]
    protected TextMeshProUGUI ValueText;
    [SerializeField]
    protected TextMeshProUGUI CoastText;
    protected bool isPossible;

    [SerializeField]
    protected ShopOption shopOption;
    public virtual void Upgrade()
    {
        if (shopOption.count < shopOption.maxCount)
        {
            if (ShopState.Instance.Gold - (shopOption.coast + (shopOption.upCoast * (shopOption.count - 1 < 0 ? 0 : shopOption.count - 1))) >= 0)
            {
                ShopState.Instance.Gold = -(shopOption.coast+(shopOption.upCoast * (shopOption.count - 1 < 0 ? 0 : shopOption.count - 1)));
                shopOption.count++;
                goldText.UpdateText();
                isPossible = true;
            }
            else
            {
                Debug.Log("재화부족");
                isPossible = false;
            }
        }
        else
        {
            isPossible = false;
            Debug.Log("MAX");
        }


    }

    private void OnEnable()
    {
        if(shop == null)
        {
            Debug.LogError("shop 프로퍼티 없음");
            return;
        }
        for (int i = 0; i < shop.options.Count; i++)
        {
            if (stats == shop.options[i].stats)
            {
                shopOption = shop.options[i];
                break;
            }
        }

        if(shop == null)
        {
            shop = GameObject.FindObjectOfType<Shop>();
        }

        if(goldText == null)
        {
            goldText = GameObject.FindObjectOfType<GoldText>();
        }

        UpdateText();
    }
    public virtual void UpdateText()
    {
        
        
    }
}
