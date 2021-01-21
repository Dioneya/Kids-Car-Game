using UnityEngine;
using UnityEngine.UI;

public class VibrationBtn : MonoBehaviour
{
    [SerializeField] private Sprite vibrateOn;
    [SerializeField] private Sprite vibrateOff;

    private Image image;
    void Awake()
    {
        image = GetComponent<Image>();
    }
    void Start()
    {
        ReplaceSpriteAndVibrateStatus();
    }
    public void ChangeMuteStatus()
    {
        Settings.isVibrate = !Settings.isVibrate;
        ReplaceSpriteAndVibrateStatus();
    }

    void ReplaceSpriteAndVibrateStatus()
    {
        image.sprite = Settings.isVibrate ? vibrateOn : vibrateOff;
    }
}
