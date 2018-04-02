/* EntityAutomaton is an object that contains all the FSMs and other useful
 * attributes/methods necessary for it. */

public class EntityAutomaton : Automaton {
    public FighterInputManager FighterInputManager;
    public FighterPhysics FighterPhysics;
    public EntityStateMachine StateMachine;

    void Awake() {
        if ((this.StateMachine =
                this.gameObject.GetComponent<EntityStateMachine>()) != null) {
            return;
        }
    }
}
