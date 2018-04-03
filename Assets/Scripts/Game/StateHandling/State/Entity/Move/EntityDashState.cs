public class EntityDashState : EntityFramedState {
    public bool HasStopped {
        get { return this._hasStopped; }
    }

    private bool _hasStopped = false;

    private int _initDirection = 0;

    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        EntityInputManager inputManager =
            this.StateMachine.Automaton.InputManager;

        if (inputManager.IsDash() &&
            this._initDirection != inputManager.MoveDir) {
            return new EntityIdleState();
        }

        if (!inputManager.IsMove()) this._hasStopped = true;

        if (this.HasStopped) {
            return new EntityRunBrakeState();
        }

        if (this.IsStateFinished()) {
            return new EntityRunState();
        }

        return null;
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

        this.StateMachine.Automaton.Physics.Run();
    }

    public override void Enter() {
        base.Enter();

        EntityPhysics entityPhysics = 
            this.StateMachine.Automaton.Physics;

        entityPhysics.CurrentSpeed = entityPhysics.DashInitialSpeed;

        this._initDirection = entityPhysics.Direction;

        // necessary to keep track of history
        this.SaveToHistory();
    }
}
