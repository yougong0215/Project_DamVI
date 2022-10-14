using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBase : StateMachineBehaviour
{

    [Header("공격범위")]
    [SerializeField] protected Vector3 size = new Vector3(1.5f, 1.5f, 1.5f);

    [Header("이팩트 넣기")]
    [SerializeField] protected List<GameObject> Effect = new List<GameObject>();


    [Header("플레이어 상태 정하기")]
    [SerializeField] protected PlayerStatues _state;

    [Header("당겨올시 true")]
    [SerializeField] protected bool Grabing;

    [Header("넉백 방향")]
    [SerializeField] protected Vector3 NuckBack;

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
    Collider[] hit;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.Fight;
        //SetSize();
        SetStateAttack();
        OnDamageEffectStart();
        ScanEnemys();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; hit.Length > i; i++)
        {
            if (hit[i].gameObject.GetComponent<EnemyBase>())
            {
                hit[i].GetComponent<EnemyBase>().DamagedForPlayer(1, 0.2f, new Vector3(2,3,1), false);
                PlayerAttackManager.Instance.SetDelayZero();
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;
    }

    /// <summary>
    /// 유형정하기
    /// </summary>
    public virtual void SetStateAttack()
    {
        PlayerAttackManager.Instance.PlayerS = _state;
    }

    public virtual void SetSize()
    {
        size = new Vector3(1.5f, 1.5f, 1.5f);
    }

    /// <summary>
    /// size 꼭 정해주기
    /// </summary>
    public virtual void ScanEnemys()
    {
        hit = Physics.OverlapBox(new Vector3(Player.position.x, Player.position.y + 1f, Player.position.z + 1f)
        , size, Quaternion.identity, 1 << (LayerMask.NameToLayer("InterectionObj")));
    }

    public virtual void OnDamageEffectStart()
    {

    }

    public virtual void OnDamageEffectHold()
    {

    }

    public virtual void OnDamageEffectEnd()
    {

    }
}
