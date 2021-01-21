using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Donut : MonoBehaviour
{
    [SerializeField] private LevelGenerator levelGenerator;
    LevelSpeed levelSpeed;

    private void Start()
    {
        levelSpeed = GetComponent<LevelSpeed>();
        levelGenerator = GetComponent<LevelGenerator>();
    }
    public void GenerateDonutBuilding()
    {
        levelGenerator.SpawnConcreteUniq("осн.кафе");//SpawnExceptUniq(new string[] { "осн.техника", "", "", ""});
        levelSpeed.SetSpeedLevel(2,true);
    }
}
