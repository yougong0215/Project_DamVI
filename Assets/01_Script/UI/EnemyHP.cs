using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class EnemyHP : MonoBehaviour
{
    private void Update()
    {
        transform.localEulerAngles -= transform.parent.localEulerAngles;
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }
}
