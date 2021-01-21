using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CarDetails", menuName = "ScriptableObjects/Car", order = 1)]
public class CarDetails : ScriptableObject
{
    [SerializeField] private List<GameObject> _carParts;
    public List<GameObject> GetCarDetails()
    {
        return _carParts;
    }
}
