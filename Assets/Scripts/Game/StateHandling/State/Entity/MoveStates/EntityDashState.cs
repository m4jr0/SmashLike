public class EntityDashState : EntityFramedState {
    public bool HasStopped {
        get { return this._hasStopped; }
    }

    private bool _hasStopped = false;

    private int _initDirection = 0;
    private float _maxSpeedFrame = 0f;
    private float _acceleration = 0f;

    public override State HandleInput(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return null;

        EntityStateMachine entityStateMachine =
            (EntityStateMachine) stateMachine;

        if (!this.IsAnimationPlaying(entityStateMachine, "EntityDash")) {
            return null;
        }

        EntityAutomaton automaton =
            (EntityAutomaton) entityStateMachine.Automaton;
        FighterInputManager inputManager = automaton.FighterInputManager;

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

    public override void Initialize() {
        base.Initialize();

        this.MaxFrame = 15;
        this.IASA = this.MaxFrame;
        this.MinActiveState = 1;
        this.MaxActiveState = this.MaxFrame;
        this._maxSpeedFrame = this.MaxFrame - 5;
    }

    public override void Update(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        this.CurrentFrame++;

        EntityAutomaton automaton = (EntityAutomaton)
            ((EntityStateMachine)stateMachine).Automaton;

        automaton.FighterPhysics.Accelerate(this._acceleration);
    }

    public override void Enter(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        EntityAutomaton automaton = (EntityAutomaton) 
            ((EntityStateMachine) stateMachine).Automaton;

        FighterPhysics fighterPhysics = automaton.FighterPhysics;

        fighterPhysics.CurrentSpeed = fighterPhysics.DashInitialSpeed;

        this._initDirection = fighterPhysics.Direction;

        this._acceleration = (fighterPhysics.RunSpeed - this._initDirection) / 
                             this._maxSpeedFrame;

        // necessary to keep track of history
        this.SaveToHistory((EntityStateMachine) stateMachine);
    }

    public override void Exit(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        EntityAutomaton automaton = (EntityAutomaton)
            ((EntityStateMachine)stateMachine).Automaton;

        automaton.FighterPhysics.Accelerate(this._acceleration);
    }
}
