using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandTips : MonoBehaviour
{
    public static int faildCnt = -1;
    private Detail lastPickedDetail;
    [SerializeField] private CarV2 car;
    [SerializeField] private GameObject GeneratorDetails;
    public static int time = 0;

    void Start() 
    {
        time = 0;
        StartCoroutine(WaitForHelp());
    }
    private IEnumerator isFaild(Detail detail) 
    {
        /*if (lastPickedDetail != null && lastPickedDetail.nameOfDetail == detail.nameOfDetail) 
        {
            faildCnt++;
        } 
        else 
        {
            lastPickedDetail = detail;
            faildCnt = 0;
        }*/
        yield return new WaitForSeconds(0.2f);
        faildCnt++;
    }

    IEnumerator WaitForHelp()
    {
        while (!LevelManager.isStart) 
        {
            yield return new WaitForSeconds(1f);
            time++;
            //Debug.LogWarning(time);
            if (time>=20) 
            {
                Detail[] details = GeneratorDetails.GetComponentsInChildren<Detail>();
                StartCoroutine(Glow(details[0].gameObject, FindDetailSlot(details[0])));
                StartCoroutine(MoveHand(FindDetailCoord(details[0]),details[0].spawnPosition));
                time = 0;
            }
        }
    }

    public void CheckForHelp(Detail detail) 
    {
        StartCoroutine(isFaild(detail));
        if (faildCnt == 3) 
        {
            DragAndDropV2 drop = detail.gameObject.GetComponent<DragAndDropV2>();
            
            Vector3 slotPos = FindDetailCoord(detail);
            Vector3 detailPos = detail.spawnPosition;

            StartCoroutine(Glow(detail.gameObject, FindDetailSlot(detail)));
            StartCoroutine(MoveHand(slotPos, detailPos));
        }
    }

    private Vector3 FindDetailCoord(Detail detail) 
    {
        foreach (ItemSlot itemSlot in car.items)
        {
            if (itemSlot.isEmpty && itemSlot.name == detail.nameOfDetail)
            {
                return itemSlot.gameObject.transform.position;
            }
        }
        return new Vector3();
    }

    private GameObject FindDetailSlot(Detail detail) 
    {
        foreach (ItemSlot itemSlot in car.items)
        {
            if (itemSlot.isEmpty && itemSlot.name == detail.nameOfDetail)
            {
                return itemSlot.gameObject;
            }
        }
        return null;
    }

    private IEnumerator MoveHand(Vector3 slotPos, Vector3 detailPos)
    {

        Debug.Log(slotPos + " " + detailPos);

        LockDetails(true);
        yield return new WaitForSeconds(0.6f);

        gameObject.GetComponent<Image>().enabled = true;
        gameObject.transform.position = detailPos;
        for (float i = 0; i < 1.1f; i += 0.01f)
        {
            yield return new WaitForSeconds(0.03f);
            gameObject.transform.position = Vector3.Lerp(transform.position, slotPos, i);
        }
        gameObject.GetComponent<Image>().enabled = false;

        LockDetails(false);
        faildCnt = 0;
    }

    IEnumerator Glow(GameObject detail, GameObject item) 
    {
        Glowing glDetail = detail.GetComponent<Glowing>();
        GameObject child = item.transform.GetChild(0).gameObject;
        child.SetActive(true);

        glDetail.AddGlow();

        yield return new WaitForSeconds(4f);

        glDetail.RemoveGlow();
        child.SetActive(false);

    }

    private void LockDetails(bool isLock) 
    {
        DragAndDropV2[] details = GeneratorDetails.GetComponentsInChildren<DragAndDropV2>();

        foreach (DragAndDropV2 drag in details) 
        {
            drag.isLocked = isLock;
        }
    }
}
