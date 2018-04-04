using UnityEngine;

public class EntityWalkState : EntityState {
    private float _walkForce = 0f;
    private readonly float _walkAnimationSpeed = 4f;

    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        EntityInputManager inputManager =
            this.StateMachine.Automaton.InputManager;

        if (inputManager.IsJump()) {
            return new EntityJumpSquatState();
        }

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
            -1, automaton.InputManager.GetMovePos(), 1
        ) * automaton.Physics.Direction;

        this.SetSpeed();

        automaton.Physics.Walk(this._walkForce);
    }

    public override void Enter() {
        base.Enter();

        EntityAutomaton automaton = this.StateMachine.Automaton;

        int direction = 
            automaton.InputManager.GetMovePos() > 0 ? 1 : -1;

        automaton.Physics.Direction = direction;

        // necessary to keep track of history
        this.SaveToHistory();
    }

    protected override void SetSpeed() {
        this.StateMachine.Animator.speed = 
            Mathf.Abs(this._walkForce) * this._walkAnimationSpeed;
    }
}
