using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Cards/ArcCard", order = 1)]
public class ArcCard : Card
{
    public bool isProgressLeftDirect;
    public ArcCard progressLeft;
    
    public bool isProgressRightDirect;
    public ArcCard progressRight;
}