using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControler : MonoBehaviour
{
    [SerializeField] private GameObject PainelConfig;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip clip;
    [SerializeField] private string GameScene;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle musicToggle;


    public bool sound_fx;
    public bool music;

    void Awake()
    {
        source.clip = clip;
        source.volume = 0.3f;

        PlayerPrefs.SetInt("music", 1);
        PlayerPrefs.SetInt("sound_fx", 1);
        music = true;
        sound_fx = true;
        sfxToggle.isOn= sound_fx;
        musicToggle.isOn = music;

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
        //Debug.Log("Start Game");
    }

    public void CloseConfig()
    { 
        PainelConfig.SetActive(false);
        if (sound_fx == true)
        {
            source.Play();
        }
    }

    public void Music(bool val)
    {
        music = val;
        PlayerPrefs.SetInt("music", music?1:0);
    }

    public void SFX(bool val)
    {
        sound_fx = val;
        PlayerPrefs.SetInt("sound_fx", sound_fx?1:0);
    }
}
