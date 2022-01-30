using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxManager : MonoBehaviour
{

    public AudioSource Audio;
    public AudioClip Click;
    public AudioClip Hover;

    public AudioClip Spawn;
    public AudioClip Walk;
    public AudioClip Hurt;
    public AudioClip Blow;
    public AudioClip Score;
    public AudioClip Place;

    public static SfxManager sfxInstance;

    void Awake()
    {
        if (sfxInstance != null && sfxInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        sfxInstance = this;
        DontDestroyOnLoad(this);
    }
}
