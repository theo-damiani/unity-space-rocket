using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputsRotateObject : MonoBehaviour
{
    [SerializeField] private List<Transform> transforms;
    [SerializeField] private Vector3 rotationAxis;
    [SerializeField] private float rotationScale;
    [SerializeField] private Vector3Variable vector;
    private Vector3 rotationRight;
    private Vector3 rotationLeft;

    void Start()
    {
        rotationRight = rotationAxis*rotationScale;
        rotationLeft = -rotationAxis*rotationScale;
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            RotateAll(rotationRight);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            RotateAll(rotationLeft);
        }
    }

    private void RotateAll(Vector3 rot)
    {
        foreach (Transform t in transforms)
        {
            t.Rotate(rot);
            Vector3 currentVector = vector.Value;
            vector.Value = Quaternion.Euler(rot) * currentVector;
        }
    }
}
