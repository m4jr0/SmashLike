public class EntityRunState : EntityState {
    private int _initDirection = 0;
    // we add a one frame delay to prevent reading the neutral position on the
    // stick when the player is moving it to the opposite side
    private bool _isStickNeutral = false;

    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        EntityInputManager inputManager = 
            this.StateMachine.Automaton.InputManager;

        if (inputManager.IsJump()) {
            return new EntityJumpSquatState();
        }

        if (this._initDirection != inputManager.MoveDir) {
            return new EntityTurnAroundState();
        }

        if (!inputManager.IsMove()) {
            if (this._isStickNeutral) return new EntityRunBrakeState();

            // we read a neutral position. But the player may be tilting it 
            // towards the opposite side so, just to be sure, we add a one
            // frame delay, which is the purpose of this boolean
            this._isStickNeutral = true;

            return null;
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
