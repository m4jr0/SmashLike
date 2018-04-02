public class EntityState : State {
    // initial speed of the state, which is the initial speed of the animator
    protected float InitialSpeed = 0;

    public override State HandleInput(StateMachine stateMachine) {
        return null;
    }

    public virtual bool IsAnimationPlaying(EntityStateMachine stateMachine,
        string animationName) {
        return stateMachine.Animator.GetCurrentAnimatorStateInfo(0)
            .IsName(animationName);
    }

    public virtual bool IsCurrentAnimationFinished(
        EntityStateMachine stateMachine) {
        return this.IsCurrentAnimationPlayedPast(stateMachine, 1);
    }

    public virtual bool IsCurrentAnimationPlayedPast(
        EntityStateMachine stateMachine, float normalizedTime = 1) {
        return stateMachine.Animator.GetCurrentAnimatorStateInfo(0)
                   .normalizedTime > normalizedTime &&
               !stateMachine.Animator.IsInTransition(0);
    }

    public virtual void FreezeAnimation(EntityStateMachine stateMachine) {
        this.SetAnimationSpeed(stateMachine, 0);
    }

    public virtual void ResumeNormalAnimation(
        EntityStateMachine stateMachine) {
        this.SetAnimationSpeed(stateMachine, 1);
    }

    public virtual void SetAnimationSpeed(EntityStateMachine stateMachine,
        float speed = 1) {
        stateMachine.Animator.speed = speed;
    }

    public virtual void SaveToHistory(EntityStateMachine stateMachine) {
        stateMachine.StateHistory.Enqueue(this);
    }

    public virtual bool IsLastState(EntityStateMachine stateMachine,
        string lastStateGuessed) {
        return stateMachine.StateHistory.Peek().GetType().Name ==
               lastStateGuessed;
    }

    protected virtual void SetSpeed(EntityStateMachine entityStateMachine) {
    }

    public override void Enter(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        EntityStateMachine entityStateMachine =
            (EntityStateMachine) stateMachine;

        base.Enter(stateMachine);
        this.InitialSpeed = entityStateMachine.Animator.speed;
    }

    public override void Exit(StateMachine stateMachine) {
        if (!(stateMachine is EntityStateMachine)) return;

        EntityStateMachine entityStateMachine =
            (EntityStateMachine) stateMachine;

        entityStateMachine.Animator.speed = this.InitialSpeed;
    }
}
