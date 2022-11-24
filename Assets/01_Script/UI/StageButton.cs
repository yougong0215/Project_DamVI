using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageButton : MonoBehaviour
{
    public void Click(GameObject obj)
    {
        obj.SetActive(obj.activeInHierarchy ? false : true);
    }
}
