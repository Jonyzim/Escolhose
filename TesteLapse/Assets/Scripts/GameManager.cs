using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const int statCount = 4;
    private const int maxStat=13;

    private const string MENU_SCENE = "Menu";
    //Simple Cards
    int currCardId = 0;
    float score = 0;
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

    private float DifficultyScale()
    {
        return (float)0.25 * math.sqrt(score);
    }

    // Stats UI
    [SerializeField] private Text scoreText;
    [SerializeField] private Slider[] sliders = new Slider[statCount];
    [SerializeField] private Transform[] statChange = new Transform[statCount];
    [SerializeField] private DeathMenu _deathMenu;

    // Card UI
    [SerializeField] private Image cardImage;
    [SerializeField] private Text cardText;
    [SerializeField] private Text characterText;
    [SerializeField] private Text cardRespLeft;
    [SerializeField] private Text cardRespRight;
    [SerializeField] Drag dragScript;
    
    //Misc
    private System.Random _random = new System.Random();
    
    private void Start()
    {
        Shuffle(0,cards.Count);
        GetSimpleCard();
        SetCardUI();
        
        SetStatsUI();
        ResetChange();
        score = 0;
    }

    public void Load(string scene)
    {
        SceneManager.LoadScene(scene);
       
    }
    
    #region CardLogic
        public void AnswerLeft()
        {
            if (currCard.die)
            {
                currCard.dieUnlocked = true;
                print("YOU DIED");
                Load(MENU_SCENE);

            }
            else
            {
                if (currCard is ArcCard temp)
                {
                    //print("WHATTT");
                    if (currArcDeck == null) throw new UnityException("The current card is a ArcCard, but currArcDeck is NULL.");

                    direct_progress = currArcDeck.currCard.isProgressLeftDirect;
                    currArcDeck.currCard = temp.progressLeft;
                    //print($"The {currArcDeck.title}'s new card will be: {currArcDeck}");
                }

                HandleResult(currCard.resultLeft);
            }

        }

        private bool direct_progress = false;
        public void AnswerRight()
        {
            if (currCard.die)
            {

            // print("YOU DIED");
                Load(MENU_SCENE);
            }
            else
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

    bool isDead = false;
    Card deathCard;
    [SerializeField] DeathPack deathPack;
        private bool CheckDeath()
        {
            for (int i = 0; i < statCount; i++)
            {
                if (stats[i] >= 100)
                {
                    deathCard = deathPack.GetMaxDeath(i);
                    return true;
                }
                else if (stats[i] <= 0)
                {
                    deathCard = deathPack.GetMinDeath(i);
                    return true;
                }
            }
            return false;
        }

        private void HandleResult(Result result)
        {
            UpdateStats(result.values);
            isDead = CheckDeath();
            if (isDead)
            {
                currCard = deathCard;
            }
            else
            {
                score += 1f;
                scoreText.text = "Semana " + score.ToString();
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
            }

            SetCardUI();
        }

        private void UpdateStats(float[] statSum)
        {
            for (int i = 0; i < statCount; i++)
            {
                stats[i] += DifficultyScale()*statSum[i];
                stats[i] = Mathf.Max(stats[i], 0);
                stats[i] = Mathf.Min(stats[i], 100);
            }
            SetStatsUI();
        }
    #endregion

    #region UiAndAnimation

        public void ResetChange()
        {
            for (int i = 0; i < statCount; i++)
            {
                statChange[i].localScale = new Vector3(0, 0, 1f);
            }
        }

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
        Coroutine changeColorRoutine=null;

        public void ResetResp()
        {

            if (changeColorRoutine != null)
                StopCoroutine(changeColorRoutine);
            cardRespRight.color = new Color(1, 1, 1, 0);
            cardRespLeft.color = new Color(1, 1, 1, 0);
        }
        public void ShowRespLeft()
        {
            if (changeColorRoutine != null)
                StopCoroutine(changeColorRoutine);
            cardRespRight.color = new Color(1, 1, 1, 0);
            changeColorRoutine = StartCoroutine(ChangeTextColor(cardRespLeft, new Color(1, 1, 1, 1), new Color(1, 1, 1, 0)));
        }

        public void ShowRespRight()
        {
            if (changeColorRoutine != null)
                StopCoroutine(changeColorRoutine);
            cardRespLeft.color = new Color(1, 1, 1, 0);
            changeColorRoutine=StartCoroutine(ChangeTextColor(cardRespRight, new Color(1, 1, 1, 1), new Color(0, 0, 0, 0)));
        }

        IEnumerator ChangeTextColor(Text text,Color newColor, Color oldColor)
        {
            float t = 0;
            do
            {
                text.color = Color.Lerp(oldColor, newColor, t);
                t += statAnimSpeed;
                yield return new WaitForFixedUpdate();
            } while (t <= 1f);
            text.color = newColor;
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
    [SerializeField] private Color positiveColor;
    [SerializeField] private Color negativeColor;
        IEnumerator StatsValueAnim(Slider slider,  float newValue)
        {
            float lastValue = slider.value;
            float t=0;
            Image statsIcon = slider.fillRect.GetComponent<Image>();
            if (newValue > lastValue)
                statsIcon.color= positiveColor;
            else if(newValue < lastValue)
                statsIcon.color = negativeColor;
            do
            {
                slider.value = Mathf.Lerp(lastValue, newValue, t);
                t += statAnimSpeed;
                yield return new WaitForFixedUpdate();
            } while (t<=1f);
            slider.value = newValue;
            statsIcon.color = Color.white;
        }
        
    #endregion
}
