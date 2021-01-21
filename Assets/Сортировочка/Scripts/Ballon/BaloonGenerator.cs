using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaloonGenerator : MonoBehaviour
{
    public string pathStars;
    public string pathSimple;
    public string pathUniq;


    public Sprite[] starSprites;
    public Sprite[] simpleSprites;
    public Sprite[] uniqSprites;

    public event System.Action OnTimeFinihed;

    [SerializeField] GameObject balloonPrefab;
    [SerializeField] AudioClip simpleSound;
    [SerializeField] AudioClip starSound;
    [SerializeField] AudioClip extraSound;
    private float timer;
    private float globalTimer = 20;
    private UI ui;
    private Balloon lastBalloon;
    private int currentAmount;

    void Awake()
    {
        ui = FindObjectOfType<UI>();
        starSprites = Resources.LoadAll<Sprite>(pathStars);
        simpleSprites = Resources.LoadAll<Sprite>(pathSimple);
        uniqSprites = Resources.LoadAll<Sprite>(pathUniq);
        enabled = false;
    }

    private void OnEnable()
    {
        globalTimer = 15f;
        //ui.TurnWinBack();
        StartCoroutine(Timer());
    }

    private void OnDisable()
    {
        OnTimeFinihed?.Invoke();
        //ui.OffWinBack();
    }

    void Update()
    {
        if (timer <= 0 && globalTimer > 0)
        {
            var rand = Random.Range(-8f, 8f);
            timer = Random.Range(0.1f, 0.7f);
            var b = Instantiate(balloonPrefab, transform.position + Vector3.left * rand, Quaternion.identity);
            b.transform.parent = this.gameObject.transform;
            currentAmount++;

            var isUniq = Random.Range(0,3);
            Sprite sprite;
            AudioClip clip;
            if (isUniq==0)
            {
                sprite = uniqSprites[Random.Range(0, uniqSprites.Length)];
                b.transform.localScale = new Vector3(.35f,.35f,1f);
                clip = extraSound;
            }
            else if(isUniq ==1)
            {
                sprite = starSprites[Random.Range(0, starSprites.Length)];
                clip = starSound;
            }
            else
            {
                sprite = simpleSprites[Random.Range(0, simpleSprites.Length)];
                clip = simpleSound;
            }
            b.GetComponent<SpriteRenderer>().sprite = sprite;
            lastBalloon = b.GetComponent<Balloon>();
            lastBalloon.clip = clip;
            lastBalloon.OnBurst += HandleBalloonBurst;
            lastBalloon.movingSpeed = Random.Range(1.25f,4f);
        }

        if(globalTimer <= 0)
        {
            //if(lastBalloon == null)
            //{
            //    DisableAfter();
            //}
            //lastBalloon.OnBurst += DisableAfter;
        }
        timer -= Time.deltaTime;
    }
    private IEnumerator Timer()
    {
        while (globalTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            globalTimer--;
            if (globalTimer == 0)
            {
                StopCoroutine(Timer());
            }
        }
    }

    private void HandleBalloonBurst()
    {
        currentAmount--;
        if (currentAmount == 0)
        {
            DisableAfter();
        }
    }

    private void DisableAfter()
    {
        enabled = false;
    }
}
