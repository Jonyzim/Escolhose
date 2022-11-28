using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Cards/Card", order = 1)]
public class Card : ScriptableObject
{
    public string text;
    public Sprite image;
    public Character character;
    public bool characterImage;
    
    public string responseLeft;
    public Result resultLeft;
    
    public string responseRight;
    public Result resultRight;
    
    private void OnValidate()
    {
        if (resultLeft.overridesNextCard && resultLeft.nextCard == null)
        {
            throw new UnityException($"Card {name}'s LEFT overrides the next card, but does not hava a card to use");
        }
        
        if (resultRight.overridesNextCard && resultRight.nextCard == null)
        {
            throw new UnityException($"Card {name}'s RIGHT overrides the next card, but does not hava a card to use");
        }
    }
}

[System.Serializable]
public class Result
{
    private const int StatCount = 4;
    public float[] values = new float[StatCount];
    
    public bool overridesNextCard;
    public Card nextCard;

    public bool unlocksPack;
    public CardPack unlockCardPack;
}
