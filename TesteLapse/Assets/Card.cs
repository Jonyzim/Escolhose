using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Card", order = 1)]

public class Card : ScriptableObject
{
    private const int statCount = 4;
    public float[] RespLeftSum = new float[statCount];
    public float[] RespRightSum = new float[statCount];
    public string text;
    public Sprite image;
    public string responseLeft;
    public string responseRight;
}
