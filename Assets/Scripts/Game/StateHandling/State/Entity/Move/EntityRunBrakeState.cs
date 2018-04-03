public class EntityRunBrakeState : EntityFramedState {
    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        EntityInputManager inputManager =
            this.StateMachine.Automaton.InputManager;

        // handle jump here

        return this.IsStateFinished() ? new EntityIdleState() : null;
    }

    public override void Initialize(StateMachine stateMachine) {
        base.Initialize(stateMachine);

        this.MaxFrame = 15;
        this.IASA = this.MaxFrame;
        this.MinActiveState = 1;
        this.MaxActiveState = this.MaxFrame;
    }

    public override void Update() {
        this.CurrentFrame++;
    }
}
