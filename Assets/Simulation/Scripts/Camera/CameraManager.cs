using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    [HideInInspector] public bool isLockedOnTarget = true;
    private Vector3 initOffsetToTarget;
    private Vector3 distanceToTarget;

    void Start()
    {
        // Cache the initial offset at time of load/spawn:
        SetOffsetToTarget();
    }

    public void SetOffsetToTarget()
    {
        initOffsetToTarget = gameObject.transform.localPosition - target.localPosition;
        distanceToTarget = initOffsetToTarget;
    }

    void LateUpdate()
    {
        if (isLockedOnTarget)
        {
            gameObject.transform.localPosition = target.localPosition + distanceToTarget;
        }
    }

    public void ZoomInAlongZ()
    {
        if (distanceToTarget.z+1 <= -1)
        {
            distanceToTarget += Vector3.forward;
        }
    }

    public void ZoomOutAlongZ()
    {
        if (distanceToTarget.z-1 >= -100)
        {
            distanceToTarget -= Vector3.forward;
        }
    }

    public void ToggleCameraLocked()
    {
        isLockedOnTarget = !isLockedOnTarget;
        if (isLockedOnTarget)
        {
            distanceToTarget = initOffsetToTarget;
        }
    }
}
