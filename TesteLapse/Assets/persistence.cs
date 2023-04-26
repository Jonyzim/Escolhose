using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistence : MonoBehaviour
{
    public Dictionary<Character, bool> characterPersistence;
    public Dictionary<Card, bool> deathPersistence;
    public static persistence persistentOne;

    public DeathMenu deathMenu;
    public CharacterMenu charMenu;

    public static void UnlockDeath(Card card)
    {
        persistentOne.deathPersistence[card] = true;
    }
    
    public static void UnlockCharacter(Character chara)
    {
        persistentOne.characterPersistence[chara] = true;
    }
    
    public void Start()
    {
        if (persistentOne != null) return;

        DontDestroyOnLoad(this.gameObject);
        persistentOne = this;
        
        characterPersistence = new Dictionary<Character, bool>();
        deathPersistence = new Dictionary<Card, bool>();

        foreach (var character in charMenu.characters)
        {
            characterPersistence.Add(character,false);           
        }

        foreach (var death in deathMenu.deathCards)
        {
            deathPersistence.Add(death,false);
        }
    }

}
