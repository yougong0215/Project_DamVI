using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLight : MonoBehaviour
{
    [Header("õ��")]
    public Light[] lights;
    [Header("����")]
    public Material lightMat;
    [Header("����")]
    public Color color;

    private void Awake()
    {
        lights = GameObject.FindObjectsOfType<Light>();

    }

}
