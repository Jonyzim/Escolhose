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
        SceneManager.LoadScene(GameScene);
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
        PlayerPrefs.SetInt("music", music?1:0);
    }

    public void SFX()
    {
        sound_fx = !sound_fx;
        PlayerPrefs.SetInt("sound_fx", sound_fx?1:0);
    }
}
