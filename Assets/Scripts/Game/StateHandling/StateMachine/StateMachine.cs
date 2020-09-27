using System;
using UnityEngine;

// An automaton can have several FSMs.
public class StateMachine : MonoBehaviour
{
    [HideInInspector]
    public State currentState
    {
        get; protected set;
    }

    [HideInInspector]
    // A next state is set when handling an input (if any).
    protected State m_nextState = null;

    /**
     * If no state is given when initializing a state machine, a default one
     * is used.
     *
     * It can be changed in a child class, if necessary.
     */
    public virtual string defaultState
    {
        get
        {
            return "State";
        }
    }

    // Inputs are handled once per frame.
    void Update()
    {
        HandleInput();
    }

    // Logic is handled one per "logical" frame.
    void FixedUpdate()
    {
        SwitchState();

        currentState.Update();
    }

    // When initializing a state machine, we get the bound automaton.
    protected virtual void Initialize(string startingState = null)
    {
    }

    // Simple function which will get a starting state type when initializing.
    protected virtual Type GetStartingStateType(string startingState)
    {
        if (string.IsNullOrEmpty(startingState))
        {
            startingState = defaultState;
        }

        Type stateType = Type.GetType(startingState);

        if (stateType != null)
        {
            return stateType;
        }

        Debug.LogError(startingState + ": unknown state to initialize!");

        return null;
    }

    // By default, the handling of an input relies on the current state.
    public virtual void HandleInput()
    {
        m_nextState = currentState.HandleInput();
    }

    // Function used to switch state from a previous to a new one.
    protected virtual void SwitchState()
    {
        // If the next state is null, it means  the current state did not
        // returned any new state when handling the inputs.
        if (m_nextState == null)
        {
            return;
        }

        m_nextState.Initialize(this);
        currentState.Exit();
        currentState = m_nextState;
        currentState.Enter();
        m_nextState = null;
    }

    public virtual void SetState(State state)
    {
        m_nextState = state;
        SwitchState();
    }
}
