using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card Pack", menuName = "ScriptableObjects/CardContainers/DeathPack", order = 1)]
public class DeathPack : ScriptableObject
{

    [SerializeField] public List<Card> maxCards;
    [SerializeField] public List<Card> minCards;
    public Card GetMaxDeath(int i)
    {
        return maxCards[i];
    }

    public Card GetMinDeath(int i)
    {
        return minCards[i];
    }
}
