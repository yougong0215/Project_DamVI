using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Bullet : PoolAble
{
    [SerializeField] int damage = 0;
    [SerializeField] float stun = 0;
    [SerializeField] Vector3 NuckBack = Vector3.zero;
    [SerializeField] bool Grab;
    [SerializeField] float DelayTime = 0;
    [SerializeField] float _speed = 50f;

    int num = 0;

    Collider[] col;
    Vector3 dir;
    private void OnEnable()
    {
        num = 0;
        GetComponent<TrailRenderer>().enabled = true;
        GetComponentInChildren<VisualEffect>().enabled = false;
        dir = GameManager.Instance.Player.localRotation * Vector3.forward;
        dir.y = 0;
        dir.Normalize();
    }

    private void Update()
    {
        transform.position += dir* _speed * Time.deltaTime;
        StartCoroutine(timeOut());
        //Debug.Log(dir);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.5f, 1.5f, 1.5f));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 30)
        {
            damage = 10;
            col = Physics.OverlapBox(other.transform.position
           , new Vector3(1.5f, 1.5f, 1.5f), Quaternion.identity, 1 << LayerMask.NameToLayer("Enemy")); ;

            transform.position = other.transform.position;
            dir = Vector2.zero;

            for (int i = 0; i < col.Length; i++)
            {
                Debug.Log(col[i].name);
                if (col[i].GetComponent<EnemyBase>())
                {
                    col[i].GetComponent<EnemyBase>().DamagedCool(damage, stun, NuckBack, Grab, DelayTime);
                    num++;
                }
            }
            

            StartCoroutine(die());

        }
    }

    IEnumerator timeOut()
    {
        yield return new WaitForSeconds(1);
        PoolManager.Instance.Push(this);
    }
    
    IEnumerator die()
    {
        GetComponent<TrailRenderer>().enabled = false;
        GetComponentInChildren<VisualEffect>().enabled = true;
        yield return new WaitUntil(() => num != 0);
        PoolManager.Instance.Push(this);
    }
}
