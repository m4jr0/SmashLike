using UnityEngine;

public class EntityFramedState : EntityState
{
    // Interruptible As Soon As: frame at which a state can be canceled with
    // another. It gives the player a feeling of reactivity: a move can be
    // canceled by another move: it does not force the player to wait for the
    // move to end entirely before doing something else.
    public int iASA
    {
        get; protected set;
    }
    // Max frame of the state.
    public int maxFrame
    {
        get; protected set;
    }
    // Minimum active state: it can be an hitbox which becomes active, a 
    // spell... whatever.
    public int minActiveState
    {
        get; protected set;
    }
    // Same with the maximum active state.
    public int maxActiveState
    {
        get; protected set;
    }
    public int currentFrame = 0;
    // If set to false, SetSpeed is called in HandleInput to set the animation
    // speed correctly.
    protected bool m_isSpeedSet = false;

    public override void Initialize(StateMachine stateMachine)
    {
        base.Initialize(stateMachine);

        // Ny default, everything equals MaxFrame, which equals zero.
        maxFrame = 0;
        iASA = maxFrame;
        minActiveState = maxFrame;
        maxActiveState = maxFrame;
    }

    public override void Update()
    {
        // A default update means incrementing the current frame.
        currentFrame++;
    }

    // This function allows the programmer to modify the current state with the
    // player inputs. For instance, an idle state can be interrupted if the
    // player chooses to run: HandleInput will therefore return a "run" state.
    public override State HandleInput()
    {
        return null;
    }

    // Check if the state can be interrupted by another one.
    public virtual bool IsInterruptible()
    {
        return (currentFrame >= iASA);
    }

    public virtual bool IsStateFinished()
    {
        return currentFrame >= maxFrame;
    }

    public virtual EntityState CheckInterruptibleActions()
    {
        return null;
    }

    protected override void SetSpeed()
    {
        /**
         * First, we need to get the total amount of time taken by the state:
         * * 1> 1 / Time.fixedDeltaTime gives the actual frames per second 
		 * ratio
         * * 2> dividing MaxFrame by this results means getting the total
         * amount of time needed by the state
         *
         * e.g.: 120 / (1 / 0.017) ~= 120 / 60 = 2 seconds
         */
        float desiredAnimationTime = maxFrame / (1 / Time.fixedDeltaTime);

        // Then, we get the current AnimatorStateInfo...
        AnimatorStateInfo animatorStateInfo = stateMachine.animator.GetCurrentAnimatorStateInfo(0);

        /**
         * ... to compute the speed needed for the state animation to have.
         * animatorStateInfo.length gives the default duration of the animator
         * state. Animator.speed gives the current speed at which the
         * animations are played (it's a float, meaning 1.0 is the normal
         * speed).
         *
         * So, animatorStateInfo.length * StateMachine.Animator.speed gives the
         * total amount of time that needs to be compared to the desired
         * animation time to get a ratio.
         */
        stateMachine.animator.speed = (animatorStateInfo.length * stateMachine.animator.speed) / desiredAnimationTime;

        // Setting this to true means not calling SetSpeed anymore.
        m_isSpeedSet = true;
    }
}
