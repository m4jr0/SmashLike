using System;
using UnityEngine;

public class StateMachine : MonoBehaviour {
    [HideInInspector]
    public State CurrentState { get; protected set; }
    [HideInInspector]
    // an automaton can have several FSMs
    public Automaton Automaton = null;
    // a next state is set when handling an input (if any)
    protected State NextState = null;

    /**
     * If no state is given when initializing a state machine, a default one
     * is used.
     *
     * It can be changed in a child class, if necessary.
     */
    public virtual string DefaultState {
        get {
            return "State";
        }
    }

    // inputs are handled once per frame
    void Update() {
        this.HandleInput();
    }

    // logic is handled one per "logical" frame
    void FixedUpdate() {
        this.SwitchState();

        this.CurrentState.Update(this);
    }

    // when initializing a state machine, we get the bound automaton
    protected virtual void Initialize(string startingState = null) {
        this.Automaton = this.gameObject.GetComponent<Automaton>();
    }

    // simple function which will get a starting state type when initializing
    protected virtual Type GetStartingStateType(string startingState) {
        if (String.IsNullOrEmpty(startingState)) {
            startingState = this.DefaultState;
        }

        Type stateType = Type.GetType(startingState);

        if (stateType != null) return stateType;

        Debug.LogError(startingState + ": unknown state to initialize!");

        return null;
    }

    // by default, the handling of an input relies on the current state
    public virtual void HandleInput() {
        this.NextState = this.CurrentState.HandleInput(this);
    }

    // function used to switch state from a previous to a new one
    protected virtual void SwitchState() {
        // if the next state is null, it means  the current state did not
        // returned any new state when handling the inputs
        if (this.NextState == null) return;

        this.NextState.Initialize();
        this.CurrentState.Exit(this);
        this.CurrentState = this.NextState;
        this.CurrentState.Enter(this);
        this.NextState = null;
    }

    public virtual void SetState(State state) {
        this.NextState = state;
        this.SwitchState();
    }
}
