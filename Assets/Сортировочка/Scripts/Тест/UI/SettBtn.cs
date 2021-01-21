using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettBtn : MonoBehaviour
{
    [SerializeField] private GameObject settingsBg;
    private Animator animatorBg;
    private bool isOpen = false;
    private bool isOpening = false;
    private bool isClose = false;

    private bool isAviable = true;
    public int rotSpeed;
    
    void Awake() 
    {
        animatorBg = settingsBg.GetComponent<Animator>();
    }
    void Start()
    {
        
    }

    public void OpenClose() 
    {
        if (isAviable) 
        {
            if (!isOpen) StartCoroutine(Open());
            else StartCoroutine(Close());
        }
        
    }

    IEnumerator Open() 
    {
        isAviable = false;
        settingsBg.SetActive(true);
        isOpening = true;
        animatorBg.Play("SettingBg");
        yield return new WaitForSeconds(1f);
        isOpening = false;
        isOpen = true;
        isAviable = true;
    }

    IEnumerator Close() 
    {
        isAviable = false;
        isClose = true;
        animatorBg.Play("Close");
        yield return new WaitForSeconds(1f);
        settingsBg.SetActive(false);
        isClose = false;
        isOpen = false;
        isAviable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOpening) gameObject.transform.Rotate(Vector3.back * (LevelManager.lvlSpeed + 1) * rotSpeed * Time.deltaTime);
        if(isClose) gameObject.transform.Rotate(Vector3.forward * (LevelManager.lvlSpeed + 1) * rotSpeed * Time.deltaTime);
    }
}
