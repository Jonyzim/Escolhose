using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class Drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private Canvas _canvas;
    Vector3 vetor = new Vector3(0, 0, 0);
    
    [SerializeField] int grau=200;
    
    Vector3 initialPos = new Vector3(0, 0, 0);
    private bool canDrag = true;
    private bool showingChange = false;
    private bool goingRight = false;
    private int state=0;
    [SerializeField] UnityEvent changeLeft;
    [SerializeField] UnityEvent changeRight;
    [SerializeField] UnityEvent resetChange;
    [SerializeField] UnityEvent chooseRight;
    [SerializeField] UnityEvent chooseLeft;
    [SerializeField] float maxX=150f;

    public float CurrRatioX;

    float exitX = 400f;
    
    private void Start()
    {
        initialPos = _transform.anchoredPosition;
    }
    
    public void OnPointerDown(PointerEventData eventData){}
    
    public void OnBeginDrag(PointerEventData eventData)
    {
       _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            float _x = _transform.anchoredPosition.x;
            _transform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            if (!showingChange)
            {
                // right
                if (_x > 0)
                {
                    changeRight.Invoke();
                    showingChange=true;
                    goingRight = true;
                }
                // left
                else if (_x < 0)
                {
                    changeLeft.Invoke();
                    showingChange = true;
                    goingRight = false;
                }
            }
            else
            {
                // right
                if (!goingRight && _x > 0)
                {
                    changeRight.Invoke();
                    goingRight = true;
                }
                // left
                else if (goingRight && _x < 0)
                {
                    changeLeft.Invoke();
                    goingRight = false;
                }
            }
            vetor.z = tres(900, grau * -1, _x);
            transform.eulerAngles = vetor;
            CurrRatioX = _x / maxX;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      _canvasGroup.blocksRaycasts = true;
        float _x = _transform.anchoredPosition.x;
        
        if (_x > maxX)
        {
            canDrag = false;
            StartCoroutine(GoDirection(1,chooseRight));
        }
        else if (_x < -maxX)
        {
            canDrag = false;
            StartCoroutine(GoDirection(-1,chooseLeft));
            
        }
        else
        {
            ResetPosition();
        }
        resetChange.Invoke();
        showingChange = false;
        CurrRatioX = _x / maxX;
    }
    private void ResetPosition()
    {

        vetor.z = 0;
        transform.eulerAngles = vetor;
        _transform.anchoredPosition = initialPos;
    }
    
    [SerializeField] float animSpeed;
    
    IEnumerator GoDirection(int dir,UnityEvent e)
    {
        Vector3 pos;
        do
        {
             pos = _transform.anchoredPosition;
            pos.x += animSpeed * dir;
            _transform.anchoredPosition = pos;
            yield return new WaitForEndOfFrame();
        }while (Mathf.Abs(pos.x)< exitX);
        
        e.Invoke();
        ResetPosition();
        canDrag = true;
    }
    
    public float tres(float a, float x, float b){
          if(a != 0)
          return (b * x)/a;
          
          return 0;
    }
}
