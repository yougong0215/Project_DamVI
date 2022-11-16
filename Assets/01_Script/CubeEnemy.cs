using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : EnemyBase
{
    

    protected override void EnemyDetection()
    {
        transform.LookAt(Player);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        Collider[] col = Physics.OverlapBox(transform.position + new Vector3(2, 0, 0)
        , new Vector3(1, 1, 1), Quaternion.Euler(Vector3.up * (transform.localEulerAngles.y)), 1 << (LayerMask.NameToLayer("Player")));
        for(int i =0; i < col.Length; i++)
        {
            if (col[i].GetComponent<PlayerInteraction>())
            {
                col[i].GetComponent<PlayerInteraction>().Damaged(ATK);
                _AttackDelayTime = 3;
            }
        }
    }

    protected override void IdleEnemy()
    {
        //_rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        transform.localEulerAngles += new Vector3(0, 900, 0) * Time.deltaTime;
    }
}
