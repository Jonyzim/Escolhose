using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CharacterMenu : MonoBehaviour
{
    [SerializeField] private List<Character> characters;
    [SerializeField] private GameObject characterViewPrefab;
    [SerializeField] private Sprite lockedIcon;
    private List<Transform> views=new List<Transform>();
    // Start is called before the first frame update
    void Awake()
    {
        int n = characters.Count;
        for (int i=0;i<n;i++)
        {
            GameObject obj=Instantiate(characterViewPrefab, transform);
            views.Add(obj.transform);
        }
    }

    private void OnEnable()
    {
        int n = characters.Count;
        for (int i = 0; i < n; i++)
        {
            SetCharacterView(characters[i], views[i]);
        }
        
        transform.localPosition=new Vector3(0,80f,0);
    }
    private void SetCharacterView(Character character, Transform view)
    {
        if (character.unlocked)
        {
            view.Find("Name").GetComponent<TMP_Text>().text = character.name;
            view.Find("Image").GetComponent<Image>().sprite = character.image;
            view.Find("Desc").GetComponent<TMP_Text>().text = character.description;
        }
        else
        {

            view.Find("Name").GetComponent<TMP_Text>().text = "";
            view.Find("Image").GetComponent<Image>().sprite = lockedIcon;
            view.Find("Desc").GetComponent<TMP_Text>().text = "";
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
