using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    private const int statCount = 4;
    private const int maxStat=10;
    int currCardId = 0;
    Card currCard;

    [SerializeField] private List<Card> cards;


    [SerializeField] private float[] stats=new float[statCount];

    // Stats UI
    [SerializeField] private Slider[] sliders = new Slider[statCount];
    [SerializeField] private Transform[] statChange = new Transform[statCount];


    // Card UI
    [SerializeField] private Image cardImage;
    [SerializeField] private Text cardText;
    [SerializeField] private Text characterText;
    [SerializeField] private Text cardRespLeft;
    [SerializeField] private Text cardRespRight;
    private void Start()
    {
        GetCard(); 
        SetStatsUI();
        ResetChange();
    }
    private void GetCard()
    {
        currCard = cards[currCardId];
        currCardId++;
        currCardId%=cards.Count;
        SetCardUI();
    }

    private void UpdateStats(float[] statSum)
    {
        for (int i = 0; i < statCount; i++)
        {
            stats[i] += statSum[i];
            stats[i] = Mathf.Max(stats[i], 0);
            stats[i] = Mathf.Min(stats[i], 100);

        }
        SetStatsUI();
    }

    private void SetCardUI()
    {
        if (currCard.characterImage && currCard.character!=null)
            cardImage.sprite = currCard.character.image;
        else
            cardImage.sprite = currCard.image;
        cardText.text= currCard.text;
        if (currCard.character != null)
        {
            characterText.text = currCard.character.name;
            currCard.character.unlocked = true;
        }
        else
            characterText.text = "";
        cardRespLeft.text = currCard.responseLeft;
        cardRespRight.text=currCard.responseRight; 
    }

    private void SetStatsUI()
    {
        // update sliders with stats value
        for(int i = 0; i < statCount; i++) {
            sliders[i].value = stats[i] / 100f;
        }
    }

    public void AnswerLeft()
    {
        UpdateStats(currCard.RespLeftSum);
        GetCard();
    }
    public void AnswerRight()
    {
        UpdateStats(currCard.RespRightSum);
        GetCard();
    }


    public void ResetChange()
    {
        for (int i = 0; i < statCount; i++)
        {
            statChange[i].localScale = new Vector3(0,0, 1f);
        }

    }
    public void ShowChangeLeft()
    {
        for(int i = 0; i < statCount; i++)
        {
            float sum=Mathf.Abs(currCard.RespLeftSum[i]);
            statChange[i].localScale = new Vector3(sum / maxStat, sum / maxStat, 1f);
        }

    }
    public void ShowChangeRight()
    {
        for (int i = 0; i < statCount; i++)
        {
            float sum = Mathf.Abs( currCard.RespRightSum[i]);
            statChange[i].localScale = new Vector3(sum / maxStat, sum / maxStat, 1f);
        }

    }
}
