using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stars : MonoBehaviour
{
    [SerializeField] List<SpriteRenderer> stars;

    int robbersCatched =0;

    public void AddColor(CatchingRobbers.ColorType type)
    {
        if (stars.Count <= robbersCatched)
        {
            Debug.Log("Stars are full");
            return;
        }
        stars[robbersCatched].sprite = Resources.Load<Sprite>("Levels/Police/Stars/" + type);
        robbersCatched++;
    }

    public void Clear()
    {
        var empty = Resources.Load<Sprite>("Levels/Police/Stars/Empty");
        foreach (SpriteRenderer star in stars)
        {
            star.sprite = empty;
        }
        robbersCatched=0;
    }
}
