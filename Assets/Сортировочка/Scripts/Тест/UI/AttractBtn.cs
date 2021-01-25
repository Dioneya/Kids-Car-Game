using UnityEngine;
using UnityEngine.UI;

public class AttractBtn : MonoBehaviour
{
    [SerializeField] private LvlGenerator generator;
    [SerializeField] private GameObject gameplayBtn;
    [SerializeField] private string nameOfAttract;
    [SerializeField] private bool isMoveToCenter = false;
    private Button btn;
    void Awake()
    {
        btn = GetComponent<Button>();
        btn.onClick.AddListener(OnBtnClicked);
    }
    public void OnBtnClicked()
    {
        generator.AddInteractiveToQueue(nameOfAttract);
        gameplayBtn.SetActive(false);
        if(isMoveToCenter)
            LevelManager.GetLevelManager().car.GetComponent<CarV2>().MoveToCenter();
    }
}
