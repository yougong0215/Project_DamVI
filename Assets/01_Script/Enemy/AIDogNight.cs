using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDogNight : EnemyBase, IEnemyDetection
{
    [SerializeField] Collider[] col;
    [SerializeField]
    LayerMask layer;
    public bool _rotate = false;
    protected override void MoveEnemy()
    {
        if (Vector3.Distance(Player.position, transform.position) >= _attackLength)
        {
            if (_ani.GetBool("Die") == false) 
            {
                try
                {
                    _nav.SetDestination(Player.position);
                    _nav.stoppingDistance = _attackLength;
                }
                catch
                {

                }
            }
            _ani.SetBool("Move", true);
        }
        else
        {
            _ani.SetBool("Move", false);
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + (Player.position - transform.position).normalized, new Vector3(1.2f, 1.2f, 1.2f));
    }
    protected override void EnemyDetection()
    {
        if(_rotate == true)
        {
            rot();
        }


        if (_AttackDelayTime <= 0 && Vector3.Distance(Player.position, transform.position) < _attackLength)
        {
            AttackBase();
        }
        else
        {
            AttackCoolBase();

        }
    }

    public void AttackBase()
    {
        _ani.SetBool("Attack", true);
        StartCoroutine(FalseAttack());
    }



    void rot()
    {
        transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }

    IEnumerator FalseAttack()
    {
        yield return new WaitForSeconds(0.2f);
        _ani.SetBool("Attack", false);
    }


    public void AttackCoolBase()
    {
        transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        MoveEnemy();
    }

}   
    