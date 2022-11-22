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

    [SerializeField] List<AudioClip> Clip = new List<AudioClip>();
    [SerializeField] List<Bullet> Type = new List<Bullet>();


    [SerializeField] AudioSource Source;

    [SerializeField]
    private VisualEffect L_muzzleEffect;
    [SerializeField][Tooltip("Default")]
    private VisualEffect R_muzzleEffect;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();
    }

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
        b.transform.position = _t.position - new Vector3(0,0.15f,0);
        Source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        Source.PlayOneShot(Clip[1]);
    }


    
}
