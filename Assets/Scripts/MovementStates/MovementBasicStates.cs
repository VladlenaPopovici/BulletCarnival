namespace MovementStates
{
    public abstract class MovementBasicStates
    {
        public abstract void EnterState(PlayerController movement);
        public abstract void UpdateState(PlayerController movement);
    }
}
