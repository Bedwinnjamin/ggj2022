using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    void Start()
    {
        MusManager.musInstance.Audio.clip = MusManager.musInstance.Menu;
        MusManager.musInstance.Audio.Play();
    }

    public void PlayGame()
    {
    	//SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    	SceneManager.LoadScene("GameScene");
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Click);

        MusManager.musInstance.Audio.clip = MusManager.musInstance.Game;
        MusManager.musInstance.Audio.Play();
    }

    public void Hover()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Hover);
    }
    
    public void QuitGame()
    {
        SfxManager.sfxInstance.Audio.PlayOneShot(SfxManager.sfxInstance.Click);
    	//Application.Quit();
    }
}
