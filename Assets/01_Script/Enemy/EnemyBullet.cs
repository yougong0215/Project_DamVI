using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : PoolAble
{

    private Transform _player;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
            }
            return _player;
        }
    }

    Quaternion dir = Quaternion.identity;
    public float speed = 0.3f;
    int damage = 0;

    Transform enemy;

    protected void OnEnable()
    {

        StartCoroutine(Shoot());
    }

    public void SetDamage(int val, Transform e)
    {
        damage = val;
        enemy = e;
       // dir = d;
    }

    protected virtual IEnumerator Shoot()
    {
        yield return null;
        yield return new WaitForSeconds(0.2f);
        //dir = (Player.position - transform.position).normalized;
        if(speed != 0.3f)
             speed = 30f;
        StartCoroutine(die());
    }

    private void Update()
    {     
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player" && other.GetComponent<PlayerMove>().isDoged== false)
        {
            Player.GetComponent<PlayerInteraction>().Damaged(damage);
            Player.GetComponent<PlayerInteraction>().arrmorBlack(100000, enemy);
            PoolManager.Instance.Push(this);
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(0.6f);
        PoolManager.Instance.Push(this);

    }

}
