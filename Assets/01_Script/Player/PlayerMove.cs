using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    #region ������
    float h, v;
    Vector3 dir;
    Vector3 dirs;
    float _originrayX, _originrayY;
    float angle;
    float cameraAngle;

    Coroutine co;

    int _dogedCount = 0;

    [Header("����")]
    [SerializeField] bool isDoged = false;
    [SerializeField] bool isRun = false;

    [Header("ī�޶� Ȥ�� ���ɶ�")]
    [SerializeField] public Transform LookObject;
    [SerializeField] public Transform ArrowLook;
    [SerializeField] Transform _front;
    Vector2 _direction;

    [Header("�̵� ��")]
    [SerializeField] float _moveSpeed = 5f;
    [SerializeField] float _sense = 10f;

    [Header("������ ��")]
    [SerializeField] float _inpuseMoveSpeed = 5;

    [Header("��Ÿ ���� Ȯ�ο� ������Ʈ")]
    [SerializeField] Animator _ani;
    [SerializeField] Rigidbody _rigid;
    #endregion


    private void OnEnable()
    {
        isDoged = false;
       _rigid = GetComponent<Rigidbody>();
       _ani = GetComponent<Animator>();

        _dogedCount = ShopState.Instance.Willadd;
    }


    private void Update()
    {
        Debug.Log(ShopState.Instance.Willadd + " : " + _dogedCount);
        if(PlayerAttackManager.Instance.PlayerP != PlayerPripoty.hit 
            && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.die
            && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.Clear)
        {
            //Debug.Log(_dogedCount);
            PrimaryCamSet();
            MoveDir();

            DogedUse();
            // �Է� X


            if (PlayerAttackManager.Instance.PlayerP == PlayerPripoty.aiming && ArrowLook != null
                && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.weaponAttack)
            {
                ZoomMove();
                return;
            }
            if (dir == Vector3.zero || _direction == Vector2.zero)
            {
                _ani.SetBool("Move", false);
                return;
            }


            if (Input.GetKeyDown(KeyCode.LeftControl))
                isRun = isRun ? false : true;

            // ������ ���� ���� �̵�


            //Debug.Log(_direction);

            if (dir != Vector3.zero && PlayerAttackManager.Instance.playerpri != PlayerPripoty.Fight
                && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.aiming && _ani.GetInteger("Attack") != 1
                && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.weaponAttack)
            {
                Move(_moveSpeed);
            }

        }


        // ���࿡ Ȱ�� ���ٸ�.. ( �ٵ� �� ������ �ٽ� ������ )
    }

    /// <summary>
    /// ���ʷ� ���� �Ǿ�� �Ǵ� ģ��
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
    /// ������ ������
    /// </summary>
    private void DogedUse()
    {
        if (isDoged == false && Input.GetKeyDown(KeyCode.LeftShift) && _dogedCount > 0 
            && PlayerAttackManager.Instance.playerpri != PlayerPripoty.Fight 
            && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.aiming
            && PlayerAttackManager.Instance.PlayerP != PlayerPripoty.weaponAttack)
        {

            StartCoroutine(Doged());
            StartCoroutine(DogedTransler());

            Vector3 direction = _front.transform.position - transform.position; // < x�����

            //direction.Normalize();
            //Quaternion quaternion = Quaternion.Euler(0, transform.localEulerAngles.y, 0);
            Vector3 newDirection =  direction;
            newDirection.Normalize();
            //Vector3 newDirection = new Vector3(Mathf.Cos(transform.localEulerAngles.y), 0, Mathf.Sin(transform.localEulerAngles.y));
            if (_direction == Vector2.zero)
            {
                newDirection *= -1;
            }

            //Debug.Log(newDirection);




            _rigid.AddForce(newDirection * _inpuseMoveSpeed, ForceMode.VelocityChange);
        }
    }

    /// <summary>
    /// ��
    /// </summary>
    private void ZoomMove()
    {
        //ArrowLoock.SetParent(null);

        transform.localEulerAngles = new Vector3(0, _originrayY, 0);
        ArrowLook.localEulerAngles = new Vector3(_originrayX, 0, 0);


        _originrayY += Input.GetAxisRaw("Mouse X") * _sense / 5;
        _originrayX -= Input.GetAxisRaw("Mouse Y") * _sense / 20;

        _originrayX = Mathf.Clamp(_originrayX, -10, 10);


        _ani.SetBool("Move", false);

        if (dir == Vector3.zero)
        {
            //_ani.SetBool("Walk", false);
            return;
        }
        transform.Translate(dir * Time.deltaTime * _moveSpeed * 0.05f);
    }

    /// <summary>
    /// �Ǵٸ� �Է°�����
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
    /// �޸��� : �̰� ���߿� Move�� ��ĥ�� ����
    /// </summary>
    private void Move(float t)
    {


        _originrayY = transform.localEulerAngles.y;

        _ani.SetBool("Move", true);
        if (_direction != Vector2.zero && isDoged == false)
        {


            if (isRun == true)
            {
                NormalMove(0.3f);

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
    }

    void NormalMove(float speed)
    {

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.up * (cameraAngle + angle)), Time.deltaTime * 7);

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

    public Quaternion GetCameraAngel()
    {
        return Quaternion.Euler(Vector3.up * (cameraAngle + angle));
    }

    private IEnumerator Doged()
    {
        isDoged = true;
        _dogedCount--;

        _ani.SetBool("Doged", true);

        yield return null;

        if(co != null)
        {
            StopCoroutine(co);
        }

        co = StartCoroutine(DogedCountUp());
        yield return new WaitForSeconds(0.55f);
        _ani.SetBool("Doged", false);
        yield return new WaitForSeconds(0.3f);

        isDoged = false;
    }


    IEnumerator DogedCountUp()
    {
        yield return new WaitForSeconds(2);
        _dogedCount = ShopState.Instance.Willadd;
    }
    
    public Vector3 ShootDir(Vector3 vec)
    {
        return (transform.position + new Vector3(0.1f,0,0)) - LookObject.position;
    }
    private IEnumerator DogedTransler()
    {
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.doged;
        yield return new WaitForSeconds(0.5f);
        PlayerAttackManager.Instance.PlayerP = PlayerPripoty.none;
        _rigid.velocity = new Vector3(0, 0, 0);
    }

    public CameraCollision CameraReturn()
    {
        return LookObject.GetComponent<CameraCollision>();
    }
}
