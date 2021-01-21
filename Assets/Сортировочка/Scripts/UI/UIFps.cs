using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIFps : MonoBehaviour
{
    Text text;
    public float deltaTime;
    float minFps = 60;
    float timer = 0;
    float timeStamp = 10;
    float sumFrames = 60;
    int numFrames = 1;
    float avgFps = 60;


    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        var fps = 1.0f / deltaTime;
        fps = Mathf.Ceil(fps);
        if (fps < minFps)
            minFps = fps;

        if(timer >= timeStamp)
        {
            timer = 0;
            avgFps = sumFrames / numFrames;
            sumFrames = 0;
            numFrames = 0;
        }

        timer += Time.deltaTime;
        sumFrames += fps;
        numFrames++;

        text.text = "FPS: " + fps + " Avg: " + avgFps + " Min: " + minFps;
    }
}
