public class EntityDashState : EntityFramedState
{
    private int m_initDirection = 0;
    private EntityInputManager m_inputManager;

    public override State HandleInput()
    {
        if (!IsAnimationPlayingMe())
        {
            return null;
        }

        if (m_inputManager.IsJump())
        {
            return new EntityJumpSquatState();
        }

        if (m_inputManager.IsDash() && m_initDirection != m_inputManager.moveDir)
        {
            return new EntityIdleState();
        }

        if (IsStateFinished())
        {
            return new EntityRunState();
        }

        return null;
    }

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        maxFrame = 15;
        iASA = maxFrame;
        minActiveState = 1;
        maxActiveState = maxFrame;
    }

    public override void FixedUpdate()
    {
        currentFrame++;
        stateMachine.automaton.physics.Run();
    }

    public override void Enter()
    {
        base.Enter();

        EntityPhysics entityPhysics = stateMachine.automaton.physics;
        entityPhysics.currentSpeed = entityPhysics.dashInitialSpeed;
        m_initDirection = entityPhysics.direction;

        m_inputManager = stateMachine.automaton.inputManager;

        // Necessary to keep track of history.
        SaveToHistory();
    }
}
