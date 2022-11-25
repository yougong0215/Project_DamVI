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
    RedBullet =1,
    ActivityBullet = 2
}

public class Weapon : MonoBehaviour
{

    [SerializeField] List<AudioClip> Clip = new List<AudioClip>();
    [SerializeField] List<Bullet> Type = new List<Bullet>();


    [SerializeField] AudioSource Source;

    bool shoot =false;
    float x = 0, y = 0;

    [SerializeField]
    private VisualEffect L_muzzleEffect;
    [SerializeField][Tooltip("Default")]
    private VisualEffect R_muzzleEffect;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();
    }

    public void fire(WeaponType type, BulletType bullet, float angleX = 0, float angleY = 0, bool f = false)
    {
        x = angleX;
        y = angleY;
        shoot = f;

            //L_muzzleEffect.transform.localScale = new Vector3(1, 1, 1);
            //R_muzzleEffect.transform.localScale = new Vector3(1, 1, 1);
        

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

    public bool Shoot()
    {
        return shoot;
    }
    public Quaternion Dir(Vector3 Euler)
    {
        return Quaternion.Euler(Euler + new Vector3(x, y, 0));
    }

    void Shoot(int i, Transform _t, int bul)
    {
        
        PoolAble b = PoolManager.Instance.Pop(Type[bul].gameObject.name);
        b.transform.position = _t.position - new Vector3(0,0.15f,0);
        
        Source.pitch = UnityEngine.Random.Range(0.8f, 1.2f);
        Source.PlayOneShot(Clip[1]);
    }


    
}
