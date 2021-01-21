using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelRotate : MonoBehaviour
{
    public int rotSpeed;
    [SerializeField] GameObject wheel1;
    [SerializeField] GameObject wheel2;

    void Update()
    {
        wheel1.transform.Rotate(Vector3.back * (LevelManager.lvlSpeed+1) * rotSpeed * Time.deltaTime);
        wheel2.transform.Rotate(Vector3.back * (LevelManager.lvlSpeed+1) * rotSpeed * Time.deltaTime);
    }
}
