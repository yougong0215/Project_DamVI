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
                Shoot(0);
                break;
            case WeaponType.Right:
                R_muzzleEffect.Play();
                Shoot(0);
                break;
        }
        
    }

    void Shoot(int i)
    {
        PoolAble b = PoolManager.Instance.Pop(Type[i].gameObject.name);
        b.transform.position = transform.position;
        b.transform.position += new Vector3(0, 1, 0);
    }


    
}
