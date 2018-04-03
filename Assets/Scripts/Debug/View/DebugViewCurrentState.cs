using UnityEngine;

public class DebugViewCurrentState : DebugViewer {
    public EntityStateMachine StateMachine = null;

    // to be overriden in child, if necessary
    public override string Label {
        get { return "CS: "; }
    }

    void Update() {
        this.TextObject.text = this.Label + this.StateMachine.CurrentState;
    }
}
