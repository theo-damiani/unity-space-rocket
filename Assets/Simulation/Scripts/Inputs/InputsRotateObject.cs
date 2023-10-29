using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsRotateObject : MonoBehaviour
{
    public BoolReference isActive;
    [SerializeField] private List<Transform> transforms;
    [SerializeField] private Vector3 rotationLocalAxis;
    [SerializeField] private float rotationScale;
    [SerializeField] private Vector3Variable vector;
    private Vector3 localRotationRight;
    private Vector3 localRotationUp;

    void Start()
    {
        enabled = isActive.Value;
        localRotationRight = rotationLocalAxis*rotationScale;
        localRotationUp = Vector3.right*rotationScale;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateAll(localRotationRight);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateAll(-localRotationRight);
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            RotateAll(localRotationUp);
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            RotateAll(-localRotationUp);
        }
    }

    private void RotateAll(Vector3 rot)
    {
        foreach (Transform t in transforms)
        {
            t.Rotate(rot);
            Vector3 worldRot = t.TransformVector(rot);
            vector.Value = Quaternion.Euler(worldRot) * vector.Value;
        }
    }
}
