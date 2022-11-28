using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    private PauseManager pauseManager;
    private void OnEnable()
    {
        pauseManager = GameObject.FindObjectOfType<PauseManager>();
    }
    public void Click()
    {
        pauseManager.Stop();
    }
}
