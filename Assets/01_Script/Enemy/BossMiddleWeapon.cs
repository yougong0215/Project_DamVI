using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BossMiddleWeapon : MonoBehaviour
{
    [SerializeField] EnemyBullet bul;
    [SerializeField] Transform ts;
    [SerializeField] Transform pl;

    private void OnEnable()
    {
        pl = Player.Find("EnemyDetection");
    }

    bool widthattack = false;
    bool objwhill = false;
    bool obsjwhill = false;
    public void Fire(int ATK, float speed = 0.3f)
    {
        EnemyBullet b = PoolManager.Instance.Pop(bul.name) as EnemyBullet;
        b.transform.position = ts.position;
        b.transform.rotation = transform.rotation;
        b.SetDamage(ATK, transform);
        b.speed = speed;
    }
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

    public void ShootVelodown(Vector3 vec, float speed)
    {
        transform.localPosition = vec;
        transform.DOMoveY(1, speed);
    }

    public void ShootUpSide()
    {
        transform.position = Player.position + new Vector3(0, 10);
        transform.DOMoveY(1, 1f);

    }

    public void myWhill()
    {
        objwhill = true;
        widthattack = false;
    }

    public void ShootWhill(Vector3 pos)
    {
        obsjwhill = true;
        transform.parent = Player.Find("EnemyDetection");
        transform.localPosition = pos;
    }
    public void ShootingWhill(Vector3 pos)
    {
        widthattack = true;
        obsjwhill = true;
        objwhill = true;
        transform.parent = Player.Find("EnemyDetection");
        transform.localPosition = pos;
    }

    public void StopWhill(Vector3 pos, Transform e)
    {
        widthattack = false;
        obsjwhill = false;
        objwhill = false;
        transform.parent = e;
        transform.localPosition = pos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerInteraction>().Damaged(50);
        }
    }

    private void Update()
    {
        if(widthattack == false)
        {
            transform.rotation = Quaternion.LookRotation(Player.position);
        }
        if (obsjwhill == true)
        {
            pl.localEulerAngles += new Vector3(0, 300, 0) * Time.deltaTime;
        }

        if (objwhill == true)
        {
            transform.localEulerAngles += new Vector3(0, 300, 0) * Time.deltaTime;
        }

    }
    public void PosReset(Vector3 pos)
    {
        transform.localPosition = pos;
    }
}
