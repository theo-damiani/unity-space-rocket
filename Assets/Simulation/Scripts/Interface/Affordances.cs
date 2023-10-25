using System;
using UnityEngine;


[CreateAssetMenu(fileName = "Affordances", menuName="Affordances")]
public class Affordances : ScriptableObject
{
    public bool showPlayButton;
    public bool showPauseButton;
    public bool showResetButton;
    public bool showTimeControl;
    public bool showCameraControl;
    public A_PhysicalObject physicalObject;
}

[Serializable]
public class A_PhysicalObject
{
    public bool showTrace;
    public bool showTraceIsInteractive;
    public A_Vector3 initialPosition;
    public bool velocityVectorIsVisible;
    public bool velocityVectorIsInteractive;
    public A_Vector3 InitialVelocity;
}

[Serializable]
public class A_Vector3
{
    public float X;
    public float Y;
    public float Z;

    public Vector3 ToVector3()
    {
        return new Vector3(X, Y, Z);
    }
}