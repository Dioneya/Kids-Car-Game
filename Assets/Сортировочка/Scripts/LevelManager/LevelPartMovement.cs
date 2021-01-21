using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPartMovement : MonoBehaviour
{
    public LevelMover levelMover;
    float process;
    Vector3 start;
    public Vector3 end;
    bool isDay = true;
    bool isTransparent = false;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidbody;
    bool isBlockDayTurning = false;


    public float parallaxCoef;

    void Start()
    {
        start = transform.position;
        end = transform.position + Vector3.left * transform.position.x * 2;
        if (end.x > -10)
            end = new Vector3(-40,end.x,end.z);
        if(levelMover == null)
            levelMover = FindObjectOfType<LevelMover>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        if(parallaxCoef == 0)
            parallaxCoef = levelMover.ParallaxCoef(spriteRenderer.sortingLayerName);
        else if (parallaxCoef == -1)
        {
            parallaxCoef = 0;
        }
        FastModeChange();
        rigidbody = GetComponent<Rigidbody2D>();


    }

    private void FastModeChange()
    {
        if (levelMover.isDay)
        {
            if (!levelMover.isSwap)
            {
                SwapBack();
            }
            
            var c = spriteRenderer.color;
            c.a = 1;
            spriteRenderer.color = c;

            isDay = true;
        }
        else if (!levelMover.isDay)
        {
            var c = spriteRenderer.color;
            c.a = 0;
            spriteRenderer.color = c;
            
            if (levelMover.isSwap)
            {
                Swap();
            }

            isDay = false;
        }

        if (levelMover.isTransparent)
        {
            var tag = GetComponent<SpriteRenderer>().sortingLayerName;
            if (tag == "BackCity" || tag == "BackBuildings")
            {
                if (transform.childCount > 0)
                    transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
                isBlockDayTurning = true;
                isTransparent = true;
                var c = spriteRenderer.color;
                c.a = 0;
                spriteRenderer.color = c;
            }
        }
    }
    
    void Update()
    {
        if (levelMover.isTransparent && !isTransparent)
        {
            //StopAllCoroutines();
            TurnTransparent();
            isTransparent = true;
        }
        else if (!levelMover.isTransparent && isTransparent)
        {
            //StopAllCoroutines();
            TurnOffTransparent();
            isTransparent = false;
        }

        if (levelMover.isDay && !isDay && !isBlockDayTurning)
        {
            //StopCoroutine("ToNight");
            StartCoroutine(ToDay(0.02f));
            isDay = true;
        }
        else if (!levelMover.isDay && isDay)
        {
            //StopCoroutine("ToDay");
            StartCoroutine(ToNight(0.02f));
            isDay = false;
        }
        //transform.position = Vector3.Lerp(start,end,process);
        //process += parallaxCoef * levelMover.movingSpeed * Time.deltaTime;

        //transform.position = Vector3.Lerp(transform.position,transform.position + Vector3.left , levelMover.movingSpeed * parallaxCoef * Time.deltaTime);
        //transform.position += Vector3.left * levelMover.movingSpeed * parallaxCoef * Time.smoothDeltaTime;
        if(rigidbody != null)
            rigidbody.velocity = Vector3.left * levelMover.movingSpeed * parallaxCoef;

        if (end.x >= transform.position.x)
        {
            Destroy(gameObject);
        }
    }

    private void TurnTransparent()
    {
        var tag = GetComponent<SpriteRenderer>().sortingLayerName;
        if (tag == "BackCity" || tag == "BackBuildings")
        {
            if(transform.childCount > 0)
                transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
            isBlockDayTurning = true;
            StartCoroutine(ToNight(0.005f));
        }
    }

    private void TurnOffTransparent()
    {
        var tag = GetComponent<SpriteRenderer>().sortingLayerName;
        if (tag == "BackCity" || tag == "BackBuildings")
        {
            isBlockDayTurning = false;
            StartCoroutine(ToDay(0.005f));
            if (transform.childCount > 0)
                StartCoroutine(EnableNightSprite());
        }
    }

    IEnumerator EnableNightSprite()
    {
        
        yield return new WaitForSeconds(5f);
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator ToDay(float num)
    {
        //Debug.Log("To day " + gameObject.name);
        //if(spriteRenderer.color.a == 1)
        //    return;
        //StartCoroutine(ChangeDayMode(0, 1, 0.02f));

        if (!levelMover.isSwap)
        {
            SwapBack();
        }

        for (float i = spriteRenderer.color.a; i <= 1+num; i += num)
        {
            var c = spriteRenderer.color;
            c.a = i;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(num);
        }

        
    }

    IEnumerator ToNight(float num)
    {
        //Debug.Log("To night " + gameObject.name);
        //if (spriteRenderer.color.a == 0)
        //    return;
        //StartCoroutine(ChangeDayMode(1,0,-0.2f));

        for (float i = spriteRenderer.color.a; i >= 0-num; i -= num)
        {
            var c = spriteRenderer.color;
            c.a = i;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(num);
        }
        Swap();
        if (levelMover.isSwap)
        {
            Swap();
        }
    }

    private void Swap()
    {
        if(spriteRenderer.sortingLayerName == "BackSky" || spriteRenderer.sortingLayerName == "BackCity" || spriteRenderer.sortingLayerName == "Clouds")
        {
            return;
        }

        spriteRenderer.maskInteraction = SpriteMaskInteraction.VisibleInsideMask;
        var c = spriteRenderer.color;
        c.a = 1;
        spriteRenderer.color = c;
    }

    private void SwapBack()
    {
        if (spriteRenderer.sortingLayerName == "BackSky" || spriteRenderer.sortingLayerName == "BacKCity")
        {
            return;
        }
        var c = spriteRenderer.color;
        c.a = 0;
        spriteRenderer.color = c;
        spriteRenderer.maskInteraction = SpriteMaskInteraction.None;
    }

    IEnumerator ChangeDayMode(float start,float end,float step)
    {
        for (float i = start; i != end; i += step)
        {
            var c = spriteRenderer.color;
            c.a = i;
            spriteRenderer.color = c;
            yield return new WaitForSeconds(0.02f);
        }
    } 


}
