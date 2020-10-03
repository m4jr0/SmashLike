public class EntityFallState : EntityState
{
    private EntityInputManager m_inputManager;

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

    public override void FixedUpdate()
    {
        stateMachine.automaton.physics.Run();
    }

    public override void Enter()
    {
        base.Enter();

        EntityPhysics entityPhysics = stateMachine.automaton.physics;
        entityPhysics.currentSpeed = entityPhysics.dashInitialSpeed;

        m_inputManager = stateMachine.automaton.inputManager;

        // Necessary to keep track of history.
        SaveToHistory();
    }
}
