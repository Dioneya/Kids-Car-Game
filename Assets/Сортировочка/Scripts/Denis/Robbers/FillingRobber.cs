using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingRobber : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Sprite _defaultSprite;
    [SerializeField] private Sprite _cathSprite;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private MovableObject _dragAndDrop;
    [SerializeField] private CatchingRobbers _catchingRobbers;
    [SerializeField] private LightCatchRobbers _catchRobbers;
    [SerializeField] private bool isFind;
    [SerializeField] private Transform _endPosition;
    [SerializeField] private Hints _hints;
   
    private PlayRobbesAnimations _robbesAnimations;
    public PlayRobbesAnimations RobbesAnimations => _robbesAnimations;
    [SerializeField] private RobbersSound _robbersSound;
    public RobbersSound RobbersSound => _robbersSound;
    public MovableObject DragAndDrop { get => _dragAndDrop; set => _dragAndDrop = value; }
    public LightCatchRobbers CatchRobbers  => _catchRobbers;
    public Sprite DefaultSprite { get => _defaultSprite; private set => _defaultSprite = value; }
    public Sprite CathSprite { get => _cathSprite; private set => _cathSprite = value; }
    public bool IsFind { get => isFind; set => isFind = value; }
    public Hints Hints { get => _hints; set => _hints = value; }
    public Transform EndPosition { get => _endPosition; set {
            _endPosition = value;
            InitHints();
    } }

    public void InitHints()
    {
        _hints.StartPosition = transform;
        _hints.EndPosition = _endPosition;
    }
    private void Awake()
    {
        _robbersSound = FindObjectOfType<RobbersSound>();
        _hints = FindObjectOfType<Hints>();
        _robbesAnimations = new PlayRobbesAnimations(_animator);
        _catchingRobbers = FindObjectOfType<CatchingRobbers>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _defaultSprite = _spriteRenderer.sprite;
    }
    private void Start()
    {
        _dragAndDrop = GetComponent<MovableObject>();
    }
    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
    }
    private void OnEnable()
    {
        _catchRobbers = _catchingRobbers.light.GetComponentInChildren<LightCatchRobbers>();
        _catchRobbers.Collider2D.enabled = true;
    }
    private IEnumerator WaitEnableColader()
    {
        yield return new WaitForSeconds(0.8f);
        _catchRobbers.Collider2D.enabled = true;
    }
    public void WaitTimeToEnableColider()
    {
        StartCoroutine(WaitEnableColader());
    }
}
