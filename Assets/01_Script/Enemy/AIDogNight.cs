using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDogNight : EnemyBase, IEnemyDetection
{
    [SerializeField] Collider[] col;
    [SerializeField]
    LayerMask layer;

    protected override void MoveEnemy()
    {
        if (Vector3.Distance(Player.position, transform.position) >= _attackLength)
        {
            transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
            try
            {
                _nav.SetDestination(Player.position);
                _nav.stoppingDistance = _attackLength;
            }
            catch
            {
                Debug.Log($"{gameObject.name}ø¿∫Í¿Ë∆Æ ªÁ∂Û¡¸");
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
        transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        col = Physics.OverlapBox(transform.position + (Player.position - transform.position).normalized
        , (Player.position - transform.position).normalized + new Vector3(1.2f,1.2f,1.2f), transform.rotation
        , layer);
        _ani.SetBool("Attack", true);


        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].GetComponent<PlayerInteraction>())
            {
                col[i].GetComponent<PlayerInteraction>().Damaged(ATK);
                _AttackDelayTime = 3;
            }
        }
    }

    public void AttackCoolBase()
    {
        transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        MoveEnemy();
        _ani.SetBool("Attack", false);
    }

}   
    