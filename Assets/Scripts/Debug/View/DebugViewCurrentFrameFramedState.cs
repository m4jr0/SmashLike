public class DebugViewCurrentFrameFramedState : DebugViewCurrentState {
    // to be overriden in child, if necessary
    public override string Label {
        get {
            return "FS: ";
        }
    }

    void Update() {
        if (!(this.StateMachine.CurrentState is EntityFramedState)) {
            this.TextObject.text = this.Label + "–";
        } else {
            EntityFramedState entityFramedState =
                (EntityFramedState) this.StateMachine.CurrentState;

            this.TextObject.text = this.Label + entityFramedState.CurrentFrame;
        }
    }
}