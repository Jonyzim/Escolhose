using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Character", order = 1)]
public class Character : ScriptableObject
{
    public string name;
    public string description;
    public Sprite image;
    public bool unlocked;
}
