using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class BossMiddleWeapon : MonoBehaviour
{
    [SerializeField] WizardBullet bul;
    [SerializeField] Transform ts;
    [SerializeField] Transform pl;
    [SerializeField] public AudioSource audios;
    [SerializeField] public AudioClip clip;

    private void OnEnable()
    {
        pl = Player.Find("EnemyDetection");
    }
    Coroutine co;
    bool widthattack = false;
    bool objwhill = false;
    bool obsjwhill = false;
    public void Fire(int ATK, float speed = 0.3f)
    {
        widthattack = true;
        WizardBullet b = PoolManager.Instance.Pop(bul.name) as WizardBullet;

        audios.PlayOneShot(clip);

        b.transform.position = ts.position;
        b.transform.rotation = transform.rotation;
        if(ATK / 5 < 1)
        {
            b.SetDamage(1, transform);
        }
        else
        {
            b.SetDamage(ATK / 5, transform);
        }
        b.speed = speed;
        if (co != null)
            StopCoroutine(Stoping());
        co = StartCoroutine(Stoping());

    }

    IEnumerator Stoping()
    {
        yield return new WaitForSeconds(0.1f);
        widthattack = false;
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

    public void ShootVelodown(Vector3 vec, float speed, Transform trans)
    {
        
        transform.rotation = Quaternion.LookRotation(Player.position + new Vector3(0,1.2f,0) - transform.position);
        transform.parent = trans;
        transform.localPosition = vec;
        transform.DOMoveY(1, speed);
        StartCoroutine(I());
    }

    public void ShootUpSide()
    {
        widthattack = true;
        transform.position = Player.position + new Vector3(0, 10);
        transform.DOMoveY(1, 1f);
        StartCoroutine(I());
    }

    public void myWhill()
    {
        obsjwhill = true;
        widthattack = false;

        StartCoroutine(I());
    }

    public void ShootWhill(Vector3 pos)
    {
        obsjwhill = true;
        transform.parent = Player.Find("EnemyDetection");
        transform.localPosition = pos;
        if (SceneManager.GetActiveScene().name != "Loop")
            StartCoroutine(I());
    }
    public void ShootingWhill(Vector3 pos)
    {
        widthattack = true;
        obsjwhill = true;
        objwhill = true;
        transform.parent = Player.Find("EnemyDetection");
        transform.localPosition = pos;
        if(SceneManager.GetActiveScene().name != "Loop")
            StartCoroutine(I());
    }

    public void StopWhill(Vector3 pos, Transform e)
    {
        widthattack = false;
        obsjwhill = false;
        objwhill = false;

        transform.parent = e;
        transform.localPosition = pos;
        StartCoroutine(I());
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            other.GetComponent<PlayerInteraction>().Damaged(50);
            other.GetComponent<PlayerInteraction>().arrmorBlack(1000000, transform);
        }
    }

    IEnumerator I()
    {
        yield return new WaitForSeconds(0.1f);
        transform.parent = null;
    }

    private void Update()
    {
        if (widthattack == false)
        {
            transform.rotation = Quaternion.LookRotation(Player.position + new Vector3(0, 1.2f, 0) - transform.position);
        }
        else
        {
            transform.localEulerAngles = new Vector3(0, 90, 0);
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
}
