using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string nameOfCorrectDetail;
    public bool isEmpty = true;
    public IInteractiveAction interactive;
    [SerializeField] private DetailsSpawner spawner;

    void Awake() 
    {
        interactive = GetComponent<IInteractiveAction>();
    }
    public void OnDrop(PointerEventData eventData) 
    {
        Debug.Log("On Drop");
        bool isCorrectDetail = eventData.pointerDrag.GetComponent<Detail>().nameOfDetail == nameOfCorrectDetail;
        Debug.Log(isCorrectDetail);
        if (eventData.pointerDrag != null && isEmpty) 
        {
            if (isCorrectDetail && !eventData.pointerDrag.GetComponent<DragAndDropV2>().isLocked)
            {
                HandTips.time = 0;
                HandTips.faildCnt = 0;
                if (Settings.isVibrate) 
                {
                    //Handheld.Vibrate();
                    Vibration.Vibrate(100);
                    Debug.Log("Вибрануло");
                } 
                var obj = eventData.pointerDrag;
                var detail = obj.GetComponent<Detail>();

                obj.GetComponent<DragAndDropV2>().isLocked = true;
                GetComponent<Image>().sprite = obj.GetComponent<Image>().sprite;
                GetComponent<Image>().color = obj.GetComponent<Image>().color;
                spawner.Generate(detail.spawnLocalPosition);
                isEmpty = false;


                detail.PlayPutSound();
                transform.parent.GetComponent<CarV2>().CheckForCarAvaible();
                /*if (gameObject.name == "kuzov")
                    transform.SetSiblingIndex(transform.GetSiblingIndex() - 12);*/

                GetComponent<Image>().raycastTarget = false;

                if (interactive != null)
                    interactive.Action();
            }

            else 
            {
                WrongDetail(eventData.pointerDrag);
            }
        }
    }

    private void WrongDetail(GameObject obj) 
    {
        var detail = obj.GetComponent<Detail>();
        detail.PlayErrorSound();
    }
}
