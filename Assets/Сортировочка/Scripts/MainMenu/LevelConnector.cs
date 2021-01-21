using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelConnector : MonoBehaviour
{
    [SerializeField] private int numOfLvl;
    private LevelSettings settings;


    private void Start()
    {
        settings = FindObjectOfType<LevelSettings>();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(numOfLvl);
    }
}
