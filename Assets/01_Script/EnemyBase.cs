using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("�� �⺻����")]
    [SerializeField] int MaxHP = 0;
    [SerializeField] int HP = 0;
    [SerializeField] int Barrier = 0;
    [SerializeField] int ATK = 0;
    [SerializeField] int _reviveCount = 0;
    [SerializeField] int _detectionLength = 0;
    [SerializeField] bool BossMonster = false;
    [SerializeField] bool _superArrmor = false;
    [SerializeField] float _stunTime = 0;
    [SerializeField] float _AttackDelayTime = 0;

    [Header("���� ������÷��̾�")]
    [SerializeField] PlayerStatues _playerStat = PlayerStatues.Idle;

    [Header("�� ������Ƽ")]
    [SerializeField] NavMeshAgent _nav;
    [SerializeField] protected Rigidbody _rigid;

    private void OnEnable()
    {
        _nav = GetComponent<NavMeshAgent>();
        _rigid = gameObject.GetComponent<Rigidbody>();
        HP = MaxHP;
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
                Debug.Log("����");
            }
            else
            {
                DieEvent();
            }
        }



        if (PlayerAttackManager.Instance.playerpri == PlayerPripoty.Move || PlayerAttackManager.Instance.playerpri == PlayerPripoty.none)
        {
            _playerStat = PlayerStatues.Idle;
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
        if (_stunTime <= 0 && _AttackDelayTime <= 0)
        {
            if (Length <= _detectionLength)
            {
                _nav.SetDestination(Player.position);
                _nav.stoppingDistance = 2;

                if(Length <= 3)
                {
                    EnemyDetection();
                }

            }
            else
            {
                IdleEnemy();
                _nav.ResetPath();
            }
        }
        else
        {
            Debug.Log($"{_stunTime}, {_AttackDelayTime}");
        }

    }

    protected virtual void IdleEnemy()
    {

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
        yield return new WaitForSeconds(DelayTIme);
        //_rigid.velocity = new Vector3(0, 0, 0);
        _playerStat = PlayerAttackManager.Instance.playerStat;

        if (_superArrmor == false)
        {
            _stunTime = stuntime;
            StopAllCoroutines();
            Vector3 force = (transform.position - Player.transform.position).normalized;
            int Grabing = Grab ? -1 : 1;

            _rigid.AddForce(force * 2, ForceMode.VelocityChange);//force.x * NuckBack.x, force.y * NuckBack.y, force.z * NuckBack.z) * Grabing, ForceMode.Impulse);

        }

        HP -= ATK;
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
        //Destroy(this.gameObject);
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
}
