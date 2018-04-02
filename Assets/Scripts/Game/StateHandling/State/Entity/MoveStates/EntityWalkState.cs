using UnityEngine;

public class EntityWalkState : EntityState {
    private float _walkForce = 0f;
    private readonly float _walkAnimationSpeed = 4f;

    public override State HandleInput(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return null;

        EntityStateMachine entityStateMachine =
            (EntityStateMachine) stateMachine;

        if (!this.IsAnimationPlaying(entityStateMachine, "EntityWalk")) {
            return null;
        }

        EntityAutomaton automaton =
            (EntityAutomaton) entityStateMachine.Automaton;
        FighterInputManager inputManager = automaton.FighterInputManager;

        if (!inputManager.IsMove()) {
            return new EntityIdleState();
        }

        if (inputManager.IsDash()) {
            return new EntityDashState();
        }

        return null;
    }

    public override void Update(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        EntityStateMachine entityStateMachine =
            (EntityStateMachine) stateMachine;

        EntityAutomaton automaton = (EntityAutomaton)
            entityStateMachine.Automaton;

        this._walkForce = Mathf.Clamp(
            -1, automaton.FighterInputManager.GetMovePos(), 1
        ) * automaton.FighterPhysics.Direction;

        this.SetSpeed(entityStateMachine);

        automaton.FighterPhysics.Walk(this._walkForce);
    }

    public override void Enter(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        base.Enter(stateMachine);

        EntityAutomaton automaton = (EntityAutomaton) 
            ((EntityStateMachine) stateMachine).Automaton;


        int direction = 
            automaton.FighterInputManager.GetMovePos() > 0 ? 1 : -1;

        automaton.FighterPhysics.Direction = direction;

        // necessary to keep track of history
        this.SaveToHistory((EntityStateMachine) stateMachine);
    }

    protected override void SetSpeed(EntityStateMachine stateMachine) {
        stateMachine.Animator.speed = 
            this._walkForce * this._walkAnimationSpeed;
    }
}
