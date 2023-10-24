public static class DefaultAffordances
{
    public static Affordances GetDefaultAffordances()
    {
        A_Vector3 velocityInit = new()
        {
            X = 5,
            Y = 0,
            Z = 0
        };

        A_Motion uniformMotion = new()
        {
            IsActive = true,
            IsInteractive = true,
            Velocity = velocityInit
        };

        A_PhysicsObject rocket = new()
        {
            ShowTrace = false,
            UniformMotion = uniformMotion
        };
        
        Affordances defaultConfig = new()
        {
            ShowPlayButton = false,
            ShowPauseButton = false,
            ShowResetButton = false,
            ShowCameraControl = false,
            ShowTimeControl = false,
            PhysicsObject = rocket
        };

        return defaultConfig;
    }
}
