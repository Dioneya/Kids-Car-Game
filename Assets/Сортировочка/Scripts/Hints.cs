using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Hints : MonoBehaviour
{
    [SerializeField] GameObject handPrefab;
    [SerializeField] private CharacterMovement _policeman;

    DragAndDrop dragAndDrop;
    InputController inputController;
    CarParts carParts;
    DetailsGenerator generator;
    private bool _isCarBuild;
    private int numFails;
    private GameObject hand;
    public GameObject Hand => hand;
    [SerializeField]
    private Transform _startPosition;
    [SerializeField]
    private Transform _endPosition;


    public CharacterMovement Policeman { get => _policeman; set => _policeman = value; }
    public Transform StartPosition { get => _startPosition; set => _startPosition = value; }
    public Transform EndPosition { get => _endPosition; set => _endPosition = value; }
    [SerializeField]
    private int _countLose = 3;

    private float timer = 20;

    private void Start()
    {
        hand = Instantiate(handPrefab,Vector3.zero,Quaternion.identity);
        hand.SetActive(false);
        inputController = GetComponent<InputController>();
        generator = FindObjectOfType<DetailsGenerator>();
        carParts = FindObjectOfType<CarParts>();
        dragAndDrop = GetComponent<DragAndDrop>();
        dragAndDrop.OnFail += FailAttempt;
        dragAndDrop.OnSuccess += SuccessAttempt;
        dragAndDrop.OnDoSmth += WhenDoSmth;
    }
    private void Update()
    {
        if(timer <= 0)
        {
            numFails = 0;
            ShowHint();
            timer = 20;

        }
        timer -= Time.deltaTime;
    }

    private void FailAttempt()
    {
        numFails++;
        if (numFails >= _countLose)
        {
            numFails = 0;
            ShowHint();
        }
    }
    private void SuccessAttempt()
    {
        numFails = 0;
    }
    private void WhenDoSmth()
    {
        timer = 30;
        _countLose = 5;
    }

    public void GoPrison()
    {
        numFails = 0;
        timer = 30;
        _countLose = 5;
    }

    private void ShowHint()
    {
        if (!generator.IsAllDetailsSetUp())
        {
            dragAndDrop.enabled = false;
            //inputController.enabled = false;
            Debug.Log("Fals enable");
            var r = Random.Range(0, generator.takedPlaces.Length);
            var detail = generator.GetRandomDetail();
            GameObject posToPlace;
            if (detail == null)
            {
                //inputController.enabled = true;
                return;
            }
            else
            {
                posToPlace = carParts.FindCarPartByDetail(detail);
            }
            MoveHand(detail.transform, posToPlace.transform);
        }
        else ShowNewHintPosition();
    }

    private void ShowNewHintPosition()
    {
        if (_startPosition != null && _endPosition != null)
        {
            MoveHand(_startPosition, _endPosition);
            dragAndDrop.enabled = false;
        }
        else print(_startPosition + "start /////" + _endPosition);
    }

    IEnumerator Working(Transform detail,Transform posToPlace)
    {
        hand.SetActive(true);
        for (float i = 0; i<=1.1f;i+= 0.02f)
        {
            hand.transform.position = Vector3.Lerp(Vector3.zero,detail.position,i);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1f);

        for (float i = 0; i <= 1.1f; i += 0.02f)
        {
            hand.transform.position = Vector3.Lerp(detail.position, posToPlace.position, i);
            yield return new WaitForSeconds(0.02f);
        }

        yield return new WaitForSeconds(1f);
        hand.SetActive(false);
        //inputController.enabled = true;
        dragAndDrop.enabled = true;
    }

    public void MoveHand(Transform startPosition , Transform endPosition)
    {
        StartCoroutine(Working(startPosition , endPosition));
    }

    public void TransformHandSize(Vector2 size)
    {
        StartCoroutine(Size(size));
    }

    private IEnumerator Size(Vector2 size)
    {
        yield return new WaitForSeconds(1f);
        for (float i = 0; i <= 1.1f; i += 0.02f)
        {
            hand.transform.localScale = Vector3.Lerp(hand.transform.localScale, size, i);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(1f);
        for (float i = 0; i <= 1.1f; i += 0.02f)
        {
            hand.transform.localScale = Vector3.Lerp(hand.transform.localScale, Vector2.one, i);
            yield return new WaitForSeconds(0.02f);
        }
    }
}
