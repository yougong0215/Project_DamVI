using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    GameObject _obj = null;
    private void OnEnable()
    {
        SceneManager.LoadScene("StageSelect", LoadSceneMode.Additive);

        StartCoroutine(StartOff());
    }

    IEnumerator StartOff()
    {
        yield return null;
        _obj = GameObject.Find("StageCanvas");
        _obj.SetActive(false);
    }

    public void Click()
    {
        if (_obj)
        {
            _obj.SetActive(_obj.activeInHierarchy ? false : true);
        }
        else
        {
            Debug.LogError("그런거 없다 게이야");
        }
    }
}
