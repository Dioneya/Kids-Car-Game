using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    public enum Difficult { Easy, Medium, Hard };

    public static Difficult difficult = Difficult.Easy;
    public static bool isMuted = false;
    public static bool isVibrate = true;
    
}
