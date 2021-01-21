using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public bool isOnlyMoving = false;
    private Vector3 startPos;
    PartSettings settings;
    int startOrderInLayer;
    string startSortingLayer;
    SpriteRenderer renderer;
    SpriteMaskInteraction cashedMask;
    private bool _isCanDrag = true;

    public bool IsCanDrag { get => _isCanDrag; set => _isCanDrag = value; }

    private void Start()
    {
        
        renderer = GetComponent<SpriteRenderer>();
        settings = GetComponent<PartSettings>();
        if (renderer == null)
            renderer = GetComponentInChildren<SpriteRenderer>();
        if (settings== null)
            settings = GetComponentInChildren<PartSettings>();

        startOrderInLayer = renderer.sortingOrder;
        startSortingLayer = renderer.sortingLayerName;
    }

    public void StartMoving()
    {
        if(startPos == Vector3.zero)
        {
            startPos = transform.position;
        }
        transform.localScale = Vector3.one;
        renderer.sortingOrder = 10;
        renderer.sortingLayerName = "Default";
        cashedMask = renderer.maskInteraction;
        renderer.maskInteraction = SpriteMaskInteraction.None;
    }

    public void FinishMoving()
    {
        settings.PlayPut();
        renderer.sortingOrder = startOrderInLayer;
        gameObject.SetActive(false);
        Destroy(gameObject);
    }

    public void ReturnToStartPosition()
    {
        StartCoroutine(SmoothReturn());
        renderer.sortingLayerName = startSortingLayer;
        renderer.sortingOrder = startOrderInLayer;
        renderer.maskInteraction = cashedMask;

        if (settings != null)
        {
            transform.localScale = settings.smallSize;
        }
        
    }

    IEnumerator SmoothReturn()
    {
        var currentStart = transform.position;

        for(float i = 0;i<=1.1f;i+=0.1f)
        {
            transform.position = Vector3.Lerp(currentStart,startPos,i);
            yield return new WaitForSeconds(0.03f);
        }
    }
}
