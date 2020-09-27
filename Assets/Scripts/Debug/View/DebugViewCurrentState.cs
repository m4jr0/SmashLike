using UnityEngine;

public class DebugViewCurrentState : DebugViewer
{
    public EntityStateMachine stateMachine = null;

    // to be overriden in child, if necessary
    public override string label
    {
        get
        {
            return "CS: ";
        }
    }

    void Update()
    {
        textObject.text = label + stateMachine.currentState;
    }
}
