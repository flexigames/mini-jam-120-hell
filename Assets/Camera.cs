using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    private Transform target;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;

        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        transform.position = new Vector3(target.position.x, target.position.y, 0) + initialPosition;
    }
}
