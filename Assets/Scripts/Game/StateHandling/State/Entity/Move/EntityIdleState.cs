public class EntityIdleState : EntityState {
    public override State HandleInput() {
        if (!this.IsAnimationPlayingMe()) {
            return null;
        }

        EntityInputManager inputManager = 
            this.StateMachine.Automaton.InputManager;

        // differencier isDash de isDashBack
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
        EntityPhysics entityPhysics = automaton.Physics;

        if (entityPhysics.Direction != automaton.InputManager.MoveDir) {
            entityPhysics.SwapDirection();
        }
    }

    public override void Enter() {
        base.Enter();

        // necessary to keep track of history
        this.SaveToHistory();
    }
}
