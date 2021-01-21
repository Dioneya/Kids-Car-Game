using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pistol : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private RectTransform rectTransform;
    public Canvas canvas;
    private CanvasGroup canvasGroup;
    [SerializeField] public Vector3 spawnLocalPosition;
    public bool isWork = false;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void SetLocalPos() 
    {
        spawnLocalPosition = gameObject.transform.localPosition;
    }
    public void OnDrop(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        GetComponent<Glowing>().RemoveGlow();
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(SmoothReturn(spawnLocalPosition));
    }

    IEnumerator SmoothReturn(Vector3 startPos)
    {
        var currentStart = gameObject.transform.localPosition;
        for (float i = 0; i <= 1.1f; i += 0.1f)
        {
            gameObject.transform.localPosition = Vector3.Lerp(currentStart, startPos, i);
            yield return new WaitForSeconds(0.03f);
        }
        GetComponent<Glowing>().AddGlow();
    }
}
