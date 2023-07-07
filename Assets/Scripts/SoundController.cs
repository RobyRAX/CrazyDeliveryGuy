using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip[] clips; 

    public void PlayCheer()
    {
        GetComponent<AudioSource>().PlayOneShot(clips[0]);
    }

    public void PlayBoo()
    {
        GetComponent<AudioSource>().PlayOneShot(clips[1]);
    }
}
