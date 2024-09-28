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
    [SerializeField] private float clickVolume;
    [SerializeField] private string GameScene;
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle musicToggle;
    private bool sound_fx;
    private bool music;
    private Controledemusica controledemusica;

    void Awake()
    {
        controledemusica = GameObject.Find("MUSIC").GetComponent<Controledemusica>();
        
        source.clip = click;
        source.volume = clickVolume;

        if (!PlayerPrefs.HasKey("sound_fx"))
        {
            PlayerPrefs.SetInt("sound_fx", 1);
        }
        if (!PlayerPrefs.HasKey("music"))
        {
            PlayerPrefs.SetInt("music", 1);
        }

        sound_fx = PlayerPrefs.GetInt("sound_fx") == 1;
        music = PlayerPrefs.GetInt("music") == 1;
        
        sfxToggle.isOn = PlayerPrefs.GetInt("sound_fx") == 1;
        musicToggle.isOn = PlayerPrefs.GetInt("music") == 1;

        if (music)
        {
            controledemusica.TocarMusica();
        }
    }
    
    public void Config()
    {
        PainelConfig.SetActive(true);

        if (sound_fx)
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
        
        if (sound_fx)
        {
            source.Play();
        }
    }

    public void OnToggleSFX()
    {
        sound_fx = sfxToggle.isOn;
        PlayerPrefs.SetInt("sound_fx", sound_fx ? 1 : 0);
    }

    public void OnToggleMusic()
    {
        PlayerPrefs.SetInt("music", musicToggle.isOn ? 1 : 0);

        if (musicToggle.isOn)
        {
            controledemusica.TocarMusica();
        }
        else
        {
            controledemusica.PararMusica();
        }
    }
}
