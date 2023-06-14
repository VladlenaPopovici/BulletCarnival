namespace AimStates
{
    public abstract class AimingBaseState
    {
        public abstract void EnterState(CameraController aim);
        public abstract void UpdateState(CameraController aim);
    }
}
