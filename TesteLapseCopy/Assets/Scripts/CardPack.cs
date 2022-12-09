using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum LockType
{
    AlwaysUnlocked,
    SoulLock,
    LiveLock,
    AlwaysLocked
}

[CreateAssetMenu(fileName = "Card Pack", menuName = "ScriptableObjects/CardContainers/CardPack", order = 1)]
public class CardPack : ScriptableObject
{
    [SerializeField] public string title;
    [SerializeField] public List<Card> cards;
    [SerializeField] public LockType lockType;
    public bool locked;

    private void OnEnable()
    {
        switch (lockType)
        {
            case LockType.AlwaysUnlocked:
                locked = false;
                break;
            
            case LockType.LiveLock:
            case LockType.AlwaysLocked: 
                locked = true;
                break;
            case LockType.SoulLock:
                break;
        }
    }
}
