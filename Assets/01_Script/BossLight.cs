using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLight : MonoBehaviour
{
    [Header("천장")]
    public Light[] lights;
    [Header("벽면")]
    public Material lightMat;
    [Header("색상")]
    public Color color;

    private void Awake()
    {
        lights = GameObject.FindObjectsOfType<Light>();

    }

}
