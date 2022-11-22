using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : EnemyBase
{
    protected override void EnemyDetection()
    {
        transform.LookAt(Player);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
        if(_AttackDelayTime <= 0 && Vector3.Distance(Player.position, transform.position) < _attackLength)
        {
            AttackBase();
        }
        else
        {
            AttackCoolBase();
        }
    
    }

    protected override void AttackBase()
    {
        Collider[] col = Physics.OverlapBox(transform.position + (transform.position - Player.position).normalized
        , new Vector3(1 * _attackLength, 1, 1), transform.rotation
        , 1 << (LayerMask.NameToLayer("Player"))); ;
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].GetComponent<PlayerInteraction>())
            {
                col[i].GetComponent<PlayerInteraction>().Damaged(ATK);
                _AttackDelayTime = 3;
            }
        }
        

    }

    protected override void AttackCoolBase()
    {
        MoveEnemy();
    }
    protected override void IdleEnemy()
    {
        //_rigid.AddForce(Vector3.up * 10, ForceMode.Impulse);
        transform.localEulerAngles += new Vector3(0, 900, 0) * Time.deltaTime;
    }
}
