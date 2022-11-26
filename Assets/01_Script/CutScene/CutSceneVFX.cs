using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DG.Tweening;

public class CutSceneVFX : MonoBehaviour
{   
    public GameObject sphere;

    public float size;
    public float time;
    public VisualEffect vfx;

    private float minSize;

    private void OnEnable()
    {
        vfx.gameObject.SetActive(false);
        minSize = sphere.transform.localScale.x;
    }
    public void Play(Vector3 position)
    {
        vfx.gameObject.SetActive(true);
        vfx.gameObject.transform.position = position;
        sphere.transform.position = position;
        sphere.transform.DOScale(size, time);
    }

    public void TestPlay(float x)
    {
        Play(new Vector3(x, x, x));
    }

    public void Stop()
    {
        vfx.gameObject.SetActive(false);
        sphere.transform.DOScale(minSize, time);

    }
}
