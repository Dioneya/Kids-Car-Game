using System.Collections;
using UnityEngine;

public class CarouselItem : MonoBehaviour
{
    [SerializeField] private CarouselItem prevNode;
    [SerializeField] private CarouselItem nextNode;
    private GameObject car;
    private GameObject platform;
    private int id;
    bool isStop = true;
    void Awake() 
    {
        car = transform.GetChild(0).gameObject;
        platform = car.transform.GetChild(0).gameObject;
    }
    public void SetRelatedNodes(CarouselItem _prevNode, CarouselItem _nextNode, int _id) //Установка соседних нодов
    {
        prevNode = _prevNode;
        nextNode = _nextNode;
        id = _id;
    }
    public void SwitchNext()
    {
        Vector3 currentPos = gameObject.transform.localPosition;
        Vector3 currentScale = gameObject.transform.localScale;
        
        StartCoroutine(SmoothPosition(currentPos, prevNode.gameObject.transform.localPosition, 0.005f));
        StartCoroutine(SmoothScale(currentScale, prevNode.gameObject.transform.localScale, 0.005f));
    }

    public void SwitchPrev()
    {
        Vector3 currentPos = gameObject.transform.localPosition;
        Vector3 currentScale = gameObject.transform.localScale;

        StartCoroutine(SmoothPosition(currentPos, nextNode.gameObject.transform.localPosition, 0.005f));
        StartCoroutine(SmoothScale(currentScale, nextNode.gameObject.transform.localScale, 0.005f));
    }

    public void SwitchToClicked(int cnt, bool isNext) 
    {
        if(isNext)
            StartCoroutine(MoveTo(cnt,isNext));
        else
            StartCoroutine(MoveTo(cnt,isNext));
    }

    public void AddGlow() 
    {
        car.GetComponent<Glowing>().AddGlow();
        platform.GetComponent<Glowing>().AddGlow();
    }
    public void RemoveGlow() 
    {
        car.GetComponent<Glowing>().RemoveGlow();
        platform.GetComponent<Glowing>().RemoveGlow();
    }

    private void OnMouseUp() 
    {
        transform.GetComponentInParent<CarouselView>().SwitchToClickedItem(id);
    }


    #region Плавное перемещение и зум
    IEnumerator SmoothScale(Vector3 startScale, Vector3 endScale,float time, float speed = 0.05f)
    {
        isStop = false;
        for (float i = 0; i < 1.1f; i += speed)
        {
            yield return new WaitForSeconds(time);
            gameObject.transform.localScale = Vector3.Lerp(startScale, endScale, i);
        }
        isStop = true;
    }
    IEnumerator SmoothPosition(Vector3 startPos, Vector3 endPos, float time, float speed = 0.05f)
    {
        for (float i = 0; i < 1.1f; i += speed)
        {
            yield return new WaitForSeconds(time);
            gameObject.transform.localPosition = Vector3.Lerp(startPos, endPos, i);
        }
    }

    IEnumerator MoveTo(int cnt, bool isNext) 
    {
        for (int k = 0; k < cnt; k++) 
        {
            Vector3 currentPos = gameObject.transform.localPosition;
            Vector3 currentScale = gameObject.transform.localScale;

            if (isNext)
            {
                StartCoroutine(SmoothPosition(currentPos, prevNode.gameObject.transform.localPosition,0.0001f, 0.05f));
                StartCoroutine(SmoothScale(currentScale, prevNode.gameObject.transform.localScale, 0.0001f,0.05f));
            }
            else 
            {
                StartCoroutine(SmoothPosition(currentPos, nextNode.gameObject.transform.localPosition, 0.0001f,.05f));
                StartCoroutine(SmoothScale(currentScale, nextNode.gameObject.transform.localScale, 0.0001f, .05f));
            }
            yield return new WaitWhile(()=>!isStop);
        }
        
    } 
    #endregion
}
