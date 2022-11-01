using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.VFX;

public class Weapon : MonoBehaviour
{
  



    [SerializeField]
    private VisualEffect L_muzzleEffect;
    [SerializeField][Tooltip("Default")]
    private VisualEffect R_muzzleEffect;
    // Start is called before the first frame update
    void Awake()
    {

    }

    public void fire(WeaponType type)
    {
        switch (type)
        {
            case WeaponType.Left:
                L_muzzleEffect.Play();
                break;
            case WeaponType.Right:
                R_muzzleEffect.Play();
                break;
        }
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
