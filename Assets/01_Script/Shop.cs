using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopOption
{
    [HideInInspector]
    public string name;
    [Header("분류")]
    public ShopEnum stats;

    [Header("가격")][Tooltip("기본 가격")]
    public int coast;
    [Tooltip("오를 가격")]
    public int upCoast;

    [Header("레벨")][Tooltip("현재 레벨")]
    public int count;
    [Tooltip("최대 레벨")]
    public int maxCount;

    [Header("수치")][Tooltip("올릴 수치")]
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
