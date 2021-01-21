using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    public event System.Action OnBurst;
    public float movingSpeed;
    public AudioClip clip;
    private AudioSource source;
    private bool isUniq; 

    private void Start()
    {
        var rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.up * movingSpeed;
        rb.gravityScale = 0;
        source = GetComponent<AudioSource>();
        StartCoroutine(LifeTime());
    }

    IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(10 / movingSpeed * 2);
        OnBurst?.Invoke();
        Destroy(gameObject);
    }

    public void Burst()
    {
        OnBurst?.Invoke();
        StartCoroutine(Bursting());
    }

    IEnumerator Bursting()
    {
        source.clip = clip;
        source.Play();
        GetComponent<SpriteRenderer>().sprite = null;
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }

    void OnMouseDown() 
    {
        Burst();
    }
}
