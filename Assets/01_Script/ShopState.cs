using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopState : Singleton<ShopState>
{
    /*
공격력 : 1 ~ 2 까지 총 10회 강화 가능 ( 데미지 최대 2배 )              -> float 프러퍼티

크리티컬 : 0 ~ 100 % ( 0 ~ 1 ) 까지 총 10회 강화 가능 ( 데미지 50% 증가 )     -> float 프러퍼티

체력 :  100 ~ 500 까지 강화 가능 총 10회 강화 가능             -> int 프러퍼티

추가 실드양 ( 피격 방어 ) - 0 ~ 100 까지 총 10회 가능             -> int 프러퍼티

MP? SP 초당 추가 회복량 - 0 ~ 5 까지 총 10회 가능             -> float 프러퍼티

    특수 능력
구르기 0 -> 1 -> 2 ( 총 2회 구매 가능 )                 -> int 프러퍼티

분신공격 0 -> 1                              -> bool 프러퍼티

특수 공격 0 -> 1                             -> bool 프러퍼티

줌 샷 퀵 드로우 0 -> 1                         -> bool 프러퍼티

필살기 0 -> 1                             -> bool 프러퍼티
     */

    private float S_attack = 0;
    private float S_critical = 0;
    private int S_hp = 0;
    private int S_Shield = 0;
    private float S_Mp = 0;
    private int S_Bullet = 0;

    private int whill = 0;
    private bool shoot = false;
    private bool quick = false;
    private bool speacial = false;

    public float AttackAdd
    {
        get
        {
            S_attack = Mathf.Clamp(S_attack, 0, 10);
            return S_attack;
        } 
        set => S_attack += value;
    }
    public float CriticalAdd
    {
        get
        {
            S_critical = Mathf.Clamp(S_critical, 0, 1);
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
    public int ShiedlAdd
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
            S_attack = Mathf.Clamp(S_Mp, 0, 5);
            return S_attack;
        }
        set => S_Mp += value;
    }

    public int BulletAdd
    {
        get
        {
            S_Bullet = Mathf.Clamp(S_Bullet, 6, 12);
            return S_Bullet;
        }
        set => S_Bullet += value;
    }

    public int Willadd
    {
        get
        {
            whill = Mathf.Clamp(whill, 2, 3);
            return whill;
        }
        set => whill = value;
    }
    public bool ShooGun
    {
        get => shoot;
        set => shoot = value;
    }
    public bool QuickDrowBool
    {
        get => quick;
        set => quick = value;
    }
    public bool UltBool
    {
        get => speacial;
        set => speacial = value;
    }


}
