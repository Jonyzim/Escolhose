using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControler : MonoBehaviour
{
    [SerializeField] private GameObject PainelConfig;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] private string GameScene;
    public bool sound_fx;
    public bool music;
        //[SerializeField] Animator ini;
    
    void Start()
    {
        source.clip = clip;
        source.volume = 0.3f;
    }
    public void Config()
    {
        PainelConfig.SetActive(true);
       if (sound_fx == true)
       {
           source.Play();
       }
    }

    public void StartGame()
    {
        //SceneManager.LoadScene(GameScene);
        Debug.Log("Start Game");
    }

    public void CloseConfig()
    { 
        PainelConfig.SetActive(false);
        if (sound_fx == true)
        {
            source.Play();
        }
    }

    public void Music()
    {
        music = !music;
    }

    public void SFX()
    {
        sound_fx = !sound_fx;
    }
}
