using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragAndDropV2 : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    public Canvas canvas;
    public bool isLocked = false;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Detail detail;

    public CarV2 car;
    //private SoundManager soundManager; 
    
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        detail = GetComponent<Detail>();
    }
    private void Start() 
    {
        
        //soundManager = canvas.gameObject.GetComponent<SoundManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isLocked) 
        {
            GetComponent<Detail>().PlayPickupSound();
            gameObject.transform.position = eventData.pointerCurrentRaycast.worldPosition;
            car.CheckDisableDetailTriggers(gameObject.GetComponent<Detail>());
            canvasGroup.blocksRaycasts = false;
            gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isLocked) 
        {
            rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        }
            
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isLocked) 
        {
            HandTips.time = 0;
            canvasGroup.blocksRaycasts = true;
            detail.PlayErrorSound();
        }
        gameObject.transform.parent.gameObject.GetComponent<DetailsSpawner>().hand.CheckForHelp(detail);
        StartCoroutine(SmoothReturn(detail.spawnLocalPosition));
    }
    IEnumerator SmoothReturn(Vector3 startPos)
    {
        var currentStart = gameObject.transform.localPosition;
        for (float i = 0; i <= 1.1f; i += 0.1f)
        {
            gameObject.transform.localPosition = Vector3.Lerp(currentStart, startPos, i);
            yield return new WaitForSeconds(0.03f);
            gameObject.transform.localScale = detail.spawnScale;
        }
    }
}