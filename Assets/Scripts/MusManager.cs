using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusManager : MonoBehaviour
{

    public AudioSource Audio;
    public AudioClip Menu;
    public AudioClip Lobby;
    public AudioClip Game;
    public AudioClip EndGame;

    public static MusManager musInstance;

    void Awake()
    {
        if (musInstance != null && musInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        musInstance = this;
        DontDestroyOnLoad(this);
    }
}
