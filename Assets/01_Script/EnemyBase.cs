using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("적 기본정보")]
    [SerializeField] int HP = 0;
    [SerializeField] int ATK = 0;
    [SerializeField] int _reviveCount = 0;
    [SerializeField] int _detectionLength = 0;
    [SerializeField] bool BossMonster = false;
    [SerializeField] bool _superArrmor = false;
    [SerializeField] float _stunTime = 0;
    [SerializeField] float _DelayTime = 0;

    [Header("적이 기억한플레이어")]
    [SerializeField] PlayerStatues _playerStat = PlayerStatues.Idle;

    [Header("적 프러퍼티")]
    [SerializeField] protected Rigidbody _rigid;

    private void OnEnable()
    {
        _rigid = gameObject.GetComponent<Rigidbody>();
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

    private void Update()
    {
        _DelayTime -= Time.deltaTime;
        _stunTime -= Time.deltaTime;

        if(PlayerAttackManager.Instance.playerpri == PlayerPripoty.Move || PlayerAttackManager.Instance.playerpri == PlayerPripoty.none)
        {
            _playerStat = PlayerStatues.Idle;
        }
    }


    /// <summary>
    /// 원형범위에서의 탐지를 뜻함 따로 뒤에서 암살같은거 할꺼면 안쓸꺼임
    /// 보스같은 경우엔 필요 없음
    /// </summary>
    private void EnemyDetectionLength()
    {

            float Length = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.position.x, 2) + Mathf.Pow(transform.position.z - Player.position.z, 2));
            if (_stunTime <= 0 && _DelayTime <= 0)
            {
                if (Length <= _detectionLength)
                {
                    EnemyDetection();
                }
                else
                {
                    IdleEnemy();
                }
            }
        
    }

    protected virtual void IdleEnemy()
    {

    }

    /// <summary>
    /// 여기서 적이 행동하는거 쓰면될듯
    /// </summary>
    protected virtual void EnemyDetection()
    {

    }



    /// <summary>
    /// 데미지 받을때 이거 받아오면됨
    /// 계산된 데미지를 불러와야
    /// </summary>
    /// <param name="ATK"></param> 
    public virtual void DamagedForPlayer(int ATK, float stuntime, Vector3 NuckBack, bool Grab)
    {
        if (_playerStat != PlayerAttackManager.Instance.playerStat)
        {
            _rigid.velocity = new Vector3(0, 0, 0);
            _playerStat = PlayerAttackManager.Instance.playerStat;
            if (_superArrmor == false)
            {
                _stunTime = stuntime;
                StopAllCoroutines();
                Vector3 force = (transform.position - Player.transform.position).normalized;
                int Grabing = Grab ? -1 : 1;

                _rigid.AddForce(new Vector3(force.x * NuckBack.x, force.y * NuckBack.y, force.z * NuckBack.z) * Grabing, ForceMode.Impulse);

            }

            HP -= ATK;
            if (HP <= 0)
            {
                if (_reviveCount > 0)
                {
                    HP = ReviveEvent();
                    Debug.Log("응애");
                }
                else
                {
                    DieEvent();
                }
            }
        }
    }

    public virtual void DamageEvent()
    {

    }

    /// <summary>
    /// 배이스는 Detroy
    /// 죽으면 되는거
    /// </summary>
    protected virtual void DieEvent()
    {
        //Destroy(this.gameObject);
    }

    /// <summary>
    /// 붕괴마냥 카운트 주고 되살아 나는거
    /// </summary>
    /// <returns></returns>
    protected virtual int ReviveEvent()
    {
        int ReviveHP = 0;
        _reviveCount--;


        return ReviveHP;
    }
}
