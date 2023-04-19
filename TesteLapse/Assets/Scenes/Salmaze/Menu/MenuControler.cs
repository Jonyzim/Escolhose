using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControler : MonoBehaviour
{
    [SerializeField] private GameObject PainelConfig;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip click;
    [SerializeField] private string GameScene;

    void Awake()
    {
        source.clip = click;
        source.volume = 0.3f;
    }
    public void Config()
    {
        PainelConfig.SetActive(true);
       if (PlayerPrefs.GetInt("sound_fx") == 1)
       {
           source.Play();
       }
    }

    public void StartGame()
    {
        SceneManager.LoadScene(GameScene);
    }

    public void CloseConfig()
    { 
        PainelConfig.SetActive(false);
        if (PlayerPrefs.GetInt("sound_fx") == 1)
        {
            source.Play();
        }
    }
}
