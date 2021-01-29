using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class kid : MonoBehaviour, IDropHandler
{
    #region Переменные
    private bool isMove = false, 
                 isMoveBack = false, 
                 isInteractive = false;

    private Vector3 coordToMove = new Vector3(), 
                    spawnCoord = new Vector3();
    private Animator animator;
    [SerializeField] GameObject cloud, 
                                iceCream;

    [SerializeField] Sprite[] iceCreams;
    private string[] names = new string[4] {"Twisted", "Chocolate", "Horn", "Ball" };
    private string cloudIceCreamName = "";
    AudioSource source;

    [SerializeField]
    private AudioClip cry, 
                      happy;
    #endregion
    void Awake() 
    {
        animator = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Start() 
    {
        spawnCoord = transform.position;
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
            StartCoroutine(CorrectPick());
        }
        else 
        {
            animator.Play("cry");
            PlayCrySound();
        }
    }
    IEnumerator CorrectPick() 
    {
        GetComponent<Animator>().Play("happy");
        PlayHappySound();
        yield return new WaitForSeconds(0.55f);
        GetComponent<SpriteRenderer>().flipX = true;
        MoveToBack();
        transform.parent.GetComponent<FeedKids>().KidFeeded();
    }

    void PlayHappySound() 
    {
        source.clip = happy;
        source.Play();
    }
    void PlayCrySound() 
    {
        source.clip = cry;
        source.Play();
    }

    public void MoveTo(Vector3 toMove)
    {
        isMove = true;
        animator.Play("Run");
        coordToMove = toMove;
    }
    public void MoveToBack()
    {
        isMoveBack = true;
        animator.Play("Run");
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
        
        if(isMoveBack)
            transform.position += Vector3.left * 3f * Time.deltaTime;
        if (isMoveBack && transform.position.x <= spawnCoord.x)
        {
            isMoveBack = false;
            animator.Play("Idle");
            Destroy(this.gameObject);
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
