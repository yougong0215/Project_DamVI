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

    bool isDoged = false;

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


        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        dir = new Vector3(h, 0, v);

        if (PlayerAttackManager.Instance.playerpri != PlayerPripoty.doged)
        {


            dirs = LookObject.transform.localRotation * Vector3.forward;
            angle = Vector2.SignedAngle(new Vector2(Mathf.Cos(0 * Mathf.Deg2Rad), Mathf.Sin(0 * Mathf.Deg2Rad)), _direction);
            dirs.y = 0.0f;
            dirs.Normalize();


            cameraAngle = Vector3.SignedAngle(Vector3.forward, dirs, Vector3.up);
        }
        else
        {
            //transform.rotation = Quaternion.Euler(Vector3.up * (cameraAngle));
        }

        if (dir == Vector3.zero)
        {
            _ani.SetBool("Move", false);
            _ani.SetInteger("Run", 0);
            return;
        }
        if (isDoged == false && Input.GetMouseButtonDown(1) && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.Fight && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.none)
        {
            StartCoroutine(Doged());
            StartCoroutine(DogedTransler());
            dirs.y = 0;
            Vector3 direction = dirs;

            var quaternion = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
            Vector3 newDirection = quaternion * direction;


            _rigid.AddForce(newDirection * _inpuseMoveSpeed, ForceMode.VelocityChange);
        }
        if (PlayerAttackManager.Instance.PlayerP == PlayerPripoty.none)
        {
            Move();
            Jump();
        }
        if(PlayerAttackManager.Instance.PlayerP == PlayerPripoty.Move)
        {
            Run();
            Jump();
        }

        if(PlayerAttackManager.Instance.PlayerP == PlayerPripoty.aiming && ArrowLook != null)
        {
            ZoomMove();
        }
        if (PlayerAttackManager.Instance.PlayerP == PlayerPripoty.Fight)
        {
            MoveDir();
            NormalMove(0.2f);
        }
    }

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

    private void MoveDir()
    {
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

    private IEnumerator Doged()
    {
        isDoged = true;
        yield return new WaitForSeconds(1.4f);
        isDoged = false;
    }
    private IEnumerator DogedTransler()
    {
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.doged;
        yield return new WaitForSeconds(0.4f);
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;
    }

    private void Run()
    {


        _originrayY = transform.localEulerAngles.y;
        //ArrowLoock.SetParent(transform);
        _direction = Vector2.zero;


        MoveDir();

        _direction.Normalize();

        if (_direction == Vector2.zero)
        {
            _ani.SetBool("Move", false);
            _ani.SetInteger("Run", 0);
            return;
        }
        else
        {
            _ani.SetInteger("Run", 1);
        }


        NormalMove(1);

        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.Move;
        PlayerAttackManager.Instance.SetDelayZero();
    }


    private void Move()
    {
        

        _originrayY = transform.localEulerAngles.y;
        //ArrowLoock.SetParent(transform);
        _direction = Vector2.zero;


        MoveDir();
        
        _direction.Normalize();
        
        if (_direction == Vector2.zero)
        {
            
            _ani.SetBool("Move", false);
            return;
        }

        NormalMove(0.5f);

        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;
        PlayerAttackManager.Instance.SetDelayZero();
    }


    void NormalMove(float speed)
    {
        _ani.SetBool("Move", true);

        transform.rotation = Quaternion.Euler(Vector3.up * (cameraAngle + angle));

        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(a * Vector3.forward), Time.deltaTime * 3);
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed * speed);
    }

    public Vector3 GetDirs()
    {
        return LookObject.transform.localRotation * Vector3.forward;
    }
    public Vector2 GetDirecction()
    {
        return _direction;
    }

    void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            _rigid.AddForce(new Vector3(0, 1, 0) * _inpuseMoveSpeed, ForceMode.VelocityChange);
        }
    }
}
