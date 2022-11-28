using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AIFacta : EnemyBase, IEnemyDetection
{
    [SerializeField] UnityEvent Clear;

    [SerializeField] public int Pase = 1;

    bool move = false;

    [SerializeField] public int _maxAttackCount = 0;
    [SerializeField] public int _nowAttackCount = 0;

    [SerializeField] public bool Attacking = false;


    [Header("근접")]
    [SerializeField] public float _MeleeLength = 10;
    [SerializeField] public PoolAble BaseBullet;
    [SerializeField] public Transform BulletPos;

    [Header("원거리")]
    [SerializeField] public BossMiddleWeapon Left;
    [SerializeField] public BossMiddleWeapon Right;

    public override IEnumerator DamagedForPlayer(int ATK, float stuntime, Vector3 NuckBack, bool Grab, float DelayTIme)
    {
        yield return null;
        if (Barrier >= 0 && BarrierBreaking == false)
        {
            Barrier -= ATK;
            //Vector3 force = (transform.position - Player.transform.position).normalized;

            //_rigid.AddForce(force * 0.5f * _fiber, ForceMode.VelocityChange);

        }
        else
        {
            BarrierBreaking = true;
            HP -= ATK;
            yield return new WaitForSeconds(0.3f);
            _rigid.velocity = Vector3.zero;
        }
    }
    protected override int ReviveEvent()
    {
        int ReviveHP = (int)MaxHP;
        _reviveCount--;
        MaxHP = MaxHP * 2;
        Barrier = (int)MaxHP * 2;
        HP = MaxHP;
        HPUI.color = Color.green;
        Pase = 2;
        return ReviveHP;
    }
    private void LateUpdate()
    {

        if (move == true)
        {
            _nav.SetDestination(Player.position);
            _nav.stoppingDistance = _MeleeLength;
        }
        Name.text = $"{nameing}";
        HPUI.fillAmount = (HP) / (MaxHP);
    }


    protected override void EnemyDetection()
    {
        transform.rotation = Quaternion.LookRotation(Player.position - transform.position);

        if (_maxAttackCount >= _nowAttackCount)
        {
            if (_AttackDelayTime <= 0 && Attacking == false)
            {
                AttackBase();
            }
        }
        else
        {
            if (BarrierBreak == true)
            {
                Barrier += 10 * Time.deltaTime;
            }
            else
            {
                Barrier += 2 * Time.deltaTime;
            }
        }
        if (MaxBarrier <= Barrier)
        {
            Barrier = MaxBarrier;
            BarrierBreaking = false;
        }
    }

    public void CheckAttack()
    {
        if (_nowAttackCount >= _maxAttackCount)
        {
            _AttackDelayTime = 5;
            _nowAttackCount = 0;
        }
    }

    public void AttackBase()
    {
        Attacking = true;
        switch (Pase)
        {
            case 1:
                Pase1();
                break;
            case 2:
                Pase2();
                break;
        }
    }

    void Pase1()
    {
        _maxAttackCount = 5;
        int a = (Random.Range(0, 30) + Random.Range(0, 30) + Random.Range(0, 30)) % 3;
        switch (a)
        {
            case 0:
                StartCoroutine(Pase1ShortAttack());
                break;
            case 1:
                StartCoroutine(Pase1MiddleShootUpSide());
                break;
            case 2:
                StartCoroutine(Pase1WonShootWhillWhill());
                break;
        }
    }

    void Pase2()
    {
        _maxAttackCount = 5;
        int a = (Random.Range(0, 30) + Random.Range(0, 30) + Random.Range(0, 30)) % 3;
        switch (a)
        {
            case 0:
                StartCoroutine(Pase2ShortAttack());
                break;
            case 1:
                StartCoroutine(Pase2MiddleShootUpSide());
                break;
            case 2:
                StartCoroutine(Pase2WonShootWhillWhill());
                break;
        }
    }

    /// <summary>
    /// 샷건
    /// </summary>
    /// <returns></returns>
    /// 
    IEnumerator Pase1ShortAttack()
    {
        yield return new WaitForSeconds(1f);
        move = true;
        _ani.SetTrigger("Short");
        //yield return new WaitUntil(() => Vector3.Distance(Player.position, transform.position) < _MeleeLength);
        _nav.stoppingDistance = 10000;
        //_rigid.velocity = Vector3.zero;

        for (int i = 0; i < 5; i++)
        {
            yield return new WaitForSeconds(0.1f);
            _ani.SetTrigger("ShortAttack");
            ShootBullet(5, 0, 0);
        }
        yield return new WaitForSeconds(1f);
        Attacking = false;
        CheckAttack();
        _AttackDelayTime = 3f;
        _nowAttackCount++;
    }
    IEnumerator Pase2ShortAttack()
    {
        yield return new WaitForSeconds(1f);
        move = true;
        _ani.SetTrigger("Short");
       // yield return new WaitUntil(() => Vector3.Distance(Player.position, transform.position) < _MeleeLength);
        _nav.stoppingDistance = 10000;
        //_rigid.velocity = Vector3.zero;
        move = false;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                yield return null;
                ShootBullet(ATK, i, j);
            }
        }
        _ani.SetTrigger("ShortAttack");
        yield return new WaitForSeconds(0.1f);


        Attacking = false;
        CheckAttack();
        _AttackDelayTime = 3f;
        _nowAttackCount++;
    }

    /// <summary>
    /// 위에서 내리꽃은 다음 돌아가면서 쏘기
    /// </summary>
    /// <returns></returns>
    IEnumerator Pase1MiddleShootUpSide()
    {
        _ani.SetTrigger("Middle");
        _ani.SetTrigger("ShortAttack");
        Left.StopWhill(new Vector3(1.4f, 1.4f), transform);
        Right.StopWhill(new Vector3(-1.4f, 1.4f), transform);
        yield return new WaitForSeconds(1f);
        //Left.PosReset(new Vector3(1.4f, 0));
        //Right.PosReset(new Vector3(1.4f, 0));
        Right.myWhill();
        Left.myWhill();


        for (int i = 0; i < 10; i++)
        {

            yield return new WaitForSeconds(0.5f);
            Left.Fire(ATK, 15);
            Right.Fire(ATK, 15);
        }
        yield return new WaitForSeconds(1f);
        Right.StopWhill(new Vector3(1.4f, 1, 0), transform);
        Left.StopWhill(new Vector3(-1.4f, 1, 0), transform);
        yield return new WaitForSeconds(1f);
        _AttackDelayTime = 1;
        _nowAttackCount++;
        CheckAttack();
        Attacking = false;
    }
    IEnumerator Pase2MiddleShootUpSide()
    {
        _ani.SetTrigger("Middle");
        _ani.SetTrigger("ShortAttack");
        Left.StopWhill(new Vector3(1.4f, 1.4f), transform);
        Right.StopWhill(new Vector3(-1.4f, 1.4f), transform);
        yield return new WaitForSeconds(1f);
        Left.ShootUpSide();
        yield return new WaitForSeconds(1.2f);
        Right.ShootUpSide();
        yield return new WaitForSeconds(1f);
        //Left.PosReset(new Vector3(1.4f, 0));
        //Right.PosReset(new Vector3(1.4f, 0));
        Right.myWhill();
        Left.myWhill();


        for (int i = 0; i < 20; i++)
        {

            yield return new WaitForSeconds(0.1f);
            Left.Fire(ATK, 15);
            Right.Fire(ATK, 15);
        }
        yield return new WaitForSeconds(1f);
        Right.StopWhill(new Vector3(1.4f, 1, 0), transform);
        Left.StopWhill(new Vector3(-1.4f, 1, 0), transform);
        _AttackDelayTime = 1;
        _nowAttackCount++;
        CheckAttack();
        Attacking = false;
    }

    IEnumerator Pase1WonShootWhillWhill()
    {
        _ani.SetTrigger("Whill");
        Attacking = true;
        _ani.SetTrigger("ShortAttack");
        Left.StopWhill(new Vector3(1.4f, 1.4f), transform);
        Right.StopWhill(new Vector3(-1.4f, 1.4f), transform);

        yield return new WaitForSeconds(3f);

        Left.ShootVelodown(new Vector3(1.4f, 6, 0), 2f, transform);
        Right.ShootVelodown(new Vector3(-1.4f, 6, 0), 2, transform);
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(0.15f);
            Left.Fire(ATK, 5);
            Right.Fire(ATK, 5);
        }
        yield return new WaitForSeconds(3f);
        Left.ShootWhill(new Vector3(3, 1, 3));
        Right.ShootWhill(new Vector3(-3, 1, -3));
        for (int i = 0; i < 50; i++)
        {
            yield return new WaitForSeconds(0.04f);
            Left.Fire(ATK, 5);
            Right.Fire(ATK, 5);
        }
        yield return new WaitForSeconds(3f);
        _AttackDelayTime = 1;
        CheckAttack();
        _nowAttackCount++;
        Attacking = false;
    }
    IEnumerator Pase2WonShootWhillWhill()
    {
        _ani.SetTrigger("ShortAttack");
        _ani.SetTrigger("Whill");
        Attacking = true;

        Left.StopWhill(new Vector3(1.4f, 1.4f), transform);
        Right.StopWhill(new Vector3(-1.4f, 1.4f), transform);

        yield return new WaitForSeconds(1f);

        Left.ShootVelodown(new Vector3(1.4f, 6, 0), 2f, transform);
        Right.ShootVelodown(new Vector3(-1.4f, 6, 0), 2, transform);
        for (int i = 0; i < 40; i++)
        {
            yield return new WaitForSeconds(0.05f);
            Left.Fire(ATK, 15);
            Right.Fire(ATK, 15);
        }
        yield return new WaitForSeconds(2f);
        Left.ShootingWhill(new Vector3(5, 1, 5));
        Right.ShootingWhill(new Vector3(-5, 1, -5));
        for (int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.02f);
            Left.Fire(ATK, 15);
            Right.Fire(ATK, 15);
        }
        yield return new WaitForSeconds(1f);
        _AttackDelayTime = 1;
        CheckAttack();
        _nowAttackCount++;
        Attacking = false;
    }


    public void ShootBullet(int attack, float x = 0, float y = 0)
    {
        PoolAble a = PoolManager.Instance.Pop(BaseBullet.name);
        a.transform.position = BulletPos.position;
        a.transform.rotation = Quaternion.LookRotation(Player.position);
        a.transform.rotation = Quaternion.Euler(transform.localEulerAngles + new Vector3(x, y, 0));
        a.GetComponent<WizardBullet>().SetDamage(attack, transform);

    }


    public void AttackCoolBase()
    {

    }
}
