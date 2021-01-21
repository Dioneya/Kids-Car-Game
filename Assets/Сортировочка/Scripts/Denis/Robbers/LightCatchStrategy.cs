using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightCatchStrategy 
{
    private LevelSettings _levelSettings = LevelSettings.Instance;
    public Vector2 LightSize()
    {
        switch (_levelSettings.difficultLevel)
        {
            case LevelSettings.Difficult.Easy:
                return new Vector2(1.2f, 1.2f);
            case LevelSettings.Difficult.Normal:
                return new Vector2(1, 1);
            case LevelSettings.Difficult.Hard:
                return new Vector2(0.8f, 0.8f);
            default:
                return new Vector2(1, 1);
        }
    }
}
