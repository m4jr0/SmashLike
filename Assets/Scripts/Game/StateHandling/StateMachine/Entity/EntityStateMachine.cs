using System;
using UnityEngine;
using UnityEngine.Networking;

public class EntityStateMachine : StateMachine
{
    public Animator animator = null;
    public PlayerController playerController = null;
    public EntityAutomaton automaton;
    [HideInInspector] public FixedSizedQueue<EntityState> stateHistory;
    public int maxHistorySize = 12;

    // To be changed in a child class, if necessary.
    public override string defaultState
    {
        get
        {
            return "EntityIdleState";
        }
    }

    void Update()
    {
        // In each update, we check the player inputs.
        HandleInput();
    }

    void Start()
    {
        Initialize(null);
    }

    protected override void Initialize(string startingState = null)
    {
        base.Initialize(null);

        animator = GetComponent<Animator>();
        stateHistory = new FixedSizedQueue<EntityState>(maxHistorySize);
        Type stateType = GetStartingStateType(startingState);

        if (stateType == null)
        {
            return;
        }

        currentState = (EntityState)Activator.CreateInstance(stateType);
        currentState.Initialize(this);
        currentState.Enter();
    }

    protected override void SwitchState()
    {
        if (m_nextState == null)
        {
            return;
        }

        base.SwitchState();
    }
}
