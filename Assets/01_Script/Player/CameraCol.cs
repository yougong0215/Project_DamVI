using Cinemachine;
using UnityEngine;

public class CameraCol : MonoBehaviour
{

    [SerializeField] CinemachineVirtualCamera _vcam;
    [SerializeField] float _originrayX;
    [SerializeField] float _originrayY;
    [SerializeField] float _sense = 1;



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

    private void Awake()
    {
        //_vaim = _vcam.GetCinemachineComponent<Cinemachine3rdPersonFollow>();
        _originrayX = 50;
    }


    private void LateUpdate()
    {

        //x = transform.position.x - _vaim.transform.position.x;
        //y = transform.position.y - _vaim.transform.position.y;
        //z = transform.position.z - _vaim.transform.position.z;

        //Leng = Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2) + Mathf.Pow(z, 2));


        transform.position = Player.position + new Vector3(0.000000000000001f, 1.4f, 0);
        if (PlayerAttackManager.Instance.PlayerP == PlayerPripoty.none || PlayerAttackManager.Instance.PlayerP == PlayerPripoty.Move)
        {
            CameraAltitude();
        }
        



    }

    void CameraAltitude()
    {


        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.localEulerAngles = new Vector3(_originrayX, _originrayY, 0);


        _originrayY += Input.GetAxis("Mouse X") * _sense;
        _originrayX += Input.GetAxisRaw("Mouse Y") * _sense;

        _originrayX = Mathf.Clamp(_originrayX, -60, 30);


    }



}

