using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletReload : MonoBehaviour
{
    [SerializeField] int _bulletcount;
    [SerializeField] Image Bullet;
    void Start()
    {
        _bulletcount = ShopState.Instance.BulletAdd;
        for(int i =0; i< _bulletcount; i++)
        {
            Instantiate(Bullet, transform);
        }
    }
}
