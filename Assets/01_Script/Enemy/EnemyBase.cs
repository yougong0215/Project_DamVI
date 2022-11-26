using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public abstract class EnemyBase : MonoBehaviour
{
    [SerializeField] string nameing = "�̸�����";
    [Header("�� �⺻����")]
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
    

    [Header("�� ������Ƽ")]
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
                Debug.Log("����");
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
    /// �������������� Ž���� ���� ���� �ڿ��� �ϻ찰���� �Ҳ��� �Ⱦ�����
    /// �������� ��쿣 �ʿ� ����
    /// </summary>
    private void EnemyDetectionLength()
    {

        float Length = Mathf.Sqrt(Mathf.Pow(transform.position.x - Player.position.x, 2) + Mathf.Pow(transform.position.z - Player.position.z, 2));
        if (_stunTime <= 0)
        {
            if (Length <= _detectionLength) // Ž�� �Ÿ�
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
    /// ���⼭ ���� �ൿ�ϴ°� ����ɵ�
    /// </summary>
    protected virtual void EnemyDetection()
    {

    }


    public virtual void DamagedCool(int ATK, float stuntime, Vector3 NuckBack, bool Grab, float DelayTime)
    {
        StartCoroutine(DamagedForPlayer(ATK, stuntime, NuckBack, Grab, DelayTime));
    }


    /// <summary>
    /// ������ ������ �̰� �޾ƿ����
    /// ���� �������� �ҷ��;�
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
    /// ���̽��� Detroy
    /// ������ �Ǵ°�
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
    /// �ر����� ī��Ʈ �ְ� �ǻ�� ���°�
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
