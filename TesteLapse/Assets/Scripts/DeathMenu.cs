using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DeathMenu : MonoBehaviour
{
    public List<Card> deathCards;

    [SerializeField] private GameObject deathViewPrefab;
    [SerializeField] private Sprite lockedIcon;
    private List<Transform> views = new List<Transform>();
    bool first = true;

    void Awake()
    {        
        int n = deathCards.Count;
        for (int i=0;i<n;i++)
        {
            deathCards[i].dieUnlocked = false;
            GameObject obj=Instantiate(deathViewPrefab, transform);
            views.Add(obj.transform);
        }
    }
    
    private void OnEnable()
    {
        int n = deathCards.Count;
        for (int i = 0; i < n; i++)
        {
            SetDeathView(deathCards[i], views[i]);
        }
        
        transform.localPosition = new Vector3(0,80f,0);
    }
    
    private void SetDeathView(Card card, Transform view)
    {
        print(card.dieTitle);
        view.Find("Name").GetComponent<TMP_Text>().text = card.dieTitle;
        if(card.dieUnlocked)
        {
            view.Find("Image").GetComponent<Image>().sprite = card.image;
            view.Find("Desc").GetComponent<TMP_Text>().text = card.dieDescription;
        }
        else
        {
            view.Find("Image").GetComponent<Image>().sprite = lockedIcon;
            view.Find("Desc").GetComponent<TMP_Text>().text = "";
        }
    }
}
