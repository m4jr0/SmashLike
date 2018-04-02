using UnityEngine;

public class EntityWalkState : EntityState {
    private float _walkForce = 0f;
    private readonly float _walkAnimationSpeed = 4f;

    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        FighterInputManager inputManager =
            this.StateMachine.Automaton.FighterInputManager;

        if (!inputManager.IsMove()) {
            return new EntityIdleState();
        }

        if (inputManager.IsDash()) {
            return new EntityDashState();
        }

        return null;
    }

    public override void Update() {
        EntityAutomaton automaton = this.StateMachine.Automaton;

        this._walkForce = Mathf.Clamp(
            -1, automaton.FighterInputManager.GetMovePos(), 1
        ) * automaton.FighterPhysics.Direction;

        this.SetSpeed();

        automaton.FighterPhysics.Walk(this._walkForce);
    }

    public override void Enter() {
        base.Enter();

        EntityAutomaton automaton = this.StateMachine.Automaton;

        int direction = 
            automaton.FighterInputManager.GetMovePos() > 0 ? 1 : -1;

        automaton.FighterPhysics.Direction = direction;

        // necessary to keep track of history
        this.SaveToHistory();
    }

    protected override void SetSpeed() {
        this.StateMachine.Animator.speed = 
            Mathf.Abs(this._walkForce) * this._walkAnimationSpeed;
    }
}
