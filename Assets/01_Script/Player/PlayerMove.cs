using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float h, v;
    Vector3 dir;
    Vector3 dirs;
    float _originrayX, _originrayY;
    float angle;
    float cameraAngle;

    int _dogedCount = 2;

    [Header("상태")]
    [SerializeField] bool isDoged = false;
    [SerializeField] bool isRun = false;

    [Header("카메라 혹은 락걸때")]
    [SerializeField] Transform LookObject;
    [SerializeField] Transform ArrowLook;
    Vector2 _direction;

    [Header("이동 값")]
    [SerializeField] float _moveSpeed = 5f;

    [Header("강력한 값")]
    [SerializeField] float _inpuseMoveSpeed = 5;

    [Header("기타 상태 확인용 컴포넌트")]
    [SerializeField] Animator _ani;
    [SerializeField] Rigidbody _rigid;
    float _sense = 0.5f;

    private void OnEnable()
    {
        isDoged = false;
       _rigid = GetComponent<Rigidbody>();
       _ani = GetComponent<Animator>();
    }


    private void Update()
    {


        PrimaryCamSet();
        MoveDir();

        DogedUse();
        // 입력 X
        if (dir == Vector3.zero || _direction == Vector2.zero)
        {
            _ani.SetBool("Move", false);
            _ani.SetBool("Run", false);
            return;
        }


        if (Input.GetKeyDown(KeyCode.LeftControl))
            isRun = isRun ? false : true;

        // 구르기 같은 순간 이동



        if(dir != Vector3.zero && PlayerAttackManager.Instance.playerpri != PlayerPripoty.Fight)
        {
            Move(_moveSpeed);
        }


        // 만약에 활을 쓴다면.. ( 근데 총 때문에 다시 쓸지도 )
        if (PlayerAttackManager.Instance.PlayerP == PlayerPripoty.aiming && ArrowLook != null)
        {
            ZoomMove();
        }
    }

    /// <summary>
    /// 최초로 실행 되어야 되는 친구
    /// </summary>
    private void PrimaryCamSet()
    {
        dirs = LookObject.transform.localRotation * Vector3.forward;
        angle = Vector2.SignedAngle(new Vector2(Mathf.Cos(0 * Mathf.Deg2Rad), Mathf.Sin(0 * Mathf.Deg2Rad)), _direction);
        dirs.y = 0.0f;
        dirs.Normalize();


        cameraAngle = Vector3.SignedAngle(Vector3.forward, dirs, Vector3.up);
    }


    /// <summary>
    /// 구르기 했을때
    /// </summary>
    private void DogedUse()
    {
        if (isDoged == false && Input.GetMouseButtonDown(1) && _dogedCount > 0)
        {
            StartCoroutine(Doged());
            StartCoroutine(DogedTransler());
            dirs.y = 0;
            Vector3 direction = dirs;

            var quaternion = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
            Vector3 newDirection = quaternion * direction;
            newDirection.Normalize();
            if (_direction == Vector2.zero)
            {
                newDirection *= -1;
            }

            Debug.Log(newDirection * _inpuseMoveSpeed);




            _rigid.AddForce(newDirection * _inpuseMoveSpeed * 2, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// 줌
    /// </summary>
    private void ZoomMove()
    {
        //ArrowLoock.SetParent(null);

        transform.localEulerAngles = new Vector3(0, _originrayY, 0);
        ArrowLook.localEulerAngles = new Vector3(_originrayX, 0, 0);


        _originrayY += Input.GetAxis("Mouse X") * _sense;
        _originrayX += Input.GetAxisRaw("Mouse Y") * _sense;

        _originrayX = Mathf.Clamp(_originrayX, -10, 10);


        if (dir == Vector3.zero)
        {
            //_ani.SetBool("Walk", false);
            return;
        }
        transform.Translate(dir * Time.deltaTime * _moveSpeed * 0.05f);
    }

    /// <summary>
    /// 또다른 입력값설정
    /// </summary>
    private void MoveDir()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        dir = new Vector3(h, 0, v);

        _direction = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
        {
            _direction += new Vector2(Mathf.Cos(0 * Mathf.Deg2Rad), Mathf.Sin(0 * Mathf.Deg2Rad));
        }
        if (Input.GetKey(KeyCode.A))
        {
            _direction += new Vector2(Mathf.Cos(-90 * Mathf.Deg2Rad), Mathf.Sin(-90 * Mathf.Deg2Rad));
        }
        if (Input.GetKey(KeyCode.S))
        {
            _direction += new Vector2(Mathf.Cos(180 * Mathf.Deg2Rad), Mathf.Sin(180 * Mathf.Deg2Rad));
        }
        if (Input.GetKey(KeyCode.D))
        {
            _direction += new Vector2(Mathf.Cos(90 * Mathf.Deg2Rad), Mathf.Sin(90 * Mathf.Deg2Rad));
        }
    }

    /// <summary>
    /// 달리기 : 이거 나중에 Move랑 합칠수 있음
    /// </summary>
    private void Move(float t)
    {


        _originrayY = transform.localEulerAngles.y;

        _ani.SetBool("Move", true);
        if (_direction != Vector2.zero)
        {


            if (isRun == true)
            {
                NormalMove(0.5f);

                _ani.SetBool("Run", false);
                PlayerAttackManager.Instance.PlayerP = PlayerPripoty.Move;
            }
            else if (isRun == false)
            {
                NormalMove(1);
                _ani.SetBool("Run", true);
                PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;
            }
        }
        PlayerAttackManager.Instance.SetDelayZero();
    }

    void NormalMove(float speed)
    {

        transform.rotation = Quaternion.Euler(Vector3.up * (cameraAngle + angle));

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(a * Vector3.forward), Time.deltaTime * 3);
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed * speed);
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rigid.AddForce(Vector3.up * _inpuseMoveSpeed, ForceMode.VelocityChange);
        }
    }


    public Vector3 GetDirs()
    {
        return LookObject.transform.localRotation * Vector3.forward;
    }
    public Vector2 GetDirecction()
    {
        return _direction;
    }

    private IEnumerator Doged()
    {
        isDoged = true;
        _dogedCount--;
        _ani.SetBool("Doged", true);
        yield return null;
        _ani.SetBool("Doged", false);
        StopCoroutine(DogedCountUp());
        StartCoroutine(DogedCountUp());
        yield return new WaitForSeconds(0.85f);
        isDoged = false;
    }


    IEnumerator DogedCountUp()
    {
        yield return new WaitForSeconds(2);
        _dogedCount = 2;
    }

    private IEnumerator DogedTransler()
    {
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.doged;
        yield return new WaitForSeconds(0.4f);
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;
        _rigid.velocity = new Vector3(0, 0, 0);
    }
}
