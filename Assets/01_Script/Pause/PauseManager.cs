using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public KeyCode PauseKey = KeyCode.Escape;
    public static bool isPausing;

    private float timeTemp = 1;
    public GameObject PauseObject = null;

    private void OnEnable()
    {

        SceneManager.LoadScene("Pause", LoadSceneMode.Additive);
        Time.timeScale = 1;
        isPausing = false;
        StartCoroutine(StartOff());
        
        
    }
    IEnumerator StartOff()
    {
        yield return null;
        PauseObject = GameObject.Find("PauseCanvas").transform.GetChild(0).gameObject;
        PauseObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(PauseKey))
        {
            if (!isPausing)
            {
                Play();
            }
            else
            {
                Stop();
            }
        }
    }

    public void Play()
    {
        timeTemp = Time.timeScale;
        Time.timeScale = 0;
        
        PauseObject.SetActive(true);
        isPausing = true;
    }
    public void Stop()
    {
        Time.timeScale = timeTemp;

        PauseObject.SetActive(false);
        isPausing = false;
    }
}
