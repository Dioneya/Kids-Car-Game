using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class IceCream : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IDropHandler
{
    private bool isInteractive = false;
    public Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector3 coordToMove = new Vector3();
    private Image image;
    // Start is called before the first frame update

    void Awake() 
    {
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void Show() 
    {
        isInteractive = true;
        image.raycastTarget = true;
        coordToMove = transform.position;
    }

    public void Hide() 
    {
        isInteractive = false;
        image.raycastTarget = false;
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

        Color color = image.color;
        color.a = 1;
        image.color = color;
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
        Color color = image.color;
        color.a = 0;
        image.color = color;
    }
}
