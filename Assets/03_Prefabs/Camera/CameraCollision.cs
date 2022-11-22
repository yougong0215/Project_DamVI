using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    // Made by GmSoft ( You Jae Hyun )


    [Header("진짜 카메라와 위치용 가짜 카메라 셋팅")]
    [SerializeField] Transform _vcam;
    [SerializeField] Transform _vcamFake;
    [SerializeField] Transform _aiming;
    [SerializeField] Transform _aimingFake;

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

    [SerializeField] LayerMask layer;
    bool _setPos = false;

    int L = 1, U = 1;

    RaycastHit hit;
    Vector3 _hitVec;
    Vector3 OriginAimingpos;

    float shakeDuration = 0;
    float shakeAmount = 0.05f;
    float decreaseFactor = 1f;

    /// <summary>
    /// 카메라 쉐이킹
    /// </summary>
    /// <param name="a"></param> 지속시간
    /// <param name="b"></param> 떨림도
    /// <param name="c"></param> 지연 %
    public void shaking(float a = 0, float b = 0.05f, float c = 1f)
    {
        shakeDuration = a;
        shakeAmount = b;
        decreaseFactor = c;

    }

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
        shake();
        Aiming();
    }

    void Aiming()
    {
        if(PlayerAttackManager.Instance.PlayerP == PlayerPripoty.aiming)
        {
            _originrayX = Player.localEulerAngles.x;
            _originrayY = Player.localEulerAngles.y;
        }
    }

    void CameraAltitude()
    {

        // 플레이어 위치는 보통 발바닥임 + 1.4f
        transform.position = Player.position + new Vector3(0.000000000000001f, 1.4f, 0);

        // 원래 있어야 할 위치로 이동하는 캠
        _hitVec = _vcamFake.transform.position;
        _aiming.transform.position = _aimingFake.transform.position;

        // 카메라 반전
        L = zxXxz ? -1 : 1;
        U = zyYyz ? 1 : -1;


        if (_setPos == false)
        {
            _hitVec = _vcamFake.transform.position;
        }

        if (Physics.Raycast(transform.position, (_vcamFake.transform.position - transform.position).normalized, out hit, CameraMaxDistance, layer))
        {
            _setPos = true;
            _hitVec = hit.point;//Vector3.Lerp(_vcamFake.transform.position, hit.point, Time.deltaTime * 1500f);

        }
        else if (Vector3.Distance(transform.position, _vcam.transform.position) > Vector3.Distance(transform.position, _vcamFake.transform.position))
        {
            _setPos = false;
            _hitVec = _vcamFake.transform.position;
        }

        _vcam.transform.position = _hitVec;

        transform.localEulerAngles = new Vector3(_originrayX, _originrayY, 0);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _originrayY += Input.GetAxis("Mouse X") * _sense * L;
        _originrayX += Input.GetAxisRaw("Mouse Y") * _sense * U;

        _originrayX = Mathf.Clamp(_originrayX, _bindCamMin, _bindCamMax);

        _setPos = false;
    }


    void shake()
    {
        if (shakeDuration > 0)
        {
            _vcam.transform.position = _hitVec + Random.insideUnitSphere * shakeAmount;
            _aiming.transform.position = _aiming.transform.position + Random.insideUnitSphere * shakeAmount;
            shakeDuration -= Time.deltaTime * decreaseFactor;
        }
    }

}

