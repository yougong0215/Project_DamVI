using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopOption
{
    
    public Stats stats;
    public int coast;
    public int upCoast;
    public int count;
    public int maxCount;
    public float upValue;
    
}
public class Shop : MonoBehaviour
{
    public List<ShopOption> options;
    public void AttackUpgrade()
    {

    }

    public void CriticalUpgrade()
    {

    }
}
