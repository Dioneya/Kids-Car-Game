using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    [SerializeField] float movSpeed;
    [SerializeField] private Car _car;
    private void FixedUpdate()
    {
        _car.transform.position += Vector3.right * (movSpeed + (movSpeed * 0.2f)) * Time.deltaTime;
    }
    private void OnEnable()
    {
        GetComponent<WheelRotate>().enabled = true;
    }
}
