using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionButton : MonoBehaviour
{
    public void Click()
    {
        if (GameObject.Find("OptionCanvas"))
        {
            GameObject obj = GameObject.Find("OptionCanvas").transform.GetChild(0).gameObject;
            obj.SetActive(obj.activeInHierarchy ? false : true);
        }
        else
        {
            Debug.LogError("�׷��� ���� ���̾�");
        }
    }
}
