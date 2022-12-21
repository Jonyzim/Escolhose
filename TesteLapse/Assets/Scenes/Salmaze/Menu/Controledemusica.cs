using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controledemusica : MonoBehaviour
{
    public AudioSource source;//Fonte
    public AudioClip clip;//Musica
    
    public GameObject MenuManager;//MenuManager
    
    // Start is called before the first frame update
    void Start()
    {
        if (MenuManager.GetComponent<MenuControler>().music == true) {
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
