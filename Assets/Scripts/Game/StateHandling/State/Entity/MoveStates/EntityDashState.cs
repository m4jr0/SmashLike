public class EntityDashState : EntityFramedState {
    public bool HasStopped {
        get { return this._hasStopped; }
    }

    private bool _hasStopped = false;

    private int _initDirection = 0;
    private float _maxSpeedFrame = 0f;
    private float _acceleration = 0f;

    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        FighterInputManager inputManager =
            this.StateMachine.Automaton.FighterInputManager;

        if (this._initDirection != inputManager.MoveDirection) {
            return new EntityIdleState();
        }

        if (!inputManager.IsMove()) this._hasStopped = true;

        if (this.HasStopped) {
            return new EntityIdleState(); // to be replaced with another state
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
        this._maxSpeedFrame = this.MaxFrame - 5;
    }

    public override void Update() {

        this.CurrentFrame++;

        this.StateMachine.Automaton.FighterPhysics.Accelerate(
            this._acceleration);
    }

    public override void Enter() {
        FighterPhysics fighterPhysics = 
            this.StateMachine.Automaton.FighterPhysics;

        fighterPhysics.CurrentSpeed = fighterPhysics.DashInitialSpeed;

        this._initDirection = fighterPhysics.Direction;

        this._acceleration = (fighterPhysics.RunSpeed - this._initDirection) / 
                             this._maxSpeedFrame;

        // necessary to keep track of history
        this.SaveToHistory();
    }

    public override void Exit() {
        this.StateMachine.Automaton.FighterPhysics.Accelerate(
            this._acceleration);
    }
}
