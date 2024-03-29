using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Pack", menuName = "ScriptableObjects/CardContainers/ArcCardDeck", order = 2)]
public class ArcDeck : ScriptableObject
{
    public string title;
    public ArcCard startingCard,currCard;

    [SerializeField]public LockType LockType{get; set;}

    public bool locked;
    
    private void OnEnable()
    {
        currCard = startingCard;
        
        switch (LockType)
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
