public class EntityFallState : EntityState
{
    private EntityInputManager _inputManager;

    public override State HandleInput()
    {
        if (!IsAnimationPlayingMe())
        {
            return null;
        }

        // TODO: Handle states.
        return new EntityIdleState();

        return null;
    }

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);
    }

    public override void Update()
    {
        stateMachine.automaton.physics.Run();
    }

    public override void Enter()
    {
        base.Enter();

        EntityPhysics entityPhysics = stateMachine.automaton.physics;
        entityPhysics.currentSpeed = entityPhysics.dashInitialSpeed;

        _inputManager = stateMachine.automaton.inputManager;

        // Necessary to keep track of history.
        SaveToHistory();
    }
}
