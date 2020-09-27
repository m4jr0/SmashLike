public class DebugViewCurrentFrameFramedState : DebugViewCurrentState
{
    // To be overriden in child, if necessary.
    public override string label
    {
        get
        {
            return "FS: ";
        }
    }

    void Update()
    {
        if (stateMachine.currentState is EntityFramedState entityFramedState)
        {
            textObject.text = label + entityFramedState.currentFrame;
        }
        else
        {
            textObject.text = label + "–";
        }
    }
}
