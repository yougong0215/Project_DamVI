using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

    float _originrayX, _originrayY;

    [Header("카메라 혹은 락걸때")]
    [SerializeField] Transform LookObject;
    [SerializeField] Transform ArrowLoock;
    Vector2 _direction;

    [Header("이동 값")]
    [SerializeField] float _moveSpeed = 5f;

    [Header("강력한 값")]
    [SerializeField] float _inpuseMoveSpeed = 5;

    [Header("기타 상태 확인용 컴포넌트")]
    [SerializeField] 
    Rigidbody _rigid;
    float _sense = 0.5f;

    private void OnEnable()
    {
       _rigid = GetComponent<Rigidbody>();
    }


    private void Update()
    {
        if(PlayerAttackManager.Instance.PlayerP == PlayerPripoty.none || PlayerAttackManager.Instance.PlayerP == PlayerPripoty.Move)
        {
            Move();
        }
        if(PlayerAttackManager.Instance.PlayerP == PlayerPripoty.aiming)
        {
            ZoomMove();
        }
    }

    private void ZoomMove()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = new Vector3(h, 0, v);
        //ArrowLoock.SetParent(null);

        transform.localEulerAngles = new Vector3(0, _originrayY, 0);
        ArrowLoock.localEulerAngles = new Vector3(_originrayX, 0, 0);


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

    private void Move()
    {
        _originrayY = transform.localEulerAngles.y;
        //ArrowLoock.SetParent(transform);
        Vector3 dirs = LookObject.transform.localRotation * Vector3.forward;
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
        _direction.Normalize();
        
        if (_direction == Vector2.zero)
        {
            //_ani.SetBool("Walk", false);
            return;
        }
        else if (_direction != Vector2.zero)
        {
            //_ani.SetBool("Walk", true);
        }
        

        float angle = Vector2.SignedAngle(new Vector2(Mathf.Cos(0 * Mathf.Deg2Rad), Mathf.Sin(0 * Mathf.Deg2Rad)), _direction);
        dirs.y = 0.0f;
        dirs.Normalize();


        float cameraAngle = Vector3.SignedAngle(Vector3.forward, dirs, Vector3.up);
        transform.rotation = Quaternion.Euler(Vector3.up * (cameraAngle + angle));
        
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(a * Vector3.forward), Time.deltaTime * 3);
        transform.Translate(Vector3.forward * Time.deltaTime * _moveSpeed);
        if (Input.GetMouseButtonDown(1))
        {
            _rigid.AddForce(new Vector3(_direction.x, 0, _direction.y) * _inpuseMoveSpeed, ForceMode.Impulse);
        }
    }
}
