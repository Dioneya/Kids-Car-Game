using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIActions : MonoBehaviour
{
    WinkerAction winker;
    bool isWrincleActive = false;
    [SerializeField] Image wrinkleButton;
    [SerializeField] Sprite enabledWrinkle;
    [SerializeField] Sprite disabledWrinkle;

    private void Start()
    {
        winker = FindObjectOfType<WinkerAction>();
        if (winker != null)
        {
            winker.OnWinkerOff += SetOffSpriteWinker;
        }
    }

    public void WrinkleClicked()
    {
        if (isWrincleActive)
        {
            winker.TurnOff();
            isWrincleActive = false;
            wrinkleButton.sprite = disabledWrinkle;
            
        }
        else
        {
            winker.TurnOn();
            isWrincleActive = true;
            wrinkleButton.sprite = enabledWrinkle;
        }
    }

    private void SetOffSpriteWinker()
    {
        isWrincleActive = false;
        wrinkleButton.sprite = disabledWrinkle;
    }
}
