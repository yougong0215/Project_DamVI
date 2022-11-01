using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackBase : StateMachineBehaviour
{
    enum AttackState
    {
        mello,
        Range
    }
    MonoBehaviour mono = new MonoBehaviour();



    [Header("���ݹ���")]
    [SerializeField] protected Vector3 size = new Vector3(1.5f, 1.5f, 1.5f);

    [Header("����Ʈ �ֱ�")]
    [SerializeField] protected List<GameObject> Effect = new List<GameObject>();


    [Header("�÷��̾� ���� ���ϱ�")]
    [SerializeField] protected PlayerStatues _state;

    [Header("��ܿý� true")]
    [SerializeField] protected bool Grabing;

    [Header("�˹� ����")]
    [SerializeField] protected Vector3 NuckBack = new Vector3(2,3,1);

    [Header("���� �ð�")]
    [SerializeField] protected float stun = 0.2f;

    [Header("�ð� ������")]
    [SerializeField] protected float Delay = 0.2f;

    [Header("�������� ���ŷ� ���� ����")]
    [SerializeField] AttackState state = AttackState.mello;

    bool isstart = false;

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
        mono = Player.GetComponent<MonoBehaviour>();
        animator.SetInteger("Attack", 0);

        isstart = false;

        Debug.Log(stateInfo);
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.Fight;
        //SetSize();
        SetStateAttack();
        OnDamageEffectStart(animator, stateInfo, layerIndex);
        ScanEnemys();

    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        for (int i = 0; hit.Length > i; i++)
        {
            if(state == AttackState.mello)
            {
                if (hit[i].gameObject.GetComponent<EnemyBase>())
                {
                    hit[i].GetComponent<EnemyBase>().DamagedCool(1, stun, NuckBack, Grabing, Delay);
                    PlayerAttackManager.Instance.SetDelayZero();
                    OnDamagedEnemyMelloAttack(animator, stateInfo, layerIndex, hit);
                }

            }
            else if (state == AttackState.Range && isstart == false)
            {
                isstart = true;
                mono.StartCoroutine(OndamagedEnemyRangeAttack(animator, stateInfo, layerIndex, Delay));
            }
        }
        PlayerAttackManager.Instance.SetDelayZero();
        OnDamageEffectHold(animator, stateInfo, layerIndex);
    }



    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;
        OnDamageEffectEnd(animator, stateInfo, layerIndex);
    }

    /// <summary>
    /// �������ϱ�
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
    /// size �� �����ֱ�
    /// </summary>
    public virtual void ScanEnemys()
    {

        float angle = Vector2.SignedAngle(new Vector2(Mathf.Cos(0 * Mathf.Deg2Rad), Mathf.Sin(0 * Mathf.Deg2Rad)), Player.GetComponent<PlayerMove>().GetDirecction());
        hit = Physics.OverlapBox(new Vector3(Player.position.x, Player.position.y + 1f, Player.position.z + 1f)
        , size, Quaternion.Euler(Vector3.up * (angle)), 1 << (LayerMask.NameToLayer("InterectionObj")));
    }

    public virtual IEnumerator OndamagedEnemyRangeAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, float delay)
    {
        yield return null;
    }

    public virtual void OnDamagedEnemyMelloAttack(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, Collider[] col)
    {

    }

    public virtual void OnDamageEffectStart(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public virtual void OnDamageEffectHold(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    public virtual void OnDamageEffectEnd(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
