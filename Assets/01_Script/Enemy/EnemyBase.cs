using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("적 기본정보")]
    [SerializeField] protected int MaxHP = 0;
    [SerializeField] protected int HP = 0;
    [SerializeField] protected int Barrier = 0;
    [SerializeField] protected int ATK = 0;
    [SerializeField] protected int _reviveCount = 0;

    [SerializeField] protected bool BossMonster = false;
    [SerializeField] protected bool _superArrmor = false;
    [SerializeField] float _stunTime = 0;
    [SerializeField] protected float _AttackDelayTime = 0;

    [SerializeField] protected int _detectionLength = 0;
    [SerializeField] protected float _attackLength = 0;

    [SerializeField] protected float _fiber = 1;

    [Header("적 프러퍼티")]
    [SerializeField] protected NavMeshAgent _nav;
    [SerializeField] protected Rigidbody _rigid;
    [SerializeField] protected Animator _ani;

    private void OnEnable()
    {
        _nav = GetComponent<NavMeshAgent>();
        _rigid = gameObject.GetComponent<Rigidbody>();
        HP = MaxHP;

        if (GetComponent<Animator>())
        {
            _ani = GetComponent<Animator>();
        }
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
        _AttackDelayTime -= Time.deltaTime;
        _stunTime -= Time.deltaTime;

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
        EnemyDetectionLength();
    }


    /// <summary>
    /// 원형범위에서의 탐지를 뜻함 따로 뒤에서 암살같은거 할꺼면 안쓸꺼임
    /// 보스같은 경우엔 필요 없음
    /// </summary>
    private void EnemyDetectionLength()
    {

        float Length = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.position.x, 2) + Mathf.Pow(transform.position.z - Player.position.z, 2));
        if (_stunTime <= 0 )
        {
            if (Length <= _detectionLength) // 탐지 거리
            {

                _ani.SetBool("Detection", true);
                EnemyDetection();
                

            }
            else
            {
                _ani.SetBool("Detection", false);
                IdleEnemy();
                try
                {
                    _nav.ResetPath();
                    _nav.SetDestination(transform.position);
                }
                catch
                {

                }
            }
        }
        else
        {
            _ani.SetBool("Detection", false);
            Debug.Log($"{_stunTime}, {_AttackDelayTime}");
        }

    }


    protected virtual void MoveEnemy()
    {
        _nav.SetDestination(Player.position);
        _nav.stoppingDistance = _attackLength - 0.5f;
    }
    protected virtual void IdleEnemy()
    {
        _ani.SetBool("Move", false);
    }

    /// <summary>
    /// 여기서 적이 행동하는거 쓰면될듯
    /// </summary>
    protected virtual void EnemyDetection()
    {
        
    }

    protected virtual void AttackBase()
    {

    }

    protected virtual void AttackCoolBase()
    {
        
    }

    public virtual void DamagedCool(int ATK, float stuntime, Vector3 NuckBack, bool Grab, float DelayTime)
    {
        StartCoroutine(DamagedForPlayer(ATK, stuntime, NuckBack, Grab, DelayTime));
    }


    /// <summary>
    /// 데미지 받을때 이거 받아오면됨
    /// 계산된 데미지를 불러와야
    /// </summary>
    /// <param name="ATK"></param> 
    public virtual IEnumerator DamagedForPlayer(int ATK, float stuntime, Vector3 NuckBack, bool Grab, float DelayTIme)
    {
        yield return null;

        if (_superArrmor == false)
        {
            _stunTime = stuntime;
            Vector3 force = (transform.position - Player.transform.position).normalized;
            int Grabing = Grab ? -1 : 1;

            _rigid.AddForce(force * 2 * _fiber, ForceMode.VelocityChange);//force.x * NuckBack.x, force.y * NuckBack.y, force.z * NuckBack.z) * Grabing, ForceMode.Impulse);
            
            
            if(_stunTime > 0)
            {
                _ani.SetBool("hit", true);
            }
        }

        HP -= ATK;
        yield return new WaitForSeconds(0.3f);
        _rigid.velocity = Vector3.zero;
        _ani.SetBool("hit", false);
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
        //gameObject.SetActive(false);
        
        Destroy(this.gameObject);
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
