using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyGameObject : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 3f);
    }
}
