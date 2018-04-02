using UnityEngine;

public class EntityFramedState : EntityState {
    // Interruptible As Soon As: frame at which a state can be canceled with
    // another. It gives the player a feeling of reactivity: a move can be
    // canceled by another move: it does not force the player to wait for the
    // move to end entirely before doing something else
    public int IASA { get; protected set; }
    // max frame of the state
    public int MaxFrame { get; protected set; }
    // minimum active state: it can be an hitbox which becomes active, a 
    // spell... whatever
    public int MinActiveState { get; protected set; }
    // same with max
    public int MaxActiveState { get; protected set; }
    public int CurrentFrame = 0;
    // if set to false, SetSpeed is called in HandleInput to set the animation
    // speed correctly
    protected bool IsSpeedSet = false;

    public override void Initialize() {
        // by default, everything equals MaxFrame, which equals zero
        this.MaxFrame = 0;
        this.IASA = this.MaxFrame;
        this.MinActiveState = this.MaxFrame;
        this.MaxActiveState = this.MaxFrame;
    }

    public override void Update(StateMachine stateMachine) {
        // a default update means incrementing the current frame
        this.CurrentFrame++;
    }

    // this function allows the programmer to modify the current state with the
    // player inputs. For instance, an idle state can be interrupted if the
    // player chooses to run: HandleInput will therefore return a "run" state
    public override State HandleInput(StateMachine stateMachine) {
        return null;
    }

    // check if the state can be interrupted by another one
    public virtual bool IsInterruptible(EntityStateMachine stateMachine) {
        return (this.CurrentFrame >= this.IASA);
    }

    public virtual bool IsStateFinished() {
        return this.CurrentFrame >= this.MaxFrame;
    }

    public virtual EntityState CheckInterruptibleActions(
        StateMachine stateMachine) {
        return null;
    }

    protected override void SetSpeed(EntityStateMachine stateMachine) {
        /**
         * First, we need to get the total amount of time taken by the state:
         * * 1> 1 / Time.fixedDeltaTime gives the actual amount of frames per
         * second
         * * 2> dividing MaxFrame by this results means getting the total
         * amount of time needed by the state
         *
         * e.g.: 120 / (1 / 0.017) ~= 120 / 60 = 2 seconds
         */
        float desiredAnimationTime = this.MaxFrame / (1 / Time.fixedDeltaTime);

        // then, we get the current AnimatorStateInfo...
        AnimatorStateInfo animatorStateInfo =
            stateMachine.Animator.GetCurrentAnimatorStateInfo(0);

        /**
         * ... to compute the speed needed for the state animation to have.
         * animatorStateInfo.length gives the default duration of the animator
         * state. Animator.speed gives the current speed at which the
         * animations are played (it's a float, meaning 1.0 is the normal
         * speed).
         *
         * So, animatorStateInfo.length * stateMachine.Animator.speed gives the
         * total amount of time that needs to be compared to the desired
         * animation time to get a ratio.
         */
        stateMachine.Animator.speed = (animatorStateInfo.length *
            stateMachine.Animator.speed) / desiredAnimationTime;

        // setting this to true means not calling SetSpeed anymore
        this.IsSpeedSet = true;
    }
}
