using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    private Vector3 distanceToTarget;

    void Start()
    {
        // Cache the initial offset at time of load/spawn:
        distanceToTarget = transform.position - target.position;
    }

    void Update()
    {
        transform.position = target.position + distanceToTarget;
    }
}
