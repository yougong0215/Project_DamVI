using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyHP : MonoBehaviour
{

    Transform a;
    private void Start()
    {
        a = transform.parent;
    }
    private void Update()
    {
        transform.position = a.position + new Vector3(0,1.6f,0);
    }
}
