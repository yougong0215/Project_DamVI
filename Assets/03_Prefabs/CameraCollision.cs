using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    // Made by GmSoft ( You Jae Hyun )


    [Header("진짜 카메라와 위치용 가짜 카메라 셋팅")]
    [SerializeField] Transform _vcam;
    [SerializeField] Transform _vcamFake;

    [Header("카메라 정보")]
    [SerializeField] float _originrayX;
    [SerializeField] float _originrayY;
    [SerializeField] float _sense = 3;

    [Header("카메라 조정 ( 초기값 : -60, 30 )")]
    [SerializeField] float _bindCamMin = -60;
    [SerializeField] float _bindCamMax = 30;
    [SerializeField] float CameraMaxDistance = 5f;

    [Header("반전")]
    [SerializeField] bool zxXxz = false;
    [SerializeField] bool zyYyz = false;

    bool _setPos = false;

    int L = 1, U = 1;



    RaycastHit hit;
    Vector3 _hitVec;


    private Transform _player;
    public Transform Player
    {
        get
        {
            if (_player == null)
            {
                _player = GameObject.Find("Player").GetComponent<Transform>();
            }
            return _player;
        }
    }


    private void LateUpdate()
    {
        CameraAltitude();
    }

    void CameraAltitude()
    {

        // 플레이어 위치는 보통 발바닥임 + 1.4f
        transform.position = Player.position + new Vector3(0.000000000000001f, 1.4f, 0);

        // 원래 있어야 할 위치로 이동하는 캠
        _hitVec = _vcamFake.transform.position;

        // 카메라 반전
        L = zxXxz ? -1 : 1;
        U = zyYyz ? 1 : -1;


        if (_setPos == false)
        {
            _hitVec = _vcamFake.transform.position;
        }

        if (Physics.Raycast(transform.position, (_vcamFake.transform.position - transform.position).normalized, out hit, CameraMaxDistance, 0))
        {
            _setPos = true;
            _vcam.transform.position = Vector3.Lerp(_vcamFake.transform.position, hit.point, Time.deltaTime * 500f);
            _hitVec = hit.point;

        }
        else
        {
            _vcam.transform.position = Vector3.Lerp(_hitVec, _vcam.transform.position, Time.deltaTime * 1500f);
            //Debug.DrawRay(transform.position, MainCamera.transform.position, Color.red);        
        }


        transform.localEulerAngles = new Vector3(_originrayX, _originrayY, 0);


        _originrayY += Input.GetAxis("Mouse X") * _sense * L;
        _originrayX += Input.GetAxisRaw("Mouse Y") * _sense * U;

        _originrayX = Mathf.Clamp(_originrayX, _bindCamMin, _bindCamMax);

        _setPos = false;
    }



}

