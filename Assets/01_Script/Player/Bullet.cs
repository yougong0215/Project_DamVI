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
        GetComponent<TrailRenderer>().Clear();
        _aimshoot = false;

        num = 0;
        transform.position -= new Vector3(0, 2f, 0);
        GetComponent<CapsuleCollider>().enabled = true;

        GetComponentInChildren<VisualEffect>().Stop();
        if (PlayerAttackManager.Instance.PlayerP != PlayerPripoty.aiming)
        {
            transform.forward = Player.forward;
            transform.rotation = Player.GetComponent<Weapon>().Dir(transform.localEulerAngles);
            dir.Normalize();
        }
        else
        {
            _aimshoot = true;
            dir = Player.GetComponent<PlayerMove>().ArrowLook.transform.forward;
            dir.Normalize();
        }
        if (Player.GetComponent<Weapon>().Shoot() == false)
        {
            StartCoroutine(timeOut(0.3f));
        }
        else
        {
            StartCoroutine(timeOut(1));
        }
        StartCoroutine(ROtateing());
    }


    private IEnumerator ROtateing()
    {
        GetComponent<TrailRenderer>().Clear();
        yield return null;
        GetComponent<TrailRenderer>().time = -100;
        if (gameObject.name != "ActiveBullet")
        {
            GetComponentInChildren<VisualEffect>().enabled = true;
        }
        else
        {
            GetComponentInChildren<VisualEffect>().enabled = true;
            //GetComponentInChildren<VisualEffect>().transform.localScale = new Vector3(1000, 1000, 1000);
        }
        GetComponent<TrailRenderer>().time = 1.5f;

    }
    private void Update()
    {
        if(_aimshoot == true)
        {
            transform.position += dir * _speed * Time.deltaTime;
        }
        else
        {
            transform.position += transform.forward * _speed * Time.deltaTime;
        }
        
        //Debug.Log(dir);
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(1.5f, 1.5f, 1.5f));
    }

    private void OnTriggerEnter(Collider other)
    {

        float a = Random.Range(0, 21) + Random.Range(0, 21) + Random.Range(0, 21) + Random.Range(0, 21) + Random.Range(0, 21);
        Debug.Log(a);

        num = 0;
        if (other.gameObject.layer == 30)
        {
            if (_aimshoot == false)
            {
                StartCoroutine(Attack(other));
            }
            else
            {
                if (other.GetComponent<EnemyBase>())
                {
                    other.GetComponent<EnemyBase>().DamagedCool((int)(damage * PlayerAttackManager.Instance._inter.CalcDamage()), stun, NuckBack, Grab, DelayTime);

                    if(a < ShopState.Instance.CriticalAdd)
                    {
                        other.GetComponent<EnemyBase>().DamagedCool((int)((damage/2) * PlayerAttackManager.Instance._inter.CalcDamage()), stun, NuckBack, Grab, DelayTime);
                    }
                   // GetComponent<TrailRenderer>().enabled = false;
                   // GetComponent<TrailRenderer>().
                    GetComponentInChildren<VisualEffect>().Play();
                    PoolManager.Instance.Push(this);
                }
            }

          

        }
    }
    IEnumerator Attack(Collider other)
    {
        col = Physics.OverlapBox(other.transform.position
        , new Vector3(1.5f, 1.5f, 1.5f), Quaternion.identity, 1 << LayerMask.NameToLayer("Enemy")); ;

        transform.position = other.transform.position;
        dir = Vector2.zero;

        for (int i = 0; i < col.Length; i++)
        {
            yield return null;
            try
            {
                if (col[i].GetComponent<EnemyBase>())
                {

                    col[i].GetComponent<EnemyBase>().DamagedCool(damage, stun, NuckBack, Grab, DelayTime);


                    num++;
                }
            }
            catch
            {
                num++;
            }
        }
        //if (other.GetComponent<EnemyBase>())
        //{
        //    other.GetComponent<EnemyBase>().DamagedCool(damage, stun, NuckBack, Grab, DelayTime);
        //    GetComponent<TrailRenderer>().enabled = false;
        //    GetComponentInChildren<VisualEffect>().Play();
        //    PoolManager.Instance.Push(this);
        //}

        GetComponent<TrailRenderer>().time = -100;
        StartCoroutine(die());
    }



    IEnumerator timeOut(float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<TrailRenderer>().time = -100;
        //GetComponent<TrailRenderer>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        PoolManager.Instance.Push(this);
    }
    
    IEnumerator die()
    {
        GetComponent<TrailRenderer>().time = -100;
        //GetComponent<TrailRenderer>().enabled = false;
        yield return null;
        GetComponent<TrailRenderer>().time = -100;
        GetComponentInChildren<VisualEffect>().Play();
        GetComponent<CapsuleCollider>().enabled = false;
        GetComponent<TrailRenderer>().Clear();
        //yield return new WaitForSeconds(0.3f);
        yield return new WaitUntil(() => num != 0);
        PoolManager.Instance.Push(this);
    }
}
