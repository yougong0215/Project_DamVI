using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BGM : MonoBehaviour
{
    public AudioSource audiosource;
    public List<AudioClip> sources = new List<AudioClip>();

    private void OnEnable()
    {
        audiosource = GetComponent<AudioSource>();
        audiosource.clip = sources[Random.Range(0, sources.Count)];
        audiosource.Play();
    }


}
