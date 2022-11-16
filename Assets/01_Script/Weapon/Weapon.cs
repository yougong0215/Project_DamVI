using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{

    [SerializeField] List<Bullet> Type = new List<Bullet>();


    [SerializeField]
    private VisualEffect L_muzzleEffect;
    [SerializeField][Tooltip("Default")]
    private VisualEffect R_muzzleEffect;



    public void fire(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Left:
                L_muzzleEffect.Play();
                Shoot(0, L_muzzleEffect.transform);
                break;
            case WeaponType.Right:
                R_muzzleEffect.Play();
                Shoot(0, R_muzzleEffect.transform);
                break;
        }
        
    }

    void Shoot(int i, Transform _t)
    {
        PoolAble b = PoolManager.Instance.Pop(Type[i].gameObject.name);
        b.transform.position = _t.position;
    }


    
}
