public class EntityRunBrakeState : EntityFramedState
{
    private EntityInputManager _inputManager;

    public override State HandleInput()
    {
        if (!IsAnimationPlayingMe())
        {
            return null;
        }

        if (_inputManager.IsJump())
        {
            return new EntityJumpSquatState();
        }

        return IsStateFinished() ? new EntityIdleState() : null;
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
    }

    public override void Enter()
    {
        base.Enter();

        _inputManager = stateMachine.automaton.inputManager;
    }
}
