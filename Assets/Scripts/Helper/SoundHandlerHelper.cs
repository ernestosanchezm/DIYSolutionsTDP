using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHandlerHelper : MonoBehaviour
{
    public AudioClip keyClick;
    private AudioSource clickSource;
    void Start()
    {
        clickSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlayKeyClick()
    {
        clickSource.PlayOneShot(keyClick);
    }
}
