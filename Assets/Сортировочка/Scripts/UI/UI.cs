using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject gamePlayUI;
    [SerializeField] GameObject winBack;
    UIDarking darking;

    private void Start()
    {
        darking = GetComponentInChildren<UIDarking>();
    }

    public void DarkScreen()
    {
        darking.Dark();
    }

    public void RemoveDark()
    {
        darking.Remove();
    }

    public void TurnOnGameplayUI(float sec)
    {
        StartCoroutine(OnGameplayUIAfterSeconds(sec));
    }

    public void TurnOffGameplayUI()
    {
        gamePlayUI.SetActive(false);
    }

    public void TurnWinBack()
    {
        winBack.SetActive(true);
    }

    public void OffWinBack()
    {
        winBack.SetActive(false);
    }

    IEnumerator OnGameplayUIAfterSeconds(float sec)
    {
        yield return new WaitForSeconds(sec);
        gamePlayUI.SetActive(true);
    }
}
