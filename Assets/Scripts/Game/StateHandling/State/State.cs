public class State {
    // used to initialize a state when creating it
    public virtual void Initialize() {
    }

    // function designed to handle input at each update
    public virtual State HandleInput(StateMachine stateMachine) {
        return null;
    }

    // called at each update
    public virtual void Update(StateMachine stateMachine) {
    }

    // used when entering the state
    public virtual void Enter(StateMachine stateMachine) {
    }

    // used when exiting the state
    public virtual void Exit(StateMachine stateMachine) {
    }
}
