using System;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    public event Action OnSuccess;
    public event Action OnFail;
   // public event Action<IDropStateRobber> NewOnFail;
    //public event Action<IDropStateRobber> NewOnSuccess;
    public event Action OnDoSmth;

    private float distanceToCorrectDrop =1;
    GameObject currentMoving;
    InputController input;
    CircleCollider2D collider;
    [SerializeField] private GameObject _carPart;
    private ICarParts _carParts;
    AudioSource audio;
    DetailsGenerator generator;
    private void OnValidate()
    {
        if (_carPart.GetComponent<ICarParts>() != null)
        {
            _carParts = _carPart.GetComponent<ICarParts>();
        }
        else _carPart = null;

    }
    private void Start()
    {
        audio = GetComponent<AudioSource>();
        generator = FindObjectOfType<DetailsGenerator>();
        collider = GetComponent<CircleCollider2D>();
        input = GetComponent<InputController>();
    }

    public void SetMovingObject(GameObject obj, bool isOnlyMoving, MovableObject movable)
    {
        OnDoSmth?.Invoke();
        currentMoving = obj;
        if (!isOnlyMoving)
        {
            AudioPlayPickUpDetail(currentMoving);
            movable.StartMoving();
        }
    }

    public void FinishMoving(bool isOnlyMoving )
    {
        if (isOnlyMoving)
        {
            currentMoving = null;
            transform.position = new Vector3(10, 10, 0);
            return;
        }

        var settings = currentMoving.GetComponent<PartSettings>();
        if ((settings.destinationObjectTag == null || settings.destinationObjectTag.Equals("")) && _carParts.TryToSetUpDetail(currentMoving, distanceToCorrectDrop))
        {
            generator.GenerateAllFreeSlots();
            AudioPlayPutDetail(currentMoving);
            Vibration.Vibrate(100);
            OnSuccess?.Invoke();
            print("Success");
            if (generator.IsAllDetailsSetUp())
                FindObjectOfType<Level>().StartLevel();

        }
        else if ((settings.destinationObjectTag != null && !settings.destinationObjectTag.Equals("")) &&
            TryToSetUp(GameObject.FindGameObjectsWithTag(settings.destinationObjectTag), settings.destionationObjectRangeInstall,
            settings.isColorMatching))
        {
            currentMoving.GetComponent<IInteractiveAction>().Action();
            OnSuccess?.Invoke();
            print("Success");
        }
        else
        {
            if (_carParts.IsInCarBase(currentMoving.transform.position))
                AudioPlayError(currentMoving);
            currentMoving.GetComponent<MovableObject>().ReturnToStartPosition();
            OnFail?.Invoke();
        }
       // transform.position = new Vector3(10, 10, 0);
        currentMoving = null;

    }

    private bool TryToSetUp(GameObject[] obj, float dist, bool isColorMatching)
    {
        foreach (GameObject o in obj)
        {
            var trueDist = Vector3.Distance(currentMoving.transform.position, o.transform.position);
            if (trueDist <= dist)
            {
                if (!isColorMatching)
                {
                    currentMoving.GetComponent<PartSettings>().targetObject = o;
                  
                    return true;
                }
                else if (currentMoving.name.Split('(')[0] == o.GetComponent<SpriteRenderer>().sprite.name)
                {
                    currentMoving.GetComponent<PartSettings>().targetObject = o;
                    return true;
                }
            }
        }
        return false;
    }

    private void Update()
    {
        if (input.isPressed())
        {
            transform.position = Camera.main.ScreenToWorldPoint(input.mousePos);
            collider.enabled = true;
            if (currentMoving != null)
            {
                currentMoving.transform.position = new Vector3(transform.position.x, transform.position.y, 0);
            }
            else
            {
                return;
            }
        }
        else
        {
            collider.enabled = false;
            transform.position = Vector3.zero;
            currentMoving = null;
        }
    }

    private void AudioPlayError(GameObject obj)
    {
        var clip = obj.GetComponent<PartSettings>().PlayError();
        if (clip == null)
            return;
        audio.clip = clip;
        audio.Play();
    }

    private void AudioPlayPutDetail(GameObject obj)
    {
        var clip = obj.GetComponent<PartSettings>().PlayPut();
        if (clip == null)
            return;
        audio.clip = clip;
        audio.Play();
    }

    private void AudioPlayPickUpDetail(GameObject obj)
    {
        var clip = obj.GetComponent<PartSettings>().PlayPickUp();
        if (clip == null)
            return;
        audio.clip = clip;
        audio.Play();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (currentMoving != null)
            return;

        var movable = collision.gameObject.GetComponent<MovableObject>();
       var robber = collision.GetComponent<Robber>();
        if (robber != null && movable.IsCanDrag)
        {
            robber.Catching();
        }
        if (movable != null && movable.IsCanDrag)
        {
            bool isOM = false;
            if (movable.isOnlyMoving)
                isOM = true;
            SetMovingObject(collision.gameObject, isOM, movable);
        }
        else
        {
            return;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentMoving == null)
            return;

        var movable = collision.GetComponent<MovableObject>();
        var robber = collision.GetComponent<Robber>();
        if (movable != null)
        {
            if (movable.gameObject == currentMoving)
            {
                bool isOM = false;
                if (movable.isOnlyMoving)
                    isOM = true;
                FinishMoving(isOM);
            }
        }
        else return;

    }
}
