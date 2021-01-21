using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractiveCloud : MonoBehaviour
{
    [SerializeField] private List<Sprite> CloudsSprite = new List<Sprite>();
    [SerializeField] private List<string> GenerateObjName;
    [SerializeField] private LvlGenerator generator;

    private bool isGenerated = false;

    private Dictionary<string, Sprite> dict;
    private Image image;

    private int currentIndex;
    void Awake() 
    {
        image = GetComponent<Image>();
    }
    void Start() 
    {
        image.enabled = false;
        GetComponent<Button>().onClick.AddListener(CloudClick);
    }
    void Update() 
    {
        if (!LevelManager.isInteract && !isGenerated && LevelManager.isMoved)
            StartCoroutine(Generate());

    }

    IEnumerator Generate() 
    {
        isGenerated = true;
        yield return new WaitForSeconds(10f);
        if (!LevelManager.isInteract && LevelManager.isMoved)
            StartCoroutine(CloudGenerateInteractive());
    }
    IEnumerator CloudGenerateInteractive() 
    {
        currentIndex = Random.Range(0, CloudsSprite.Count);
        image.sprite = CloudsSprite[currentIndex];
        image.enabled = true;
        yield return new WaitForSeconds(5f);
        image.enabled = false;

        isGenerated = false;
    }

    private void CloudClick() 
    {
        generator.GenerateInteractive(GenerateObjName[currentIndex]);
        //LevelManager.isInteract = true;
        image.enabled = false;
    }
}
