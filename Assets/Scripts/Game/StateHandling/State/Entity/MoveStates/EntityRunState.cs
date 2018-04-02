public class EntityRunState : EntityState {
    private int _initDirection = 0;

    public override State HandleInput(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return null;

        EntityStateMachine entityStateMachine =
            (EntityStateMachine) stateMachine;

        if (!this.IsAnimationPlaying(entityStateMachine, "EntityRun")) {
            return null;
        }

        EntityAutomaton automaton =
            (EntityAutomaton) entityStateMachine.Automaton;
        FighterInputManager inputManager = automaton.FighterInputManager;

        if (!inputManager.IsMove()) {
            return new EntityIdleState();
        }

        if (this._initDirection != inputManager.MoveDirection) {
            return new EntityIdleState(); // to be replaced with another state
        }

        return null;
    }

    public override void Update(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        EntityAutomaton automaton = (EntityAutomaton)
            ((EntityStateMachine)stateMachine).Automaton;

        automaton.FighterPhysics.Run();
    }

    public override void Enter(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        EntityAutomaton automaton = (EntityAutomaton) 
            ((EntityStateMachine) stateMachine).Automaton;



        this._initDirection = automaton.FighterPhysics.Direction;

        // necessary to keep track of history
        this.SaveToHistory((EntityStateMachine) stateMachine);
    }
}
