using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using Cinemachine;

public class Bullet : PoolAble
{
    [SerializeField] int damage = 0;
    [SerializeField] float stun = 0;
    [SerializeField] Vector3 NuckBack = Vector3.zero;
    [SerializeField] bool Grab;
    [SerializeField] float DelayTime = 0;
    [SerializeField] float _speed = 50f;


    bool _aimshoot = false;

    int num = 0;

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

    Collider[] col;
    Vector3 dir;
    private void OnEnable()
    {

        _aimshoot = false;

        num = 0;
        transform.position -= new Vector3(0, 2f, 0);
        GetComponent<TrailRenderer>().enabled = false;
        GetComponent<TrailRenderer>().enabled = true;
        GetComponentInChildren<VisualEffect>().enabled = true;
        GetComponentInChildren<VisualEffect>().Stop();
        if (PlayerAttackManager.Instance.PlayerP != PlayerPripoty.aiming)
        {
            dir = GameManager.Instance.Player.localRotation * Vector3.forward;
            dir.y = 0;
            dir.Normalize();
        }
        else
        {
            _aimshoot = true;
            dir = Player.GetComponent<PlayerMove>().ArrowLook.transform.forward;
            dir.Normalize();
        }
        GetComponent<TrailRenderer>().enabled = true;
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
            if (_aimshoot == false)
            {
                //col = Physics.OverlapBox(other.transform.position
                //, new Vector3(1f, 1f, 1f), Quaternion.identity, 1 << LayerMask.NameToLayer("Enemy")); ;

                //transform.position = other.transform.position;
                //dir = Vector2.zero;

                //for (int i = 0; i < col.Length; i++)
                //{
                //    Debug.Log(col[i].name);
                //    if (col[i].GetComponent<EnemyBase>())
                //    {
                //        col[i].GetComponent<EnemyBase>().DamagedCool(damage, stun, NuckBack, Grab, DelayTime);
                //        num++;
                //    }
                //}
                if (other.GetComponent<EnemyBase>())
                {
                    other.GetComponent<EnemyBase>().DamagedCool(damage, stun, NuckBack, Grab, DelayTime);
                    GetComponent<TrailRenderer>().enabled = false;
                    GetComponentInChildren<VisualEffect>().Play();
                    PoolManager.Instance.Push(this);
                }
                //StartCoroutine(die());
            }
            else
            {
                if (other.GetComponent<EnemyBase>())
                {
                    other.GetComponent<EnemyBase>().DamagedCool(damage, stun, NuckBack, Grab, DelayTime);
                    GetComponent<TrailRenderer>().enabled = false;
                    GetComponentInChildren<VisualEffect>().Play();
                    PoolManager.Instance.Push(this);
                }
            }

          

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
        GetComponentInChildren<VisualEffect>().Play();
        yield return new WaitUntil(() => num != 0);
        PoolManager.Instance.Push(this);
    }
}
