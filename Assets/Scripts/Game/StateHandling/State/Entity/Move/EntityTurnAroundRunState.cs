public class EntityTurnAroundRunState : EntityFramedState
{
    private EntityPhysics m_physics;
    private EntityInputManager m_inputManager;
    private float m_speedThreshold = 0.02f;
    private int m_initMoveDir = 0;
    private float m_currentSpeed = 0;
    private float m_brakeForce = 0;

    public override State HandleInput()
    {
        /*if (!IsAnimationPlayingMe())
		{
            return null;
        }*/

        if (m_inputManager.IsJump())
        {
            return new EntityJumpSquatState();
        }

        if (IsStateFinished())
        {
            if (m_initMoveDir != m_inputManager.moveDir)
            {
                return new EntityTurnAroundState();
            }
            else if (m_inputManager.IsMove())
            {
                return new EntityRunState();
            }

            return new EntityIdleState();
        }

        return null;
    }

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        maxFrame = 19;
        iASA = maxFrame;
        minActiveState = 1;
        maxActiveState = maxFrame;
    }

    public override void Update()
    {
        currentFrame++;

        if (m_currentSpeed > m_speedThreshold)
        {
            m_currentSpeed -= m_brakeForce;
            m_physics.Run(m_currentSpeed);
            return;
        }

        if (m_initMoveDir != m_physics.direction)
        {
            m_physics.direction = m_initMoveDir;
        }

        m_currentSpeed += m_brakeForce;
        m_physics.Run(m_currentSpeed);
    }

    public override void Enter()
    {
        base.Enter();

        m_physics = stateMachine.automaton.physics;
        m_currentSpeed = 1.0f;
        m_brakeForce = 1.0f / (maxFrame / 2.0f);

        m_inputManager = stateMachine.automaton.inputManager;
        m_initMoveDir = m_inputManager.moveDir;

        // Necessary to keep track of history.
        SaveToHistory();
    }
}
