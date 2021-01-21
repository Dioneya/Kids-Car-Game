using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBorders : MonoBehaviour
{
    private LevelSettings _levelSettings;
    public void DisableChildObject(CarBorderDisable carBorderDisable)
    {
        for (int i = 0; i<transform.childCount;i++)
        {
            var obj = transform.GetChild(i).gameObject;
            if (_levelSettings.difficultLevel == LevelSettings.Difficult.Hard)
            {
                obj.SetActive(false);
            }
            else
            {
                carBorderDisable.DisableBorder();
                return;
            }
        }
    }
    private void Start()
    {
        _levelSettings = LevelSettings.Instance;
        HardLevel();
    }
    private void HardLevel()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var obj = transform.GetChild(i).gameObject;
            switch (_levelSettings.difficultLevel)
            {
                case LevelSettings.Difficult.Hard:
                    obj.SetActive(false);
                    break;
            }
        }
    }
}
