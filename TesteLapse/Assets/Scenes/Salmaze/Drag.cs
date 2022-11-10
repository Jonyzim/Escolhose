using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField] private RectTransform _transform;
    [SerializeField] private CanvasGroup _canvasGroup;
    Vector3 vetor = new Vector3(0, 0, 0);
    float armaz;
    [SerializeField] int grau=200;
    
    public void OnPointerDown(PointerEventData eventData){
    }
    
    public void OnBeginDrag(PointerEventData eventData)
    {
       _canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      armaz = _transform.anchoredPosition.x;
      _transform.anchoredPosition += eventData.delta;
      vetor.z = tres(900,grau*-1,armaz);
      //Debug.Log(vetor.z);
      transform.eulerAngles = vetor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      vetor.z = 0;
      transform.eulerAngles = vetor;       
      _canvasGroup.blocksRaycasts = true;
    }

    public float tres(float a, float x, float b){
          if(a != 0)
          return (b * x)/a;
          
          return 0;
    }
}
