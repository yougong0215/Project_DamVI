using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI Hp;
    [SerializeField] TextMeshProUGUI Mp;
    [SerializeField] TextMeshProUGUI ZoomHp;
    [SerializeField] TextMeshProUGUI ZoomMp;

    PlayerInteraction _inter;
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

    void Start()
    {
        _inter = Player.GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        Hp.text = $" HP  {_inter.I_HP}";
        Mp.text = $" MP  {_inter.I_MP}";
        ZoomHp.text = $" HP  {_inter.I_HP}";
        ZoomMp.text = $" MP  {(int)_inter.I_MP}";
    }
}
