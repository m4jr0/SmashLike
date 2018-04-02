using System;
using UnityEngine;
using UnityEngine.Networking;

public class EntityStateMachine : StateMachine {
    public Animator Animator = null;
    public PlayerController PlayerController = null;
    [HideInInspector] public FixedSizedQueue<EntityState> StateHistory;
    public int MaxHistorySize = 12;

    // to be changed in a child class, if necessary
    public override string DefaultState {
        get { return "EntityIdleState"; }
    }

    void Update() {
        // in each update, we check the player inputs
        this.HandleInput();
    }

    void Start() {
        this.Initialize(null);
    }

    protected override void Initialize(string startingState = null) {
        base.Initialize(null);

        this.Animator = this.GetComponent<Animator>();

        Type stateType = this.GetStartingStateType(startingState);

        if (stateType == null) return;

        this.CurrentState = (EntityState) Activator.CreateInstance(stateType);

        this.StateHistory = new FixedSizedQueue<EntityState>(
            this.MaxHistorySize
        );
    }

    protected override void SwitchState() {
        if (!(this.NextState is EntityState)) {
            return;
        }

        base.SwitchState();
        this.Animator.SetTrigger(this.CurrentState.GetType().Name);
    }
}
