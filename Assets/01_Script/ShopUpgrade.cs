using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopUpgrade : MonoBehaviour
{
    [SerializeField]
    protected Shop shop;
    [SerializeField]
    protected Stats stats;
    [SerializeField]
    protected TextMeshProUGUI CountText;
    [SerializeField]
    protected TextMeshProUGUI ValueText;
    [SerializeField]
    protected TextMeshProUGUI CoastText;

    public virtual void Upgrade()
    {

    }

    public void UpdateText()
    {
        ShopOption shopOption = new ShopOption();
        for(int i = 0; i < shop.options.Count; i++)
        {
            if(stats == shop.options[i].stats)
            {
                shopOption = shop.options[i];
                break;
            }
        }
        
    }
}
