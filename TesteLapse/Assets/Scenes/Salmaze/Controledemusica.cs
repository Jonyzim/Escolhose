using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controledemusica : MonoBehaviour
{
    public AudioSource source;//Fonte
    public AudioClip clip;//Musica
    
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("music")==1) {
              source.loop = true;
              source.clip = clip;
              source.volume = 0.3f;
              source.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
