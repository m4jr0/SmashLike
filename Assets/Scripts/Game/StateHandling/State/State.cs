public class State
{
    // Used to initialize a state when creating it.
    public virtual void Initialize(StateMachine stateMachine)
    {
    }

    // Function designed to handle input at each update.
    public virtual State HandleInput()
    {
        return null;
    }

    // Called at each update.
    public virtual void FixedUpdate()
    {
    }

    // Used when entering the state.
    public virtual void Enter()
    {
    }

    // Used when exiting the state.
    public virtual void Exit()
    {
    }
}
