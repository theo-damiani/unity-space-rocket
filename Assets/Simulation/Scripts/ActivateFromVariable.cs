using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFromVariable : MonoBehaviour
{
    [SerializeField] BoolReference activation;
    [SerializeField] GameObject go;
    void Start()
    {
        go.SetActive(activation.Value);
    }
}
