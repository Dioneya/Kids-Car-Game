using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class IceCreamBox : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private bool isMove = false;
    private bool isInteractive = false;
    private Vector3 coordToMove = new Vector3();
    public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isMove)
            transform.position += Vector3.right * 1f * Time.deltaTime;
        if (transform.position.x >= coordToMove.x) 
        {
            isMove = false;
            isInteractive = true;
        }
            
    }

    public void MoveTo(Vector3 toMove) 
    {
        isMove = true;
        coordToMove = toMove;
    }

    public void OnDrop(PointerEventData eventData)
    {
        if (!isInteractive)
            return;
        throw new System.NotImplementedException();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!isInteractive)
            return;
        transform.parent = LevelManager.GetLevelManager().GetRider().transform.parent.transform.parent;
        canvasGroup.blocksRaycasts = false;
        GetComponent<Glowing>().RemoveGlow();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isInteractive)
            return;
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isInteractive)
            return;
        transform.parent = canvas.transform;
        canvasGroup.blocksRaycasts = true;
        StartCoroutine(SmoothReturn(coordToMove));
    }

    IEnumerator SmoothReturn(Vector3 startPos)
    {
        var currentStart = gameObject.transform.position;
        for (float i = 0; i <= 1.1f; i += 0.1f)
        {
            gameObject.transform.position = Vector3.Lerp(currentStart, startPos, i);
            yield return new WaitForSeconds(0.03f);
        }
        GetComponent<Glowing>().AddGlow();
    }
}
