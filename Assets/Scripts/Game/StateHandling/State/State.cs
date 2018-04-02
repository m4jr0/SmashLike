public class State {
    // used to initialize a state when creating it
    public virtual void Initialize(StateMachine stateMachine) {
    }

    // function designed to handle input at each update
    public virtual State HandleInput() {
        return null;
    }

    // called at each update
    public virtual void Update() {
    }

    // used when entering the state
    public virtual void Enter() {
    }

    // used when exiting the state
    public virtual void Exit() {
    }
}
