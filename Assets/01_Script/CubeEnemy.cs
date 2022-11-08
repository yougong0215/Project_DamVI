using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : EnemyBase
{
    

    protected override void EnemyDetection()
    {

    }

    protected override void IdleEnemy()
    {
        //_rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        transform.localEulerAngles += new Vector3(0, 300, 0) * Time.deltaTime;
    }
}
