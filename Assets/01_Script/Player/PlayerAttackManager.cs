using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager : Singleton<PlayerAttackManager>
{
    [SerializeField] PlayerStatues playerStat;
    [SerializeField] PlayerPripoty playerpri;

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
    Collider[] hit;
    void Start()
    {
        playerpri = PlayerPripoty.none;
        playerStat = PlayerStatues.Idle;
        hit = Physics.OverlapBox(transform.position, new Vector3(4, 4, 4), Quaternion.identity, 1 << (LayerMask.NameToLayer("InterectionObj")));
    }
    


    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            playerpri = PlayerPripoty.aiming;
            CameraManager.Instance.CiemanchineChange("ArrowView");
        }
        if(Input.GetMouseButtonUp(0))
        {
            playerpri = PlayerPripoty.none;
            CameraManager.Instance.PlayerViewChange();
        }
    }
    

}
