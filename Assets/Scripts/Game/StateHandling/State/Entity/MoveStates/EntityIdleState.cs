public class EntityIdleState : EntityState {
    public override State HandleInput(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return null;

        EntityStateMachine entityStateMachine =
            (EntityStateMachine)stateMachine;

        if (!this.IsAnimationPlaying(entityStateMachine, "EntityIdle")) {
            return null;
        }

        EntityAutomaton automaton =
            (EntityAutomaton)entityStateMachine.Automaton;
        FighterInputManager inputManager = automaton.FighterInputManager;

        EntityState lastState = entityStateMachine.StateHistory.Peek();

        if (lastState is EntityDashState) {
            EntityDashState entityDashState = (EntityDashState) lastState;

            if (!entityDashState.HasStopped) {
                return new EntityDashState();
            }
        }

        if (inputManager.IsDash()) {
            return new EntityDashState();
        }

        if (inputManager.IsWalk()) {
            return new EntityWalkState();
        }

        return null;
    }

    public override void Update(StateMachine stateMachine) {
        EntityStateMachine entityStateMachine =
            (EntityStateMachine)stateMachine;

        EntityAutomaton automaton =
            (EntityAutomaton)entityStateMachine.Automaton;

        FighterInputManager inputManager = automaton.FighterInputManager;
        FighterPhysics fighterPhysics = automaton.FighterPhysics;

        if (fighterPhysics.Direction != inputManager.MoveDirection) {
            fighterPhysics.SwapDirection();
        }
    }

    public override void Enter(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        // necessary to keep track of history
        this.SaveToHistory((EntityStateMachine) stateMachine);
    }

    public override void Exit(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        // the animation does not have to be frozen anymore
        this.ResumeNormalAnimation((EntityStateMachine) stateMachine);
    }
}
