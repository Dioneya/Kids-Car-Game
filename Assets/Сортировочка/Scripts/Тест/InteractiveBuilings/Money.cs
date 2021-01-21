using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Money : MonoBehaviour
{
    private void OnEnable() 
    {
        GetComponent<AudioSource>().Play();
    }
}
