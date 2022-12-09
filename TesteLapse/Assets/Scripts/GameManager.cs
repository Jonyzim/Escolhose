using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private const int statCount = 4;
    private const int maxStat=10;
    
    //Simple Cards
    int currCardId = 0;
    public Card currCard;
    [SerializeField] private List<Card> cards;
    
    //Arc Cards
    public ArcDeck currArcDeck = null;
    [FormerlySerializedAs("arcProgressMinRate")] public int arcMinProgressRate;
    public float arcProgressChance;
    private int _arcCurrProgressDelay = 0;
    
    //CardPacks
    [SerializeField] private List<CardPack> cardPacks;
    [SerializeField] private List<ArcDeck> arcDecks;
    
    //Stats Values
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
    
    //Misc
    private System.Random _random = new System.Random();
    
    private void Start()
    {
        GetSimpleCard();
        SetCardUI();
        
        SetStatsUI();
        ResetChange();
    }
    
    #region CardLogic
        public void AnswerLeft()
        {
            if (currCard is ArcCard temp)
            {
                print("WHATTT");
                if (currArcDeck == null) throw new UnityException("The current card is a ArcCard, but currArcDeck is NULL.");

                direct_progress = currArcDeck.currCard.isProgressLeftDirect;
                currArcDeck.currCard = temp.progressLeft;
                print($"The {currArcDeck.title}'s new card will be: {currArcDeck}");
            }
            
            HandleResult(currCard.resultLeft);
        }

        private bool direct_progress = false;
        public void AnswerRight()
        {
            if (currCard is ArcCard temp)
            {
                if (currArcDeck == null) throw new UnityException("The current card is a ArcCard, but currArcDeck is NULL.");

                direct_progress = currArcDeck.currCard.isProgressRightDirect;
                currArcDeck.currCard = temp.progressRight;
                print($"The {currArcDeck.title}'s new card will be: {currArcDeck}");
            }

            HandleResult(currCard.resultRight);
        }

        private bool IsNextCardArc()
        {
            if ((float)_random.NextDouble() < arcProgressChance || _arcCurrProgressDelay++ > arcMinProgressRate)
            {
                _arcCurrProgressDelay = 0;
                return true;
            }

            return false;
        }
        
        private void GetCard()
        {
            //Obtains a ArcCard
            if ((IsNextCardArc() && GetArcCard()))
                return;
            
            print("TO Aqui!!!");
            
            //Obtains a SimpleCard
            currArcDeck = null;
            GetSimpleCard();
        }

        private void GetSimpleCard()
        {
            currCard = cards[currCardId];
            currCardId++;
            currCardId%=cards.Count;
            if(currCardId == 0) Shuffle(0,cards.Count);// embaralha as cartas ao terminar o baralho
        }

        private bool GetArcCard()
        {
            if (arcDecks.Count == 0) return false; 
         
            ArcCard temp = null;
                
            while (temp == null && arcDecks.Count > 0)
            {
                int index = _random.Next(0, arcDecks.Count);
                currArcDeck = arcDecks[index];
                temp = currArcDeck.currCard;

                if (temp == null)
                    arcDecks.Remove(currArcDeck);
                else
                {
                    currCard = temp;
                    return true;
                }
            }

            return false;
        }

        private void AddCardsFromPack(CardPack cardPack)
        {
            foreach (Card c in cardPack.cards)
                cards.Add(c);
            Shuffle(currCardId,cards.Count);
        }
        
        private void Shuffle(int startIndex, int endIndex)
        {
            if (startIndex == endIndex) return;
            
            for (int i = endIndex-1; i > startIndex; i--)
            {
                int k = _random.Next(i + 1);
                (cards[i], cards[k]) = (cards[k], cards[i]);
            }
        }
    #endregion
    
    #region StatControl

    private void HandleResult(Result result)
    {
            UpdateStats(result.values);
            
            //Desbloqueia Cartas
            if (result.unlocksPack && result.unlockCardPack.locked)
            {
                result.unlockCardPack.locked = false;
                AddCardsFromPack(result.unlockCardPack);
            }

            //Possibilita a sobrecarga de cartas futuras
            if (result.nextCard != null) 
                currCard = result.nextCard;
            else if (direct_progress && currArcDeck.currCard != null)
            {
                currCard = currArcDeck.currCard;
            }
            else GetCard();

            direct_progress = false;
            SetCardUI();
    }


        public void ResetChange()
        {
            for (int i = 0; i < statCount; i++)
            {
                statChange[i].localScale = new Vector3(0,0, 1f);
            }
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
        #endregion
        
    #region UiAndAnimation
        
        public void ShowChangeLeft()
        {
            for(int i = 0; i < statCount; i++)
            {
                float sum=Mathf.Abs(currCard.resultLeft.values[i]);
                statChange[i].localScale = new Vector3(sum / maxStat, sum / maxStat, 1f);
            }
        }
        
        public void ShowChangeRight()
        {
            for (int i = 0; i < statCount; i++)
            {
                float sum = Mathf.Abs( currCard.resultRight.values[i]);
                statChange[i].localScale = new Vector3(sum / maxStat, sum / maxStat, 1f);
            }
        }
        
        private void SetStatsUI()
        {
            // update sliders with stats value
            for(int i = 0; i < statCount; i++) {
                StartCoroutine(StatsValueAnim(sliders[i], stats[i] / 100f));
                //sliders[i].value = stats[i] / 100f;
            }
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
        
        [SerializeField] private float statAnimSpeed = 0.008f;
        IEnumerator StatsValueAnim(Slider slider,  float newValue)
        {
            float lastValue = slider.value;
            float t=0;
            Image statsIcon = slider.fillRect.GetComponent<Image>();
            if (newValue > lastValue)
                statsIcon.color= Color.green;
            else if(newValue < lastValue)
                statsIcon.color = Color.red;
            do
            {
                slider.value = Mathf.Lerp(lastValue, newValue, t);
                t += statAnimSpeed;
                yield return new WaitForEndOfFrame();
            } while (t<=1f);
            slider.value = newValue;
            statsIcon.color = Color.white;
        }
        
    #endregion
}
