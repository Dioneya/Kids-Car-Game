using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSettings : MonoBehaviour
{
    public static LevelSettings Instance { get; private set; }
    public  Difficult difficultLevel;
    public string path; //{ get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public void SetPath(string str)
    {
        path = str;
    }

    public enum Difficult { Easy, Normal, Hard }
}
