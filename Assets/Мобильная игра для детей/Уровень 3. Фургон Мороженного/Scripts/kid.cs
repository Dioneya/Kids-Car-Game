using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class kid : MonoBehaviour, IDropHandler
{
    private bool isMove = false;
    private bool isInteractive = false;
    private Vector3 coordToMove = new Vector3();
    private Animator animator;
    [SerializeField] GameObject cloud;
    [SerializeField] GameObject iceCream;
    [SerializeField] Sprite[] iceCreams;
    private string[] names = new string[4] {"Twisted", "Chocolate", "Horn", "Ball" };
    private string cloudIceCreamName = "";
    void Awake() 
    {
        animator = GetComponent<Animator>();
    }
    public void OnDrop(PointerEventData eventData)
    {
        if (!isInteractive)
            return;
        if (eventData.pointerDrag.name == cloudIceCreamName)
        {
            HideCloud();
            Color color = Color.white;
            color.a = 0;
            eventData.pointerDrag.gameObject.GetComponent<Image>().color = color;
        }
    }

    public void MoveTo(Vector3 toMove)
    {
        isMove = true;
        animator.Play("Run");
        coordToMove = toMove;
    }
    void Update()
    {
        if (isMove)
            transform.position += Vector3.right * 3f * Time.deltaTime;
        if (isMove && transform.position.x >= coordToMove.x)
        {
            isMove = false;
            animator.Play("Idle");
            GenerateCloud();
        }
    }

    void GenerateCloud() 
    {
        isInteractive = true;
        int rnd = Random.Range(0,4);
        cloud.SetActive(true);
        cloudIceCreamName = names[rnd];
        iceCream.GetComponent<SpriteRenderer>().sprite = iceCreams[rnd];
    }

    void HideCloud() 
    {
        isInteractive = false;
        cloud.SetActive(false);
    }
}
