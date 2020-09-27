public class DebugViewCurrentFrame : DebugViewer
{
    public FrameCounter frameCounter = null;

    // To be overriden in child, if necessary.
    public override string label
    {
        get
        {
            return "Frame: ";
        }
    }

    void Update()
    {
        if (frameCounter == null || textObject == null)
        {
            return;
        }

        textObject.text = label + frameCounter.currentFrame;
    }
}
