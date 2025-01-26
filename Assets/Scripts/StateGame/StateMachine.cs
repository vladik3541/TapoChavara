
public class StateMachine 
{
    public State CurrentState { get; set; }

    public void InitializeState(State state)
    {
        CurrentState = state;
        CurrentState.Enter();
    }
    public void SwitchState(State newState)
    {
        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}
