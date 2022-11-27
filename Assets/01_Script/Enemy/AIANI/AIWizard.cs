using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWizard : EnemyBase, IEnemyDetection
{
    [SerializeField] public GameObject Bullet = null;
    [SerializeField] public Transform _pos;

    public bool _rotate = false;

    protected override void MoveEnemy()
    {
        if(Vector3.Distance(Player.position, transform.position) >= _attackLength)
        {

            if (_ani.GetBool("Die") == false)
            {
                _nav.SetDestination(Player.position);
                _nav.stoppingDistance = _attackLength - Random.Range(0.0f, 5.0f);
            }
            _ani.SetBool("Move", true);
        }
        else
        {
            _ani.SetBool("Move", false);
        }
        
    }

    void rot()
    {
        transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        transform.localEulerAngles = new Vector3(0, transform.localEulerAngles.y, 0);
    }



    /// <summary>
    /// 여기서 적이 행동하는거 쓰면될듯
    /// </summary>
    protected override void EnemyDetection()
    {

        if(_rotate == true)
        {
            rot();
        }

        if (_AttackDelayTime <= 0 && Vector3.Distance(Player.position, transform.position) - 5 < _attackLength)
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
        //transform.rotation = Quaternion.LookRotation(Player.position - transform.position);
        _ani.SetBool("Attack", true);
        if (_ani.GetBool("Die") == false)
        {
            _nav.stoppingDistance = 100;

        }
        
        _AttackDelayTime = 2f;
    }

    public void AttackCoolBase()
    {
        MoveEnemy();
    }


    public void Att(float angle = 0)
    {
        PoolAble a = PoolManager.Instance.Pop(Bullet.name);
        a.transform.position = _pos.position;
        a.transform.rotation = Quaternion.LookRotation(Player.position);
        a.transform.rotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(0, angle, 0));
        a.GetComponent<WizardBullet>().SetDamage(ATK, transform);
    }
}
