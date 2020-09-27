public class EntityState : State
{
    public EntityStateMachine stateMachine = null;

    // Initial speed of the state, which is the initial speed of the animator.
    protected float m_initialSpeed = 0;

    public override State HandleInput()
    {
        return null;
    }

    public override void Initialize(StateMachine stateMachine)
    {
        this.stateMachine = (EntityStateMachine)stateMachine;
    }

    public virtual bool IsAnimationPlayingMe()
    {
        return stateMachine.animator.GetCurrentAnimatorStateInfo(0).IsName(GetType().Name);
    }

    public virtual bool IsCurrentAnimationFinished()
    {
        return IsCurrentAnimationPlayedPast(1);
    }

    public virtual bool IsCurrentAnimationPlayedPast(float normalizedTime = 1)
    {
        return stateMachine.animator.GetCurrentAnimatorStateInfo(0).normalizedTime > normalizedTime && !stateMachine.animator.IsInTransition(0);
    }

    public virtual void FreezeAnimation()
    {
        SetAnimationSpeed(0);
    }

    public virtual void ResumeNormalAnimation()
    {
        SetAnimationSpeed(1);
    }

    public virtual void SetAnimationSpeed(float speed = 1)
    {
        stateMachine.animator.speed = speed;
    }

    public virtual void EnterAnimation()
    {
        m_initialSpeed = stateMachine.animator.speed;
        stateMachine.animator.SetTrigger(GetType().Name);
    }

    public virtual void ExitAnimation()
    {
        stateMachine.animator.speed = m_initialSpeed;
    }

    public virtual void SaveToHistory()
    {
        stateMachine.stateHistory.Enqueue(this);
    }

    public virtual bool IsLastState(string lastStateGuessed)
    {
        return stateMachine.stateHistory.Peek().GetType().Name == lastStateGuessed;
    }

    protected virtual void SetSpeed()
    {
    }

    public override void Enter()
    {
        base.Enter();
        EnterAnimation();
    }

    public override void Exit()
    {
        ExitAnimation();
    }
}
