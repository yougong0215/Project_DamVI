using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : Singleton<PlayerAttackManager>
{
    [Header("Enum 들")]
    [SerializeField] PlayerStatues playerStat;
    [SerializeField] PlayerPripoty playerpri;

    [Header("에니메이션")]
    [SerializeField] Animator _ani;

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
    Collider[] hit;

    void Start()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
        hit = Physics.OverlapBox(transform.position, new Vector3(4, 4, 4), Quaternion.identity, 1 << (LayerMask.NameToLayer("InterectionObj")));
        _ani = Player.GetComponent<Animator>();
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && (playerpri == PlayerPripoty.Move || playerpri == PlayerPripoty.none || playerpri == PlayerPripoty.Fight))
        {
            playerpri = PlayerPripoty.Fight;
            _ani.SetTrigger("Attack");
            //CameraManager.Instance.CiemanchineChange("ArrowView");
        }
        //if(Input.GetMouseButtonUp(0))
        {
            //playerpri = PlayerPripoty.none;
            //CameraManager.Instance.PlayerViewChange();
        }
    }

    public void SetStateNone()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
    }
    

}
