using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelEnvironment", menuName = "ScriptableObjects/Environment", order = 1)]
public class LevelEnvironment : ScriptableObject
{
    [SerializeField]
    private List<GameObject> _backCity;
    [SerializeField]
    private List<GameObject> _backBuildings;
    [SerializeField]
    private List<GameObject> _backTrees;
    [SerializeField]
    private List<GameObject> _uniq;
    [SerializeField]
    private List<GameObject> _simple;
    [SerializeField]
    private List<GameObject> _trees;
    [SerializeField]
    private List<GameObject> _bushes;
    [SerializeField]
    private List<GameObject> _fence;
    [SerializeField]
    private List<GameObject> _branches;
    [SerializeField]
    private List<GameObject> _coreBuilding;
    [SerializeField]
    private List<GameObject> _clouds;
    [SerializeField]
    private List<GameObject> _roadStuff;
    [SerializeField]
    private GameObject _fakeUniq;
    public List<GameObject> BackCity { get => _backCity; private set => _backCity = value; }
    public List<GameObject> BackBuildings { get => _backBuildings; private set => _backBuildings = value; }
    public List<GameObject> BackTrees { get => _backTrees; private set => _backTrees = value; }
    public List<GameObject> Uniq { get => _uniq; private set => _uniq = value; }
    public List<GameObject> Simple { get => _simple; private set => _simple = value; }
    public List<GameObject> Trees { get => _trees; private set => _trees = value; }
    public List<GameObject> Bushes { get => _bushes; private set => _bushes = value; }
    public List<GameObject> Fence { get => _fence; private set => _fence = value; }
    public List<GameObject> Branches { get => _branches; private set => _branches = value; }
    public List<GameObject> CoreBuilding { get => _coreBuilding; private set => _coreBuilding = value; }
    public List<GameObject> Clouds { get => _clouds; private set => _clouds = value; }
    public List<GameObject> RoadStuff { get => _roadStuff; private set => _roadStuff = value; }
    public GameObject FakeUniq { get => _fakeUniq; private set => _fakeUniq = value; }
}
