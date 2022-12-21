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
    public bool die;
    public string responseLeft;
    public Result resultLeft;
    
    public string responseRight;
    public Result resultRight;
}

[System.Serializable]
public class Result
{
    private const int StatCount = 4;
    public float[] values = new float[StatCount];
    
    public Card nextCard;

    public bool unlocksPack;
    public CardPack unlockCardPack;
}
