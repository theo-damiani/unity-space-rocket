using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorLabel : MonoBehaviour
{
    [SerializeField] private Vector vector;
    [SerializeField] private float offset;
    [SerializeField] private Camera mainCamera;

    public void SetSpriteOrientation()
    {
        transform.LookAt(mainCamera.transform);
    }

    void LateUpdate()
    {
        transform.localPosition = vector.components.Value + vector.components.Value.normalized * offset;
    }
}
