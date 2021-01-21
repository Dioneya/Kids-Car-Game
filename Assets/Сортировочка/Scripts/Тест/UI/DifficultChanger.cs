using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DifficultChanger : MonoBehaviour
{
    [SerializeField] Settings.Difficult difficult;

    public void ChangeDifficult() 
    {
        Settings.difficult = difficult;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
