using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMover : MonoBehaviour
{
    public float movingSpeed { get; private set; }
    public bool isDay { get; private set; }
    public bool isTransparent { get; private set; }
    public bool isToogleBlocked = false;
    public bool isSwap { get; private set; }
    private bool cashedIsDay = true;

    public event System.Action OnDay;
    public event System.Action OnNight;
    private void Start()
    {
        isTransparent = false;
        isDay = true;
        isSwap = false;
    }
    public void SetSpeed(float speed)
    {
        movingSpeed = speed;
    }

    public float ParallaxCoef(string name)
    {
        if (name == "BackCity")
        {
            //return 0.5f;
            return 0f;
        }
        if (name == "BackBuildings")
        {
            return 0.75f;
        }
        if (name == "Road" || name == "BackSky")
        {
            return 0;
        }

        return 1;
    }

    public void TurnTransparent()
    {
        isTransparent = true;
    }

    public void TurnOffTransparent(float afterSec)
    {
        StartCoroutine(OffTransparent(afterSec));
    }

    IEnumerator OffTransparent(float sec)
    {
        yield return new WaitForSeconds(sec);
        isTransparent = false;
    }

    public void ReturnCashedDay()
    {
        if (cashedIsDay)
        {
            TurnDay(false);
        }
        else
        {
            TurnNight(false);
        }
    }

    public bool TurnDay(bool isButtonCalled)
    {
        if (isToogleBlocked)
            return false;

        if (isButtonCalled)
        {
            cashedIsDay = true;
            StartCoroutine(Cooldown());
        }

        OnDay?.Invoke();
        isDay = true;
        return true;
    }

    public bool TurnNight(bool isButtonCalled)
    {

        if (isToogleBlocked)
            return false;

        if (isButtonCalled)
        {
            cashedIsDay = false;
            StartCoroutine(Cooldown());
            OnNight?.Invoke();
        }

        OnNight?.Invoke();
        isDay = false;
        return true;
    }

    public void SwapLayers()
    {
        isSwap = true;
    }

    public void SwapBackLayers()
    {
        isSwap = false;
    }

    IEnumerator Cooldown()
    {
        isToogleBlocked = true;
        yield return new WaitForSeconds(3f);
        isToogleBlocked = false;
    }
}
