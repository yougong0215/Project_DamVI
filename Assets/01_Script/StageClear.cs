using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageClear : MonoBehaviour
{
    [SerializeField] GameObject UI;
    GameObject obj;

    float time = 0;
    float Score = 0;
    
    public void OnClear()
    {
        obj = Instantiate(UI);
        time = PlayerAttackManager.Instance.CurrentClearTime;
    }
}
