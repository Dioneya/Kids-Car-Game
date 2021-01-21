using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunOnBackground : MonoBehaviour
{
    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            Application.runInBackground = true;
        }
        else
        {
            Application.runInBackground = true;
        }
    }
}
