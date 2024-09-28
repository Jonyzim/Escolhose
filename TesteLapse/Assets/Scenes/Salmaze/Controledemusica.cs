using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controledemusica : MonoBehaviour
{
    public AudioSource source;//Fonte
    public AudioClip clip;//Musica
    public float musica_volume;

    public AudioSource sfx_source;
    public AudioClip cardClip;
    public float sfx_volume;
    
    private bool sound_fx;

    void Start()
    {
        if (PlayerPrefs.GetInt("music") == 1)
        {
            TocarMusica();
        }
    }

    public void TocarMusica()
    {
        source.loop = true;
        source.clip = clip;
        source.volume = musica_volume;
        source.Play();
    }

    public void PararMusica()
    {
        source.Stop();
    }
}
