using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardBullet : PoolAble
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
    float speed = 0.3f;
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
        speed = 0.3f;
        //dir = (Player.position - transform.position).normalized;
        speed = 5f;

        StartCoroutine(damagelow());
        StartCoroutine(die());
    }

    private void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    IEnumerator damagelow()
    {
        yield return new WaitForSeconds(1);
        damage -= 1;
        StartCoroutine(damagelow());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            Player.GetComponent<PlayerInteraction>().Damaged(damage);
            //Player.GetComponent<PlayerInteraction>().arrmorBlack(100000, enemy);
            PoolManager.Instance.Push(this);
        }
    }

    IEnumerator die()
    {
        yield return new WaitForSeconds(5f);
        PoolManager.Instance.Push(this);

    }

}
