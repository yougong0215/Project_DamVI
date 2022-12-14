using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionButton : MonoBehaviour
{
    GameObject _obj = null;
    private void OnEnable()
    {
        SceneManager.LoadScene("Option", LoadSceneMode.Additive);

        StartCoroutine(StartOff());
    }

    IEnumerator StartOff()
    {
        yield return null;
        _obj = GameObject.Find("OptionCanvas").transform.GetChild(0).gameObject;
        _obj.SetActive(false);
    }

    public void Click()
    {
        if (GameObject.Find("OptionCanvas"))
        {
            _obj.SetActive(_obj.activeInHierarchy ? false : true);
        }
        else
        {
            Debug.LogError("그런거 없다 게이야");
        }
    }
}
