using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform target;
    public float minZ;
    public float maxZ;
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

    public void ZoomAlongZ(float value)
    {
        distanceToTarget = new Vector3(distanceToTarget.x, distanceToTarget.y, SliderToCameraZ(value));
    }

    public void ZoomInAlongZ()
    {
        if (distanceToTarget.z+1 <= minZ)
        {
            distanceToTarget += Vector3.forward;
        }
    }

    public void ZoomOutAlongZ()
    {
        if (distanceToTarget.z-1 >= maxZ)
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
    
    public float GetSliderMinZ()
    {
        if (minZ<0 && maxZ<0)
            return CameraToSliderZ(maxZ);
        else
            return CameraToSliderZ(minZ);
    }

    public float GetSliderMaxZ()
    {
        if (minZ<0 && maxZ<0)
            return CameraToSliderZ(minZ);
        else
            return CameraToSliderZ(maxZ);
    }

    public float CameraToSliderZ(float value)
    {
        return Mathf.Log(-value);
    }

    public float SliderToCameraZ(float value)
    {
        return -Mathf.Exp(value);
    }
}
