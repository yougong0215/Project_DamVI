using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] string nameing = "이름없음";
    [Header("적 기본정보")]
    [SerializeField] public float MaxHP = 0;
    [SerializeField] public float HP = 0;
    [SerializeField] public int Barrier = 0;
    [SerializeField] public int ATK = 0;
    [SerializeField] public int _reviveCount = 0;
    
    

    [SerializeField] public bool BossMonster = false;
    [SerializeField] public bool _superArrmor = false;
    [SerializeField] float _stunTime = 0;
    [SerializeField] public float _AttackDelayTime = 0;

    [SerializeField] public int _detectionLength = 0;
    [SerializeField] public float _attackLength = 0;
    [SerializeField] public bool _die =false;

    [SerializeField] public float _fiber = 1;
    [SerializeField] public float Score = 0;
    [SerializeField] public int MinScore = 0;
    

    [Header("적 프러퍼티")]
    [SerializeField] public NavMeshAgent _nav;
    [SerializeField] public Rigidbody _rigid;
    [SerializeField] public Animator _ani;
    [SerializeField] public GameObject UI;
    [SerializeField] public Image HPUI;
    [SerializeField] public TextMeshProUGUI Name;
    GameObject cam;

    private void OnEnable()
    {
        _nav = GetComponent<NavMeshAgent>();
        _rigid = gameObject.GetComponent<Rigidbody>();
        cam = GameObject.Find("Cam");


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

    private void LateUpdate()
    {
        
        Name.text = $"{nameing}";
        HPUI.fillAmount = (HP) / (MaxHP);
      
    }

    private void Update()
    {
        UI.transform.localEulerAngles = cam.transform.localEulerAngles;

        _AttackDelayTime -= Time.deltaTime;
        _stunTime -= Time.deltaTime;
        Debug.Log((HP) / (MaxHP));
        Score -= Time.deltaTime;

        if(Score < MinScore)
        {
            Score = MinScore;
        }

        if (HP <= 0)
        {
            if (_reviveCount > 0)
            {
                HP = ReviveEvent();
                Debug.Log("응애");
            }
            else if(_die == false)
            {
                _die = true;
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
        if (_stunTime <= 0)
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
                if (_ani.GetBool("Die") == false)
                {
                    _nav.ResetPath();
                    _nav.SetDestination(transform.position);
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
        if (_ani.GetBool("Die") == false)
        {
            _nav.SetDestination(Player.position);
            _nav.stoppingDistance = _attackLength - 0.5f;
        }
    }
    protected virtual void IdleEnemy()
    {
        _rigid.velocity = new Vector2(0, 0);
        //_nav.SetDestination(transform.position);
        _ani.SetBool("Move", false);
    }

    /// <summary>
    /// 여기서 적이 행동하는거 쓰면될듯
    /// </summary>
    protected virtual void EnemyDetection()
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


            if (_stunTime > 0)
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
        
        _ani.SetBool("Die", true);
        _nav.enabled = false;
        PlayerAttackManager.Instance.CurrentScore += (int)Score;
        Destroy(this.gameObject, 0.5f);
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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            _rigid.velocity = new Vector3(0, 0, 0);
        }
    }
}
