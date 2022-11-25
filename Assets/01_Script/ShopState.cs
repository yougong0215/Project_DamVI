using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum Stats
{
    attack,
    critical,
    hp,
    shield,
    mp,
    bullet
}
public class ShopState : Singleton<ShopState>
{
    /*
���ݷ� : 1 ~ 2 ���� �� 10ȸ ��ȭ ���� ( ������ �ִ� 2�� )              -> float ������Ƽ

ũ��Ƽ�� : 0 ~ 100 % ( 0 ~ 1 ) ���� �� 10ȸ ��ȭ ���� ( ������ 50% ���� )     -> float ������Ƽ

ü�� :  100 ~ 500 ���� ��ȭ ���� �� 10ȸ ��ȭ ����             -> int ������Ƽ

�߰� �ǵ�� ( �ǰ� ��� ) - 0 ~ 100 ���� �� 10ȸ ����             -> int ������Ƽ
    33
MP? SP �ʴ� �߰� ȸ���� - 0 ~ 5 ���� �� 10ȸ ����             -> float ������Ƽ

    Ư�� �ɷ�
������ 0 -> 1 -> 2 ( �� 2ȸ ���� ���� )                 -> int ������Ƽ

�нŰ��� 0 -> 1                              -> bool ������Ƽ

Ư�� ���� 0 -> 1                             -> bool ������Ƽ

�� �� �� ��ο� 0 -> 1                         -> bool ������Ƽ

�ʻ�� 0 -> 1                             -> bool ������Ƽ
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

    private int S_gold = 1000000;

    public int Gold
    {
        get => S_gold;
        set => S_gold += value;
    }

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
            S_attack = Mathf.Clamp(S_Mp, 1, 6);
            return S_attack;
        }
        set => S_Mp += value;
    }

    public int BulletAdd
    {
        get
        {
            S_Bullet = Mathf.Clamp(S_Bullet, 4, 13);
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
