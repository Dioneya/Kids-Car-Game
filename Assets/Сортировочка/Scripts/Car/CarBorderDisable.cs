using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarBorderDisable : MonoBehaviour
{
    [SerializeField] private GameObject _myPartBorder;
    public void DisableBorder()
    {
        _myPartBorder.SetActive(false);
    }
}
