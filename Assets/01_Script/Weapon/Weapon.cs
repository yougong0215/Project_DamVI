using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.VFX;


public enum BulletType
{
    Bulletbase = 0,
    RedBullet =1
}

public class Weapon : MonoBehaviour
{

    [SerializeField] List<Bullet> Type = new List<Bullet>();


    [SerializeField]
    private VisualEffect L_muzzleEffect;
    [SerializeField][Tooltip("Default")]
    private VisualEffect R_muzzleEffect;



    public void fire(WeaponType type, BulletType bullet)
    {
        switch (type)
        {
            case WeaponType.Left:
                L_muzzleEffect.Play();
                Shoot(0, L_muzzleEffect.transform, (int)bullet);
                break;
            case WeaponType.Right:
                R_muzzleEffect.Play();
                Shoot(0, R_muzzleEffect.transform, (int)bullet);
                break;
        }
        
    }

    void Shoot(int i, Transform _t, int bul)
    {
        PoolAble b = PoolManager.Instance.Pop(Type[bul].gameObject.name);
        b.transform.position = _t.position;
    }


    
}
