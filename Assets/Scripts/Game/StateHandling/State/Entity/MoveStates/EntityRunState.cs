public class EntityRunState : EntityState {
    private int _initDirection = 0;

    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        FighterInputManager inputManager = 
            this.StateMachine.Automaton.FighterInputManager;

        if (!inputManager.IsMove()) {
            return new EntityIdleState();
        }

        if (this._initDirection != inputManager.MoveDirection) {
            return new EntityIdleState(); // to be replaced with another state
        }

        return null;
    }

    public override void Update() {
        this.StateMachine.Automaton.FighterPhysics.Run();
    }

    public override void Enter() {
        this._initDirection =
            this.StateMachine.Automaton.FighterPhysics.Direction;

        // necessary to keep track of history
        this.SaveToHistory();
    }
}
