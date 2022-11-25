using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class PlayerAttackManager : Singleton<PlayerAttackManager>
{
    [Header("Enum 들")]
    [SerializeField] public PlayerStatues playerStat;
    [SerializeField] public PlayerPripoty playerpri;

    [Header("에니메이션")]
    [SerializeField] public Animator _ani;

    [Header("적들")]
    [SerializeField] Collider[] hit;

    [Header("UI")]
    [SerializeField] GameObject _aimDraw;

    [SerializeField] public BulletReload Bullet;


    [SerializeField] private bool _normalAttack = false;
    [SerializeField] private int _playerAttackValue;

    Coroutine co = null;
    public PlayerInteraction _inter;



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

    public PlayerStatues PlayerS
    {
        get => playerStat;
        set
        {
            playerStat = value;
        }
    }
    public PlayerPripoty PlayerP
    {
        get => playerpri;
        set
        {
            playerpri = value;
        }
    }

    Vector3 OriginZoomUivec;
    void Start()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
        _ani = Player.GetComponent<Animator>();
        _inter = GetComponent<PlayerInteraction>();
        OriginZoomUivec = _aimDraw.transform.position;
        _aimDraw.GetComponent<RectTransform>().localPosition = new Vector3(10000, 10000, 10000);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //LookEnemy();
        }

        if (Input.GetMouseButton(1) == false)
        {
            Attack();
        }
        else if (_normalAttack == false && Input.GetMouseButton(1))
        {
            Aimaing();
        }

        if (Input.GetKeyDown(KeyCode.Q) && _inter.I_MP >= 20 && (playerpri == PlayerPripoty.Move || playerpri == PlayerPripoty.none || playerpri == PlayerPripoty.Fight))
        {
            _inter.UseMp(20);
            playerpri = PlayerPripoty.weaponAttack;
            _ani.SetBool("Weapon", true);
        }

        if (Input.GetMouseButtonUp(1) && playerpri == PlayerPripoty.aiming)
        {
            SetStateNone();
        }
    }

    void Aimaing()
    {   
        SetStateAim();
        if (Input.GetMouseButtonDown(0))
        {
            _ani.SetTrigger("AimShoot");
        }
        if (Input.GetKeyDown(KeyCode.R) && _inter.I_MP >= 10)
        {
            _inter.UseMp(10);
            Bullet._bulletcount = ShopState.Instance.BulletAdd;
        }



    }


    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && (playerpri == PlayerPripoty.Move || playerpri == PlayerPripoty.none || playerpri == PlayerPripoty.Fight))
        {
            _ani.SetInteger("Attack", 1);
        }



    }

    public void Corutines()
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(clearStat());
    }

    public void SetStateAim()
    {
        playerpri = PlayerPripoty.aiming;
        playerStat = PlayerStatues.bifurcationAttack1;
        Player.GetComponent<PlayerMove>().ArrowLook.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        _ani.SetBool("Aiming", true);
        _aimDraw.GetComponent<RectTransform>().position = OriginZoomUivec;
    }

    public void SetStateNone()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
        Player.GetComponent<PlayerMove>().ArrowLook.GetComponent<CinemachineVirtualCamera>().Priority = 9;

        _ani.SetBool("Aiming", false);
        _ani.SetInteger("Attack", 0);
        _ani.SetBool("Weapon", false);
        _aimDraw.GetComponent<RectTransform>().localPosition = new Vector3(10000, 10000, 10000);
    }

    IEnumerator clearStat()
    {
        if(_ani.GetBool("Aiming")== false)
            Player.GetComponent<PlayerMove>().ArrowLook.GetComponent<CinemachineVirtualCamera>().Priority = 9;

        _normalAttack = true;
        yield return new WaitForSeconds(0.1f);

        SetStateNone();
        _normalAttack = false;
    }

    public void Rectmove(float f = 0.9f)
    {
        _aimDraw.GetComponent<RectTransform>().DOScale(1.05f, 0.05f).SetEase(Ease.InQuint).OnComplete(
            ()=> _aimDraw.GetComponent<RectTransform>().DOScale(1, 0.05f).SetEase(Ease.InQuint)
        );
    }

    //protected void LookEnemy()
    //{
    //    if (_inter.DistannsEnemy())
    //    {
    //        Vector3 enemy = (_inter.DistannsEnemy().position - Player.transform.position).normalized;
    //        Debug.Log(enemy);
    //        Player.rotation = Quaternion.LookRotation(enemy);
    //        Player.localEulerAngles = new Vector3(0, Player.localEulerAngles.y, 0);
    //        //Player.rotation = Quaternion.Euler(0, Player.localEulerAngles.y, 0);
    //    }
    //    else
    //    {
    //        Player.rotation = Player.GetComponent<PlayerMove>().GetCameraAngel();
    //    }
    //}
}
