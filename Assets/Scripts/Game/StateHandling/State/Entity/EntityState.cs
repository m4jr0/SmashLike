public class EntityState : State {
    public EntityStateMachine StateMachine = null;

    // initial speed of the state, which is the initial speed of the animator
    protected float InitialSpeed = 0;

    public override State HandleInput() {
        return null;
    }

    public override void Initialize(StateMachine stateMachine) {
        this.StateMachine = (EntityStateMachine) stateMachine;
    }

    public virtual bool IsAnimationPlayingMe() {
        return StateMachine.Animator.GetCurrentAnimatorStateInfo(0)
            .IsName(this.GetType().Name);
    }

    public virtual bool IsCurrentAnimationFinished() {
        return this.IsCurrentAnimationPlayedPast(1);
    }

    public virtual bool IsCurrentAnimationPlayedPast(
        float normalizedTime = 1) {
        return StateMachine.Animator.GetCurrentAnimatorStateInfo(0)
                   .normalizedTime > normalizedTime &&
               !StateMachine.Animator.IsInTransition(0);
    }

    public virtual void FreezeAnimation() {
        this.SetAnimationSpeed(0);
    }

    public virtual void ResumeNormalAnimation() {
        this.SetAnimationSpeed(1);
    }

    public virtual void SetAnimationSpeed(float speed = 1) {
        this.StateMachine.Animator.speed = speed;
    }

    public virtual void EnterAnimation() {
        this.InitialSpeed = this.StateMachine.Animator.speed;
        this.StateMachine.Animator.SetTrigger(this.GetType().Name);
    }

    public virtual void ExitAnimation() {
        this.StateMachine.Animator.speed = this.InitialSpeed;
    }

    public virtual void SaveToHistory() {
        this.StateMachine.StateHistory.Enqueue(this);
    }

    public virtual bool IsLastState(string lastStateGuessed) {
        return StateMachine.StateHistory.Peek().GetType().Name ==
               lastStateGuessed;
    }

    protected virtual void SetSpeed() {
    }

    public override void Enter() {
        base.Enter();

        this.EnterAnimation();
    }

    public override void Exit() {
        this.ExitAnimation();
    }
}
