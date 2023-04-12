using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controledemusica : MonoBehaviour
{
    public AudioSource source;//Fonte
    public AudioClip clip;//Musica
    [SerializeField] private Toggle sfxToggle;
    [SerializeField] private Toggle musicToggle;

    private bool sound_fx;
    private bool music;

    void Start()
    {
        source.loop = true;
        source.clip = clip;
        source.volume = 0.3f;

        if (sfxToggle != null || musicToggle != null)
        {
            PlayerPrefs.SetInt("music", 1);
            PlayerPrefs.SetInt("sound_fx", 1);
            music = true;
            sound_fx = true;
            sfxToggle.isOn = sound_fx;
            musicToggle.isOn = music;
            source.Play();
        }
        
        if (PlayerPrefs.GetInt("music") == 1)
        {
            source.Play();
        }
    }

    public void Music()
    {
        music = !music;
        if (music == false)
        {
            source.Stop();
        }
        if (music == true)
        {
            source.Play();
        }
        PlayerPrefs.SetInt("music", music ? 1 : 0);
    }

    public void SFX()
    {
        sound_fx = !sound_fx;
        PlayerPrefs.SetInt("sound_fx", sound_fx ? 1 : 0);
    }
}
