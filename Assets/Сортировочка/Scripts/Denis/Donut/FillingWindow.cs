using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillingWindow : MonoBehaviour
{
    [SerializeField] private AudioSource _buySound;
    [SerializeField] private GameObject _money;
    [SerializeField] private SpriteRenderer _donut;
    [SerializeField] private Animator _animator;
    [SerializeField] private Collider2D _windowCollider;
    [SerializeField] private Vector3 positionDonut;
    [SerializeField] private CarClick carClick;
    [SerializeField] private Hints _hints;
    [SerializeField] private DreamSpawner dreamSpawner;

    private IWindowState _state;
    public AudioSource BuySound { get => _buySound; set => _buySound = value; }
    public IWindowState State { get => _state; set => _state = value; }
    public SpriteRenderer Donut { get => _donut; set => _donut = value; }
    public Collider2D WindowCollider { get => _windowCollider; set => _windowCollider = value; }
    public Vector3 PositionDonut { get => positionDonut; set => positionDonut = value; }
    public CarClick CarClick { get => carClick; set => carClick = value; }
    public DreamSpawner DreamSpawner { get => dreamSpawner; set => dreamSpawner = value; }

    private bool firstPrompt;
    private bool secondPrompt;
    private bool threePrompt;

    private void Start()
    {
        dreamSpawner = FindObjectOfType<DreamSpawner>();
        carClick = FindObjectOfType<CarClick>();
        carClick.Init(dreamSpawner);
        _hints = FindObjectOfType<Hints>();
    }
    public void ShowAnimation(bool state)
    {
        _animator.SetBool("On", state);
    }
    public void CreateMoney() 
    {
        Instantiate(_money, Vector3.zero, Quaternion.identity);
    }
    public void NotActive()
    {
        _state = new NotActive();
        _state.Active(this);
        StartFirstPrompt();
        _state = new WindowActive();
    }
    public void EatDonut()
    {
        _state = new NotActive();
        _state.Active(this);
        StartThreePrompt();
          _state = new WindowActive();
    }
    public void Active()
    {
        _state = new WindowActive();
        _state.Active(this);
        _state = new NotActive();
    }
    private IEnumerator FirstPrompt()
    {
        yield return new WaitForSeconds(20);
        if (firstPrompt)
        {
            _hints.MoveHand(transform, transform);
            _hints.TransformHandSize(new Vector2(0.8f, 0.8f));
        }yield return null;
    }
    public void StartFirstPrompt()
    {
        firstPrompt = true;

       StartCoroutine(FirstPrompt());
    }
    public void StopFirstPrompt()
    {
        firstPrompt = false;
        StopCoroutine(FirstPrompt());
    }

    public void StartSecondPrompt()
    {
        secondPrompt = true;
        StartCoroutine(SecondPrompt());
    }
    public void StopSecondPrompt()
    {
        secondPrompt = false;
        StopCoroutine(SecondPrompt());
    }
    private IEnumerator SecondPrompt()
    {
        yield return new WaitForSeconds(8);
        if (secondPrompt)
        {
            _hints.MoveHand(transform, _hints.Policeman.transform);
        }yield return null;
    }
    public void StartThreePrompt()
    {
        threePrompt = true;
        firstPrompt = false;
        carClick.Collider2D.enabled = true;
        StartCoroutine(ThreePrompt());
    }
    public void StopThreePrompt()
    {
        threePrompt = false;
    }
    private IEnumerator ThreePrompt()
    {
        yield return new WaitForSeconds(10);
        if (threePrompt)
        {
            _hints.MoveHand(CarClick.transform, CarClick.transform);

        }yield return null; 
    }

}
