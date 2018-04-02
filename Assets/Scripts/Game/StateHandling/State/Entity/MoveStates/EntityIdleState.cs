public class EntityIdleState : EntityState {
    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        FighterInputManager inputManager = 
            this.StateMachine.Automaton.FighterInputManager;

        EntityState lastState = this.StateMachine.StateHistory.Peek();

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

    public override void Update() {
        EntityAutomaton automaton = this.StateMachine.Automaton;

        FighterInputManager inputManager = automaton.FighterInputManager;
        FighterPhysics fighterPhysics = automaton.FighterPhysics;

        if (fighterPhysics.Direction != inputManager.MoveDirection) {
            fighterPhysics.SwapDirection();
        }
    }

    public override void Enter() {
        // necessary to keep track of history
        this.SaveToHistory();
    }

    public override void Exit() {
        // the animation does not have to be frozen anymore
        this.ResumeNormalAnimation();
    }
}
