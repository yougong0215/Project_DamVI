using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ShopOption
{
    [HideInInspector]
    public string name;
    [Header("�з�")]
    public ShopEnum stats;

    [Header("����")][Tooltip("�⺻ ����")]
    public int coast;
    [Tooltip("���� ����")]
    public int upCoast;

    [Header("����")][Tooltip("���� ����")]
    public int count;
    [Tooltip("�ִ� ����")]
    public int maxCount;

    [Header("��ġ")][Tooltip("�ø� ��ġ")]
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
