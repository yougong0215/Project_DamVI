using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseInteraction : MonoBehaviour
{
    [Header("����")]
    [SerializeField] string _name;
    [SerializeField] string _Info;
    public void Interaction()
    {
        Active();
    }

    public void Active()
    {
        Debug.Log($"{_name} : {_Info}");
    }


}
