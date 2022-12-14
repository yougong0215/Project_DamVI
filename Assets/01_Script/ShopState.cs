using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ShopEnum
{
    attack,
    critical,
    hp,
    shield,
    mp,
    bullet,
    whill,
    shoot,
    quick,
    special
}

public class ShopState : Singleton<ShopState>
{
    /*
    공격력 : 1 ~ 2 까지 총 10회 강화 가능 ( 데미지 최대 2배 )                     -> float 프러퍼티

    크리티컬 : 0 ~ 100 % ( 0 ~ 1 ) 까지 총 10회 강화 가능 ( 데미지 50% 증가 )     -> float 프러퍼티

    체력 :  100 ~ 500 까지 강화 가능 총 10회 강화 가능                            -> int 프러퍼티

    추가 실드양 ( 피격 방어 ) - 0 ~ 100 까지 총 10회 가능                         -> int 프러퍼티
    33
    MP? SP 초당 추가 회복량 - 0 ~ 5 까지 총 10회 가능                             -> float 프러퍼티

    특수 능력
    구르기 0 -> 1 -> 2 ( 총 2회 구매 가능 )                                      -> int 프러퍼티

    분신공격 0 -> 1                              -> bool 프러퍼티

    특수 공격 0 -> 1                             -> bool 프러퍼티

    줌 샷 퀵 드로우 0 -> 1                         -> bool 프러퍼티

    필살기 0 -> 1                             -> bool 프러퍼티
     */

    private void Awake()
    {
        SettingSaveLoad.Instance.LoadShop();
    }   

    private float S_attack = 0;
    private float S_critical = 0;
    private int S_hp = 0;
    private int S_Shield = 0;
    private float S_Mp = 0;
    private int S_Bullet = 0;

    private int whill = 0;
    private int shoot = 0;
    private int quick = 0;
    private int special = 0;

    private int S_gold = 0;

    public int Gold
    {
        get => S_gold;
        set => S_gold += value;
    }

    #region 업그레이드 프로퍼티
    public float AttackAdd
    {
        get
        {
            S_attack = Mathf.Clamp(S_attack, 1, 10f);
            return S_attack;
        } 
        set => S_attack += value;
    }
    public float CriticalAdd
    {
        get
        {
            S_critical = Mathf.Clamp(S_critical, 0.0f, 1.0f);
            return S_critical;
        }
        set => S_critical += value;
    }
    public int HPAdd
    {
        get
        {
            S_hp = Mathf.Clamp(S_hp, 100, 500);
            return S_hp;
        }
        set => S_hp += value;
    }
    public int ShieldAdd
    {
        get
        {
            S_Shield = Mathf.Clamp(S_Shield, 10, 100);
            return S_Shield;
        }
        set => S_Shield += value;
    }
    public float MPAdd
    {
        get
        {
            S_Mp = Mathf.Clamp(S_Mp, 1, 5);
            return S_Mp;
        }
        set => S_Mp += value;
    }

    public int BulletAdd
    {
        get
        {
            S_Bullet = Mathf.Clamp(S_Bullet, 4, 12);
            return S_Bullet;
        }
        set => S_Bullet += value;
    }

    public int Willadd
    {
        get
        {
            whill = Mathf.Clamp(whill, 0, 3);
            return whill;
        }
        set => whill += value;
    }
    public int ShooGun
    {
        get
        {
            shoot = Mathf.Clamp(shoot, 0, 2);
            return shoot;
        }
        set => shoot = value;
    }
    public int QuickDrowBool
    {
        get
        {
            quick = Mathf.Clamp(quick, 0, 2);
            return quick;
        }
        set => quick = value;
    }
    public int UltBool
    {
        get
        {
            special = Mathf.Clamp(special, 0, 2);
            return special;
        }
        set => special = value;
    }
    #endregion
    #region 레벨 프로퍼티
    private int attackCount = 0;
    private int criticalCount = 0;
    private int hpCount = 0;
    private int shieldCount = 0;
    private int mpCount = 0;
    private int bulletCount = 0;

    private int whillCount = 0;
    private int shootCount = 0;
    private int quickCount = 0;
    private int specialCount = 0;

    public int AttackCount
    {
        get => attackCount;
        set => attackCount = value;
    }
    public int CriticalCount
    {
        get => criticalCount;
        set => criticalCount = value;
    }
    public int HPCount
    {
        get => hpCount;
        set => hpCount = value;
    }
    public int ShieldCount
    {
        get => shieldCount;
        set => shieldCount = value;
    }
    public int MPCount
    {
        get => mpCount;
        set => mpCount = value;
    }
    public int BulletCount
    {
        get => bulletCount;
        set => bulletCount = value;
    }

    public int WhillCount
    {
        get => whillCount;
        set => whillCount = value;
    }
    public int ShootCount
    {
        get => shootCount;
        set => shootCount = value;
    }
    public int QuickCount
    {
        get => quickCount;
        set => quickCount = value;
    }
    public int SpecialCount
    {
        get => specialCount;
        set => specialCount = value;
    }
    #endregion
    private void Update()
    {

    }
}
public class ShopStateForSave
{
    public float S_attack = 0;
    public float S_critical = 0;
    public int S_hp = 0;
    public int S_Shield = 0;
    public float S_Mp = 0;
    public int S_Bullet = 0;

    public int whill = 0;
    public int shoot = 0;
    public int quick = 0;
    public int special = 0;

    public int S_gold = 0;

    public int attackCount = 0;
    public int criticalCount = 0;
    public int hpCount = 0;
    public int shieldCount = 0;
    public int mpCount = 0;
    public int bulletCount = 0;

    public int whillCount = 0;
    public int shootCount = 0;
    public int quickCount = 0;
    public int specialCount = 0;

}
