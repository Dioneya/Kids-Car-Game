using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LightCatchRobbers : MonoBehaviour
{
    [SerializeField] private MovableObject movableObject;
    [SerializeField] private Robber _robber;
    [SerializeField] private Collider2D collider2D;
    public Collider2D[] colliders;
    [SerializeField] private GameObject _parent;

    public Collider2D Collider2D { get => collider2D; set => collider2D = value; }

    private void Init()
    {
        _parent.transform.localScale = new LightCatchStrategy().LightSize();
    }

    private void OnEnable()
    {
        Init();
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Robber>() != null)
        {
            _robber = collision.GetComponent<Robber>();
            _robber.Release();

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Robber>() != null)
        {
            _robber = collision.GetComponent<Robber>();
            _robber.Find();

        }
    }

}
