using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICarParts
{
     bool TryToSetUpDetail(GameObject detail, float distanceToSetUp);
     bool IsInCarBase(Vector3 position);
    GameObject FindCarPartByDetail(GameObject detail);
}

