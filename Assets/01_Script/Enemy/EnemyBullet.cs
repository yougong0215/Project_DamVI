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
    Vector3 dir = Vector3.zero;
    float speed = 0.3f;
    int damage = 0;

    protected void OnEnable()
    {

        StartCoroutine(Shoot());
    }

    public void SetDamage(int val)
    {
        damage = val;
    }

    protected virtual IEnumerator Shoot()
    {
        yield return null;
        speed = 0.3f;
        dir = (Player.position - transform.position).normalized;
        yield return new WaitForSeconds(0.2f);
        //dir = (Player.position - transform.position).normalized;
        speed = 30f;
        StartCoroutine(die());
    }

    private void Update()
    {     
        transform.position += dir * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name == "Player")
        {
            Player.GetComponent<PlayerInteraction>().Damaged(damage);
            PoolManager.Instance.Push(this);
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(0.6f);
        PoolManager.Instance.Push(this);

    }

}
