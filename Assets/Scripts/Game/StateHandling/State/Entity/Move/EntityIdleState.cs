public class EntityIdleState : EntityState
{
    private EntityInputManager m_inputManager;
    private EntityAutomaton m_automaton;
    private EntityPhysics m_physics;

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

        if (m_inputManager.IsDash())
        {
            return new EntityDashState();
        }

        if (m_inputManager.IsWalk())
        {
            return new EntityWalkState();
        }

        return null;
    }

    public override void FixedUpdate()
    {
        if (m_physics.direction != m_automaton.inputManager.moveDir)
        {
            m_physics.SwapDirection();
        }
    }

    public override void Enter()
    {
        base.Enter();

        m_automaton = stateMachine.automaton;
        m_physics = m_automaton.physics;
        m_inputManager = stateMachine.automaton.inputManager;

        // Necessary to keep track of history.
        SaveToHistory();
    }
}
