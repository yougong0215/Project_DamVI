using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : MonoBehaviour
{
    [Header("�� �⺻����")]
    [SerializeField] int HP = 0;
    [SerializeField] int ATK = 0;
    [SerializeField] int _reviveCount = 0;
    [SerializeField] int _detectionLength = 0;
    [SerializeField] bool BossMonster = false;
    [SerializeField] bool _superArrmor = false;
    [SerializeField] float _stunTime = 0;
    [SerializeField] float _DelayTime = 0;

    [Header("���� ������÷��̾�")]
    [SerializeField] PlayerStatues _playerStat = PlayerStatues.Idle;

    [Header("�� ������Ƽ")]
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
    /// �������������� Ž���� ���� ���� �ڿ��� �ϻ찰���� �Ҳ��� �Ⱦ�����
    /// �������� ��쿣 �ʿ� ����
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
    /// ���⼭ ���� �ൿ�ϴ°� ����ɵ�
    /// </summary>
    protected virtual void EnemyDetection()
    {

    }



    /// <summary>
    /// ������ ������ �̰� �޾ƿ����
    /// ���� �������� �ҷ��;�
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
                    Debug.Log("����");
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
