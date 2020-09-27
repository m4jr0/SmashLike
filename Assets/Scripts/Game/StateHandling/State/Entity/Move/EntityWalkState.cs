using UnityEngine;

public class EntityWalkState : EntityState
{
    private float m_walkForce = 0f;
    private readonly float m_walkAnimationSpeed = 4f;
    private EntityInputManager m_inputManager;
    private EntityAutomaton m_automaton;

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

        if (!m_inputManager.IsMove())
        {
            return new EntityIdleState();
        }

        if (m_inputManager.IsDash())
        {
            return new EntityDashState();
        }

        return null;
    }

    public override void Update()
    {
        m_walkForce = Mathf.Clamp(-1, m_automaton.inputManager.GetMovePos(), 1) * m_automaton.physics.direction;
        SetSpeed();
        m_automaton.physics.Walk(m_walkForce);
    }

    public override void Enter()
    {
        base.Enter();

        m_automaton = stateMachine.automaton;
        int direction = m_automaton.inputManager.GetMovePos() > 0 ? 1 : -1;
        m_automaton.physics.direction = direction;

        m_inputManager = stateMachine.automaton.inputManager;

        // Necessary to keep track of history.
        SaveToHistory();
    }

    protected override void SetSpeed()
    {
        stateMachine.animator.speed = Mathf.Abs(m_walkForce) * m_walkAnimationSpeed;
    }
}
