using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    [SerializeField] PlayerStatues playerStat;

    [SerializeField] List<CinemachineVirtualCamera> _cinemachine = new List<CinemachineVirtualCamera>();
    CinemachineVirtualCamera _currentCamera;
    GameObject[] _cin;

    public CinemachineVirtualCamera CurrentCam
    {
        get => _currentCamera;
    }

    public void CiemanchineChange(string a)
    {
        for(int i =0; i< _cinemachine.Count; i++)
        {
            if (_cinemachine[i].gameObject.name == a)
            {
                _cinemachine[i].Priority = 10;
                _currentCamera = _cinemachine[i];
                //break;
            }
            else
            {
                _cinemachine[i].Priority = 0;
            }
        }
    }

    public void PlayerViewChange()
    {
        for(int i =0; _cinemachine.Count > i; i++)
        {
            if (_cinemachine[i].gameObject.name == "PlayerView")
            {
                _cinemachine[i].Priority = 10;
                _currentCamera = _cinemachine[i];
                //break;
            }
            else
            {
                _cinemachine[i].Priority = 0;
            }
        }
    }

    private void Start()
    {
        _cin = GameObject.FindGameObjectsWithTag("VirtucalCamera");
        for(int i =0; i < _cin.Length; i++)
        {
            _cinemachine.Add(_cin[i].GetComponent<CinemachineVirtualCamera>());
        }
    }

}
