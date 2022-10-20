using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{

    private const int statCount = 4;
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
        cardImage.sprite = currCard.image;
        cardText.text= currCard.text;
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
            statChange[i].localScale = new Vector3(sum / 30, sum / 30, 1f);
        }

    }
    public void ShowChangeRight()
    {
        for (int i = 0; i < statCount; i++)
        {
            float sum = Mathf.Abs( currCard.RespRightSum[i]);
            statChange[i].localScale = new Vector3(sum / 30, sum / 30, 1f);
        }

    }
}
