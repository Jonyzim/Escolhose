using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Absurdities : MonoBehaviour
{
    public List<Card> Cards;
    public List<UnityEvent> Events;
    private Color PINK = new Color(1, (float)145/255, (float)175/255);
    public void AbsurdCheck(Card card)
    {
        //Falta um dia pra entregar o código, supostamente só vou precisar isso pra um único caso, futuramente irei deixá-lo genérico, muito no futuro
        int index = Cards.IndexOf(card);
        if (index != -1)
        {
            print("Bonk!");
            Events[index].Invoke();
        }
    }

    public void SetPinkBackground()
    {
        Camera.main.backgroundColor = PINK;
    }
}