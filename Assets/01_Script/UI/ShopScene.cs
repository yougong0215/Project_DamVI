using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShopScene : MonoBehaviour
{
    GameObject _obj = null;
    private void OnEnable()
    {
        SceneManager.LoadScene("Store", LoadSceneMode.Additive);

        StartCoroutine(StartOff());
    }

    IEnumerator StartOff()
    {
        yield return null;
        _obj = GameObject.Find("ShopCanvas").transform.GetChild(0).gameObject;
        _obj.SetActive(false);
    }

    public void Click()
    {
        if (GameObject.Find("ShopCanvas"))
        {
            _obj.SetActive(_obj.activeInHierarchy ? false : true);
        }
        else
        {
            Debug.LogError("그런거 없다 게이야");
        }
    }
}
