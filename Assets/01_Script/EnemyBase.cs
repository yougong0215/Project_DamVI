using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("�� �⺻����")]
    [SerializeField] protected int MaxHP = 0;
    [SerializeField] protected int HP = 0;
    [SerializeField] protected int Barrier = 0;
    [SerializeField] protected int ATK = 0;
    [SerializeField] protected int _reviveCount = 0;
    [SerializeField] protected int _detectionLength = 0;
    [SerializeField] protected bool BossMonster = false;
    [SerializeField] protected bool _superArrmor = false;
    [SerializeField] float _stunTime = 0;
    [SerializeField] protected float _AttackDelayTime = 0;

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
            if (Length <= _detectionLength)
            {

                MoveEnemy();

                if(Length <= 4)
                {
                    EnemyDetection();
                }

            }
            else
            {
                IdleEnemy();
                _nav.ResetPath();
                _nav.SetDestination(transform.position);
            }
        }
        else
        {
            Debug.Log($"{_stunTime}, {_AttackDelayTime}");
        }

    }


    protected virtual void MoveEnemy()
    {
        _nav.SetDestination(Player.position);
        _nav.stoppingDistance = 2;
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
    /// ������ ������ �̰� �޾ƿ����
    /// ���� �������� �ҷ��;�
    /// </summary>
    /// <param name="ATK"></param> 
    public virtual IEnumerator DamagedForPlayer(int ATK, float stuntime, Vector3 NuckBack, bool Grab, float DelayTIme)
    {
        yield return new WaitForSeconds(DelayTIme);

        if (_superArrmor == false)
        {
            _stunTime = stuntime;
            StopAllCoroutines();
            Vector3 force = (transform.position - Player.transform.position).normalized;
            int Grabing = Grab ? -1 : 1;

            _rigid.AddForce(force * 2, ForceMode.VelocityChange);//force.x * NuckBack.x, force.y * NuckBack.y, force.z * NuckBack.z) * Grabing, ForceMode.Impulse);

        }

        HP -= ATK;

        yield return new WaitForSeconds(0.3f);
        _rigid.velocity = Vector3.zero;
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
