using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerAttackManager :  MonoBehaviour
{
    private static PlayerAttackManager m_Instance;
    public static PlayerAttackManager Instance
    {
        get
        {

            if (m_Instance == null)
            {
                m_Instance = (PlayerAttackManager)FindObjectOfType(typeof(PlayerAttackManager));
                if (m_Instance == null)
                {
                    var singletonObject = new GameObject();
                    m_Instance = singletonObject.AddComponent<PlayerAttackManager>();
                    singletonObject.name = typeof(PlayerAttackManager).ToString() + " (Singleton)";
                }
            }

            return m_Instance;
        }
    }


    [Header("Enum 들")]
    [SerializeField] public PlayerStatues playerStat;
    [SerializeField] public PlayerPripoty playerpri;

    [Header("에니메이션")]
    [SerializeField] public Animator _ani;

    [Header("적들")]
    [SerializeField] Collider[] hit;

    [Header("UI")]
    [SerializeField] GameObject _aimDraw;
    [SerializeField] GameObject _draw;

    [SerializeField] public BulletReload Bullet;


    [SerializeField] private bool _normalAttack = false;
    [SerializeField] private int _playerAttackValue;

    Coroutine co = null;
    public PlayerInteraction _inter;

    [SerializeField] StageClear _stage;
    


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

    public float CurrentClearTime = 0;
    public int CurrentScore = 0;

    public PlayerStatues PlayerS
    {
        get => playerStat;
        set
        {
            playerStat = value;
        }
    }
    public PlayerPripoty PlayerP
    {
        get => playerpri;
        set
        {
            playerpri = value;
        }
    }

    Vector3 OriginZoomUIvec;
    Vector3 OriginUIvec;
    void Start()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
        _ani = Player.GetComponent<Animator>();
        _inter = GetComponent<PlayerInteraction>();
        OriginZoomUIvec = _aimDraw.transform.position;
        OriginUIvec = _draw.transform.position;
        _aimDraw.GetComponent<RectTransform>().localPosition = new Vector3(10000, 10000, 10000);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerP != PlayerPripoty.Clear && PlayerP != PlayerPripoty.die)
        {
            CurrentClearTime += Time.deltaTime;

            Debug.Log(CurrentScore);

            if (Input.GetMouseButtonDown(1))
            {
                //LookEnemy();
            }

            if (Input.GetMouseButton(1) == false)
            {
                Attack();
            }
            else if (_normalAttack == false && Input.GetMouseButton(1))
            {
                Aimaing();
            }


            if (_inter.I_HP <= 0)
            {
                _ani.SetBool("Die", true);
                PlayerAttackManager.Instance.PlayerP = PlayerPripoty.die;
                _stage.OnDied();
            }

            if (Input.GetKeyDown(KeyCode.Q) && _inter.I_MP >= 20 && (playerpri == PlayerPripoty.Move || playerpri == PlayerPripoty.none || playerpri == PlayerPripoty.Fight))
            {
                _inter.UseMp(20);
                playerpri = PlayerPripoty.weaponAttack;
                _ani.SetBool("Weapon", true);
            }

            if (Input.GetMouseButtonUp(1) && playerpri == PlayerPripoty.aiming)
            {
                SetStateNone();
            }
        }
        else
        {
            _stage.OnDied();
        }
    }

    void Aimaing()
    {   
        SetStateAim();
        if (Input.GetMouseButtonDown(0))
        {
            _ani.SetTrigger("AimShoot");
        }
        if (Input.GetKeyDown(KeyCode.R) && _inter.I_MP >= 10)
        {
            _inter.UseMp(10);
            Bullet._bulletcount = ShopState.Instance.BulletAdd;
        }



    }


    void Attack()
    {
        if (Input.GetMouseButtonDown(0) && (playerpri == PlayerPripoty.Move || playerpri == PlayerPripoty.none || playerpri == PlayerPripoty.Fight))
        {
            _ani.SetInteger("Attack", 1);
        }



    }

    public void Corutines()
    {
        if (co != null)
            StopCoroutine(co);
        co = StartCoroutine(clearStat());
    }

    public void Scene()
    {
        SceneManager.LoadScene(1);
    }
    public void SetStateAim()
    {
        playerpri = PlayerPripoty.aiming;
        playerStat = PlayerStatues.bifurcationAttack1;
        Player.GetComponent<PlayerMove>().ArrowLook.GetComponent<CinemachineVirtualCamera>().Priority = 11;
        _ani.SetBool("Aiming", true);
        _aimDraw.GetComponent<RectTransform>().position = OriginZoomUIvec;
        _draw.GetComponent<RectTransform>().position = new Vector3(10000, 10000, 10000);
    }

    public void SetStateNone()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
        Player.GetComponent<PlayerMove>().ArrowLook.GetComponent<CinemachineVirtualCamera>().Priority = 9;

        _ani.SetBool("Aiming", false);
        _ani.SetInteger("Attack", 0);
        _ani.SetBool("Weapon", false);
        _draw.GetComponent<RectTransform>().position = OriginUIvec;
        _aimDraw.GetComponent<RectTransform>().localPosition = new Vector3(10000, 10000, 10000);
    }

    IEnumerator clearStat()
    {
        if(_ani.GetBool("Aiming")== false)
            Player.GetComponent<PlayerMove>().ArrowLook.GetComponent<CinemachineVirtualCamera>().Priority = 9;

        _normalAttack = true;
        yield return new WaitForSeconds(0.1f);

        SetStateNone();
        _normalAttack = false;
    }

    public void Rectmove(float f = 0.9f)
    {
        _aimDraw.GetComponent<RectTransform>().DOScale(1.05f, 0.05f).SetEase(Ease.InQuint).OnComplete(
            ()=> _aimDraw.GetComponent<RectTransform>().DOScale(1, 0.05f).SetEase(Ease.InQuint)
        );
        _draw.GetComponent<RectTransform>().DOScale(1.05f, 0.05f).SetEase(Ease.InQuint).OnComplete(
            () => _draw.GetComponent<RectTransform>().DOScale(1, 0.05f).SetEase(Ease.InQuint)
        );
    }

    //protected void LookEnemy()
    //{
    //    if (_inter.DistannsEnemy())
    //    {
    //        Vector3 enemy = (_inter.DistannsEnemy().position - Player.transform.position).normalized;
    //        Debug.Log(enemy);
    //        Player.rotation = Quaternion.LookRotation(enemy);
    //        Player.localEulerAngles = new Vector3(0, Player.localEulerAngles.y, 0);
    //        //Player.rotation = Quaternion.Euler(0, Player.localEulerAngles.y, 0);
    //    }
    //    else
    //    {
    //        Player.rotation = Player.GetComponent<PlayerMove>().GetCameraAngel();
    //    }
    //}
}
