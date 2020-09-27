public class EntityRunState : EntityState
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

        if (m_initDirection != m_inputManager.moveDir)
        {
            return new EntityTurnAroundRunState();
        }

        if (!m_inputManager.IsMove())
        {
            return new EntityRunBrakeState();
        }

        return null;
    }

    public override void Update()
    {
        stateMachine.automaton.physics.Run();
    }

    public override void Enter()
    {
        base.Enter();

        m_inputManager = stateMachine.automaton.inputManager;
        m_initDirection = m_inputManager.moveDir;

        // Necessary to keep track of history.
        SaveToHistory();
    }
}
