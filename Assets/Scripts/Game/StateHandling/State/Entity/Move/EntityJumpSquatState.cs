public class EntityJumpSquatState : EntityFramedState
{
    private EntityInputManager m_inputManager;
    private bool m_isShortHop = false;

    public override State HandleInput()
    {
        /*if (!IsAnimationPlayingMe())
		{
            return null;
        }*/

        if (!m_isShortHop && !m_inputManager.IsJump())
        {
            m_isShortHop = true;
        }

        if (IsStateFinished())
        {
            return new EntityJumpState(m_isShortHop);
        }

        return null;
    }

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        maxFrame = 5;
        iASA = maxFrame;
        minActiveState = 1;
        maxActiveState = maxFrame;
    }

    public override void Update()
    {
        currentFrame++;

        // TODO: Handle update behavior.
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
