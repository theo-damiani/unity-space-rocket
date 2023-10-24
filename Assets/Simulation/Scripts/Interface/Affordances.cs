using System;

[Serializable]
public class Affordances
{
    public bool ShowPlayButton;
    public bool ShowPauseButton;
    public bool ShowResetButton;
    public bool ShowTimeControl;
    public bool ShowCameraControl;
    public A_PhysicsObject PhysicsObject;
}

[Serializable]
public class A_PhysicsObject
{
    public bool ShowTrace;
    public A_Motion UniformMotion;
}

[Serializable]
public class A_Motion
{
    public bool IsInteractive;
    public bool IsActive;
    public A_Vector3 Velocity;
}

[Serializable]
public class A_Vector3
{
    public float X;
    public float Y;
    public float Z;
}
