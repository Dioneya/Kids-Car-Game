using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayBtn : MonoBehaviour
{
    [SerializeField] private Sprite play, buy;
    private Image image;
    int currentLvl = 1;
    bool isLvlAviable = true;
    private void Awake() {
        image = GetComponent<Image>(); 
    }
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(Click);
    }

    public void CheckAviable(bool isAviable, int lvlIndex)
    {
        image.sprite = isAviable ? play : buy;
        currentLvl = lvlIndex+1;
        isLvlAviable = isAviable;
    }

    public void Click()
    {
        if(isLvlAviable && currentLvl < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(currentLvl);   
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
