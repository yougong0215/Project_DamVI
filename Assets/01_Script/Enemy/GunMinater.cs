using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMinater : EnemyBase
{
    protected override void MoveEnemy()
    {
        if(Vector3.Distance(Player.position, transform.position) >= _attackLength)
        {
            transform.rotation = Quaternion.Euler(Player.position.normalized);
            try
            {
                _nav.SetDestination(Player.position);
                _nav.stoppingDistance = _attackLength - Random.Range(0.0f, 5.0f);
            }
            catch
            {
                Debug.Log($"{gameObject.name}������Ʈ �����");
            }
            _ani.SetBool("Move", true);
        }
        else
        {
            _ani.SetBool("Move", false);
        }
        
    }

    /// <summary>
    /// ���⼭ ���� �ൿ�ϴ°� ����ɵ�
    /// </summary>
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

    protected override void IdleEnemy()
    {

    }

    protected override void AttackBase()
    {
        _ani.SetBool("Attack", true);
    }

    protected override void AttackCoolBase()
    {
        MoveEnemy();
        _ani.SetBool("Attack", false);
    }
}
