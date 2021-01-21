using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveEnviroment : MonoBehaviour
{
    [SerializeField] private float[] moveSpeed = new float[3];
    [SerializeField] private GameObject spawnTrigger;
    [SerializeField] private GameObject destroyTrigger;
    [SerializeField] private LvlGenerator generator;
    [SerializeField] private LvlGenerator.Type type;
    public bool isAlwaysMove = false;
    public bool spawn = true;
    
    void Update() 
    {
        if (isAlwaysMove || (LevelManager.isMoved && spawnTrigger!=null)) 
        {
            if (isAlwaysMove && !LevelManager.isMoved)
                transform.localPosition += transform.right * (-moveSpeed[0] / 2) * Time.deltaTime;
            else if (isAlwaysMove && LevelManager.isMoved)
                transform.localPosition += transform.right * -(moveSpeed[LevelManager.lvlSpeed] + moveSpeed[0] / 2) * Time.deltaTime;
            else
                transform.localPosition += transform.right * -moveSpeed[LevelManager.lvlSpeed] * Time.deltaTime;

            if (gameObject.transform.localPosition.x <= spawnTrigger.transform.localPosition.x && spawn) 
            {
                generator.Generate(type);
                spawn = false;
            }

            if (gameObject.transform.localPosition.x <= destroyTrigger.transform.localPosition.x)
                Destroy(gameObject);
                
        }

    }

    public void SetInfo(LvlGenerator _generator, LvlGenerator.Type _type, GameObject _spawnTrigger=null, GameObject _destroyTrigger = null) 
    {
        generator = _generator;
        type = _type;
        spawnTrigger = _spawnTrigger;
        destroyTrigger = _destroyTrigger;
    }

    public void SetSpeed(float low, float middle, float max) 
    {
        moveSpeed[0] = low;
        moveSpeed[1] = middle;
        moveSpeed[2] = max;
    }

    public LvlGenerator GetGenerator() 
    {
        return generator;
    }
}
