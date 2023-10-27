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
        distanceToTarget = gameObject.transform.localPosition - target.localPosition;
    }

    void LateUpdate()
    {
        gameObject.transform.localPosition = target.localPosition + distanceToTarget;
    }
}
