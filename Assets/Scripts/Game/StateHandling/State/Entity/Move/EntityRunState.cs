public class EntityRunState : EntityState {
    private int _initDirection = 0;

    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        EntityInputManager inputManager = 
            this.StateMachine.Automaton.InputManager;

        if (!inputManager.IsMove()) {
            return new EntityIdleState();
        }

        if (this._initDirection != inputManager.MoveDir) {
            return new EntityRunBrakeState();
        }

        return null;
    }

    public override void Update() {
        this.StateMachine.Automaton.Physics.Run();
    }

    public override void Enter() {
        base.Enter();

        this._initDirection =
            this.StateMachine.Automaton.Physics.Direction;

        // necessary to keep track of history
        this.SaveToHistory();
    }
}
