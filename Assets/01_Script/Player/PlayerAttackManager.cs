using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : Singleton<PlayerAttackManager>
{
    [Header("Enum 들")]
    [SerializeField] public PlayerStatues playerStat;
    [SerializeField] public PlayerPripoty playerpri;

    [Header("에니메이션")]
    [SerializeField] Animator _ani;

    [Header("적들")]
    [SerializeField] Collider[] hit;

    [SerializeField] private int _playerAttackValue;

    Coroutine co = null;




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

    void Start()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
        _ani = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButton(1) == false)
        {
            Attack();
        }
        else
        {
            Aimaing();
        }
    }

    void Aimaing()
    {
        if (Input.GetMouseButtonDown(0) && (playerpri == PlayerPripoty.Move || playerpri == PlayerPripoty.none || playerpri == PlayerPripoty.Fight))
        {
            _ani.SetInteger("Attack", 1);
        }

        SetStateAim();

    }


    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && (playerpri == PlayerPripoty.Move || playerpri == PlayerPripoty.none || playerpri == PlayerPripoty.Fight))
        {
            _ani.SetInteger("Attack", 1);
            if(co !=null)
                StopCoroutine(co);
            co = StartCoroutine(clearStat());
        }
    }

    public void SetStateAim()
    {
        playerpri = PlayerPripoty.aiming;
        playerStat = PlayerStatues.bifurcationAttack1;
    }

    public void SetStateNone()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
    }

    IEnumerator clearStat()
    {
        yield return new WaitForSeconds(0.6f);
            SetStateNone();
    }
   
}
