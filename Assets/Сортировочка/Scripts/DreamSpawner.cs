using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamSpawner : MonoBehaviour
{
    [SerializeField] GameObject dream;
    public bool isBlocked = true;
    float timer = 120;

    private void Update()
    {
        if (!isBlocked )
        {
            if(timer < 0)
            {
                EnableDream();
                timer = Random.Range(40,60);
            }
            timer -= Time.deltaTime;
        }

    }

    public void Block()
    {
        isBlocked = true;
        dream.SetActive(false);
    }

    public void UnBlock(bool lessTimeTimer)
    {
        isBlocked = false;
        timer = Random.Range(40,60);
        if(lessTimeTimer)
            timer = Random.Range(15, 30);
    }

    public void EnableDream()
    {
        dream.SetActive(true);
        StartCoroutine(StayTime());
    }

    public void DisableDream()
    {
        dream.SetActive(false);
    }

    IEnumerator StayTime()
    {
        yield return new WaitForSeconds(15);
        DisableDream();
    }
}
