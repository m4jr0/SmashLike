using UnityEngine;

public class EntityJumpState : EntityFramedState
{
    private EntityInputManager m_inputManager;
    private readonly bool m_isShortHop = false;

    public EntityJumpState(bool isShortHop = false)
    {
        m_isShortHop = isShortHop;
        Debug.Log("Short hop? " + m_isShortHop);
    }

    public override State HandleInput()
    {
        if (!IsAnimationPlayingMe())
        {
            return null;
        }

        if (IsStateFinished())
        {
            return new EntityFallState();
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
