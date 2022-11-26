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

    private void OnEnable()
    {
        for (int i = 0; i < options.Count; i++)
        {
            switch (options[i].stats)
            {
                case ShopEnum.attack:
                    options[i].count = ShopState.Instance.AttackCount;
                    break;
                case ShopEnum.critical:
                    options[i].count = ShopState.Instance.CriticalCount;
                    break;
                case ShopEnum.hp:
                    options[i].count = ShopState.Instance.HPCount;
                    break;
                case ShopEnum.shield:
                    options[i].count = ShopState.Instance.ShieldCount;
                    break;
                case ShopEnum.mp:
                    options[i].count = ShopState.Instance.MPCount;
                    break;
                case ShopEnum.bullet:
                    options[i].count = ShopState.Instance.BulletCount;
                    break;
                case ShopEnum.whill:
                    options[i].count = ShopState.Instance.WhillCount;
                    break;
                case ShopEnum.shoot:
                    options[i].count = ShopState.Instance.ShootCount;
                    break;
                case ShopEnum.quick:
                    options[i].count = ShopState.Instance.QuickCount;
                    break;
                case ShopEnum.special:
                    options[i].count = ShopState.Instance.SpecialCount;
                    break;
                default:
                    continue;
            }
        }
    }
}

