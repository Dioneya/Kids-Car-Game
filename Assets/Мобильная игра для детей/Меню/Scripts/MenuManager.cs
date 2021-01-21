using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Character tourist;
    [SerializeField] private Character police;
    [SerializeField] private GameObject markerLeft;
    [SerializeField] private GameObject markerRight;
    // Start is called before the first frame update
    void Start()
    {
        tourist.GoTo(markerLeft.gameObject.transform.position);
        police.GoTo(markerRight.gameObject.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
