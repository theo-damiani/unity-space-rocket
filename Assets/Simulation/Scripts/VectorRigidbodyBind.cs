using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class VectorRigidbodyBind : MonoBehaviour
{
    [SerializeField] private Vector3Variable velocityVector;
    [SerializeField] private DraggableVector vector;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!vector.IsDragged())
        {
            SetVectorVelocity();
        }
    }

    public void SetRigidbodyVelocity()
    {
        rb.velocity = velocityVector.Value;
    }

    public void SetVectorVelocity()
    {
        if (velocityVector.Value == rb.velocity)
        {
            return;
        }
        velocityVector.Value = rb.velocity;
        vector.Redraw();
    }
}
