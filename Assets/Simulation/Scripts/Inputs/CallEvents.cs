using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CallEvents : MonoBehaviour
{
    public KeyCode key;
    public UnityEvent responseOnKeyDown;
    public UnityEvent responseOnKeyUp;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(key))
        {
            responseOnKeyDown.Invoke();
        }
        else if (Input.GetKeyUp(key))
        {
            responseOnKeyUp.Invoke();
        }
    }
}
