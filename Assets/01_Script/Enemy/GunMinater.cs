using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunMinater : EnemyBase, IEnemyDetection
{
    [SerializeField] public GameObject Bullet = null;
    [SerializeField] public Transform _pos;
    protected override void MoveEnemy()
    {
        if(Vector3.Distance(Player.position, transform.position) >= _attackLength)
        {
            transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
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
        if (_AttackDelayTime <= 0 && Vector3.Distance(Player.position, transform.position) - 5 < _attackLength)
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

    public void AttackBase()
    {
        transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        _ani.SetBool("Attack", true);

        _nav.stoppingDistance = 100;

        transform.rotation = Quaternion.LookRotation((Player.position + new Vector3(0,1f,0) - transform.position).normalized);
        _AttackDelayTime = 5f;
        _rigid.velocity = Vector3.zero;
    }

    public void AttackCoolBase()
    {
        transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        MoveEnemy();
        _ani.SetBool("Attack", false);
    }


    public void Att(float angle = 0)
    {
        PoolAble a = PoolManager.Instance.Pop(Bullet.name);
        a.transform.position = _pos.position;
        a.transform.rotation = Quaternion.LookRotation(Player.position);
        a.transform.rotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, angle, 0));
        a.GetComponent<EnemyBullet>().SetDamage(ATK, transform);
    }
}
