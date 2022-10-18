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


    float Delay = 0;

    void Start()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
        _ani = Player.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       

        Debug.Log(PlayerP);
        Attack();
        RunState();
    }

    void RunState()
    {
        if(Input.GetKey(KeyCode.LeftShift))
        {
            if(PlayerP != PlayerPripoty.Fight && PlayerP != PlayerPripoty.doged)
                 PlayerP = PlayerPripoty.Move;
        }
    }

    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && (playerpri == PlayerPripoty.Move || playerpri == PlayerPripoty.none || playerpri == PlayerPripoty.Fight))
        {
            _ani.SetTrigger("Attack");
            SetDelayZero();
        }
       // Debug.Log(Delay);
        if (Delay >= 0.34f)
        {
            PlayerP = PlayerPripoty.none;
            PlayerS = PlayerStatues.Idle;
        }
        Delay += Time.deltaTime;
    }

    public void SetDelayZero()
    {
        Delay = 0;
    }

    public void SetStateNone()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
    }
    
    public void AbleDamage(Vector3 size)
    {
        if (size == Vector3.zero)
        {
            size = new Vector3(1.5f, 1.5f, 1.5f);
        }

    }
}
